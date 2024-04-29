using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tapa : MonoBehaviour
{
    public GameObject tapa; // Asigna la tapa del cubo de basura en el Inspector
    private const string itemTag = "TrashItem"; // Etiqueta de los objetos que abrirán la tapa

    public KeyCode keyToDrop = KeyCode.E; // Tecla para soltar el item
    public TrashType trashType;

    public AudioSource correctSong;

    public AudioSource wrongSong;

    public AudioSource dropSong;

    public GameObject trashBag;

    private PlayerDetector playerDetector;

    public int capacity = 5;

    public int penalization;
    private int amountAlreadyIn;

    private Collider item;

    private TMP_Text itemCountText;
    private Image progressBar;

    private CharacterScript player;

    private bool isTapaOpen = false; // Variable para rastrear si la tapa está abierta o cerrada

    private Vector3 spawnPosition;


    
     private void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterScript>();
        switch (trashType)
        {
            case TrashType.Organic:
                progressBar = GameObject.Find("OrganicIndicator").GetComponent<Image>();
                itemCountText = GameObject.Find("OrganicIndicatorText").GetComponent<TMP_Text>();
                playerDetector = GameObject.Find("OrganicPlayerDetector").GetComponent<PlayerDetector>();
                break;
            case TrashType.Inorganic:
                progressBar = GameObject.Find("InorganicIndicator").GetComponent<Image>();
                itemCountText = GameObject.Find("InorganicIndicatorText").GetComponent<TMP_Text>();
                playerDetector = GameObject.Find("InorganicPlayerDetector").GetComponent<PlayerDetector>();
                break;
            case TrashType.Paper:
                progressBar = GameObject.Find("PaperIndicator").GetComponent<Image>();
                itemCountText = GameObject.Find("PaperIndicatorText").GetComponent<TMP_Text>();
                playerDetector = GameObject.Find("PaperPlayerDetector").GetComponent<PlayerDetector>();
                break;
            case TrashType.Glass:
                progressBar = GameObject.Find("GlassIndicator").GetComponent<Image>();
                itemCountText = GameObject.Find("GlassIndicatorText").GetComponent<TMP_Text>();
                playerDetector = GameObject.Find("GlassPlayerDetector").GetComponent<PlayerDetector>();
                break;

            default:
            break;
        }
        
        
    }

    private void Update()
    {
        
        /// Check if the user is close to Self.
        if (playerDetector.IsUserHere && amountAlreadyIn == capacity) {
            isTapaOpen = true;
            tapa.SetActive(false);
        } else if (!playerDetector.IsUserHere && amountAlreadyIn == capacity) {
            isTapaOpen = false;
            tapa.SetActive(true);
        }
        
        // When the user presses the defined key:
        if (Input.GetKeyDown(keyToDrop))
        {
            
            if (isTapaOpen)
            {
                if (item != null) { /// Drop the item inside the container.                    
                    TrashItem tI = item.GetComponent<TrashItem>();
                    correctSong.volume = 0.2F;
                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        if (trashType == TrashType.Inorganic)
                        {
                            if (tI.trashType == TrashType.Inorganic || tI.trashType == TrashType.Paper || tI.trashType == TrashType.Glass)
                            { /// Correct classification                        
                                if (amountAlreadyIn + 1 <= capacity)
                                {
                                    /// Container has enough space.
                                    amountAlreadyIn += 1;
                                    correctSong.Play();
                                    Destroy(item.gameObject);
                                    UpdateUI();
                                    
                                }
                            }
                            else
                            { /// Wrong classification
                                wrongSong.Play();
                                amountAlreadyIn -= penalization;
                                if (amountAlreadyIn < 0)
                                    amountAlreadyIn = 0;
                                Destroy(item.gameObject);
                                UpdateUI();
                            }
                        }
                        else if (trashType == TrashType.Organic)
                        {
                            if (tI.trashType == TrashType.Organic)
                            { /// Correct classification                        
                                if (amountAlreadyIn + 1 <= capacity)
                                {
                                    /// Container has enough space.
                                    amountAlreadyIn += 1;
                                    correctSong.Play();
                                    Destroy(item.gameObject);
                                    UpdateUI();
                                }
                            }
                            else
                            { /// Wrong classification
                                wrongSong.Play();
                                amountAlreadyIn -= penalization;
                                if (amountAlreadyIn < 0)
                                    amountAlreadyIn = 0;
                                Destroy(item.gameObject);
                                UpdateUI();
                            }
                        }
                    }
                    else 
                    {
                        if (tI.trashType == trashType)
                        { /// Correct classification                        
                            if (amountAlreadyIn + 1 <= capacity)
                            {
                                /// Container has enough space.
                                amountAlreadyIn += 1;
                                correctSong.Play();
                                Destroy(item.gameObject);
                                UpdateUI();
                            }
                        }
                        else
                        { /// Wrong classification
                            wrongSong.Play();
                            amountAlreadyIn -= penalization;
                            if (amountAlreadyIn < 0)
                                amountAlreadyIn = 0;
                            Destroy(item.gameObject);
                            UpdateUI();
                        }
                    }

                } else { /// The player is not carrying a TrashItem.
                    if (amountAlreadyIn == capacity) { /// The container is filled.
                        dropSong.Play();
                        Vector3 spawnPosition = transform.position;
                        Vector3 localOffset = new Vector3(1f, 0f, 0f);
                        Vector3 worldOffset = transform.TransformDirection(localOffset);
                        spawnPosition += worldOffset;
                        TransformIntoBag(spawnPosition);
                        amountAlreadyIn = 0;
                        UpdateUI();
                    }
                }
                tapa.SetActive(true);
                isTapaOpen = false;
                item = null;
            }
            else
            {   
                
            }
        }
    }

    public void TransformIntoBag(Vector3 spawnPosition)
    {
        GameObject bolsa = Instantiate(trashBag, spawnPosition, Quaternion.Euler(-75f, 0f, 0f));

        // Obtener el Rigidbody y el Collider de la bolsa
        Rigidbody bolsaRigidbody = bolsa.GetComponent<Rigidbody>();
        Collider bolsaCollider = bolsa.GetComponent<Collider>();

        // Si no hay un Rigidbody, añadir uno
        if (bolsaRigidbody == null)
        {
            bolsaRigidbody = bolsa.AddComponent<Rigidbody>();
        }

        // Si no hay un Collider, añadir uno
        if (bolsaCollider == null)
        {
            bolsaCollider = bolsa.AddComponent<BoxCollider>();
        }

        // Asignar la etiqueta "bag" al objeto de la bolsa
        bolsa.tag = "bag";
        bolsa.layer = LayerMask.NameToLayer("grab");

        // Habilitar el Rigidbody y el Collider
        bolsaRigidbody.isKinematic = false;
        bolsaCollider.enabled = true;


        // Ajustar la escala de la bolsa
        bolsa.transform.localScale = new Vector3(450f, 450f, 450f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en contacto tiene la etiqueta correcta
        if (other.CompareTag(itemTag))
        {
            // Desactiva la tapa
            tapa.SetActive(false);
            isTapaOpen = true;
            item = other;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del contacto tiene la etiqueta correcta
        if (other.CompareTag(itemTag))
        {
            // Activa la tapa nuevamente
            tapa.SetActive(true);
            isTapaOpen = false;
            item = null;
        }

        
    }

    /// <summary>
    /// Updates the indicators with the new data.
    /// </summary>
    private void UpdateUI()
    {
        float fillAmount = (float)amountAlreadyIn / capacity;
        progressBar.fillAmount = fillAmount;
        itemCountText.text = amountAlreadyIn.ToString();
    }

    
}
