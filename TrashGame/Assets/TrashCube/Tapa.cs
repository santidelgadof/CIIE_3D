using UnityEngine;

public class Tapa : MonoBehaviour
{
    public GameObject tapa; // Asigna la tapa del cubo de basura en el Inspector
    public string itemTag = "TrashItem"; // Etiqueta de los objetos que abrirán la tapa
    public KeyCode keyToDrop = KeyCode.E; // Tecla para soltar el item
    public TrashType trashType;

    private Collider item;

    private bool isTapaOpen = false; // Variable para rastrear si la tapa está abierta o cerrada

    private void Update()
    {
        // Verifica si se presiona la tecla para soltar el item
        if (Input.GetKeyDown(keyToDrop))
        {
            // Verifica si la tapa está abierta
            if (isTapaOpen)
            {
                // Suelta el item dentro del contenedor (puedes agregar tu lógica aquí)
                //Debug.Log("Item soltado dentro del contenedor de basura");
                TrashItem tI = item.GetComponent<TrashItem>();
                if (tI.trashType == trashType) {
                    /// TODO: Add logic on correct classification
                    Destroy(item.gameObject);
                } else {
                    ///TODO: Add logic on incorrect classification
                }
                tapa.SetActive(true);
                isTapaOpen = false;
                item = null;
            }
            else
            {
                // La tapa está cerrada, no se puede soltar el item
                //Debug.Log("No se puede soltar el item porque la tapa está cerrada");
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
}
