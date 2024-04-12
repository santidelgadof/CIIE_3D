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

    private bool isTapaOpen = false; // Variable para rastrear si la tapa está abierta o cerrada

    
     private void Start()
    {
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
        
        
        if (playerDetector.IsUserHere) {
            isTapaOpen = true;
            tapa.SetActive(false);
        } else {
            isTapaOpen = false;
            tapa.SetActive(true);
        }
        
        // Verifica si se presiona la tecla para soltar el item
        if (Input.GetKeyDown(keyToDrop))
        {
            // Verifica si la tapa está abierta
            if (isTapaOpen)
            {
                // Suelta el item dentro del contenedor (puedes agregar tu lógica aquí)
                //Debug.Log("Item soltado dentro del contenedor de basura");
                if (item != null) {
                    TrashItem tI = item.GetComponent<TrashItem>();
                    if (tI.trashType == trashType) {
                        /// TODO: Add logic on correct classification
                        if (amountAlreadyIn + 1 <= capacity) {
                            amountAlreadyIn += 1;
                            Destroy(item.gameObject);                
                            UpdateUI();
                        } 
                    } else {
                        amountAlreadyIn -= penalization;
                        if (amountAlreadyIn < 0)
                            amountAlreadyIn = 0;
                        Destroy(item.gameObject);  
                        UpdateUI();
                    }
                } else {
                    if (amountAlreadyIn == capacity) {
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

   

    private void UpdateUI()
    {
        float fillAmount = (float)amountAlreadyIn / capacity;
        progressBar.fillAmount = fillAmount;
        itemCountText.text = amountAlreadyIn.ToString();
    }

    
}
