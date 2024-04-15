using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tapa : MonoBehaviour
{
    public GameObject tapa; // Asigna la tapa del cubo de basura en el Inspector
    private const string itemTag = "TrashItem"; // Etiqueta de los objetos que abrirán la tapa

    public KeyCode keyToDrop = KeyCode.E; // Tecla para soltar el item
    public TrashType trashType;

    public GameObject trashBag;

    private PlayerDetector playerDetector;

    public int capacity;

    public int penalization;
    private int amountAlreadyIn;

    private Collider item;

    private TMP_Text itemCountText;
    private Image progressBar;

    private CharacterScript player;

    private bool isTapaOpen = false; // Variable para rastrear si la tapa está abierta o cerrada

    
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
        if (playerDetector.IsUserHere) {
            isTapaOpen = true;
            tapa.SetActive(false);
        } else {
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
                    if (tI.trashType == trashType) { /// Correct classification                        
                        if (amountAlreadyIn + 1 <= capacity) {
                            /// Container has enough space.
                            amountAlreadyIn += 1;
                            Destroy(item.gameObject);                
                            UpdateUI();
                        } 
                    } else { /// Wrong classification
                        amountAlreadyIn -= penalization;
                        if (amountAlreadyIn < 0)
                            amountAlreadyIn = 0;
                        Destroy(item.gameObject);  
                        UpdateUI();
                    }
                } else { /// The player is not carrying a TrashItem.
                    if (amountAlreadyIn == capacity) { /// The container is filled.
                    Vector3 spawnPosition = transform.position;
                    if (trashType == TrashType.Organic || trashType == TrashType.Inorganic)
                    {
                        spawnPosition = transform.position + new Vector3(-3f, 0f, 0f);
                    } else {
                        spawnPosition = transform.position + new Vector3(0f, 0f, 0f);
                    }
                    
                    Instantiate(trashBag, spawnPosition, Quaternion.identity);
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
