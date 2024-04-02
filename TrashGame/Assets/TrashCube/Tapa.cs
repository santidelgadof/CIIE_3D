using UnityEngine;

public class Tapa : MonoBehaviour
{
    public GameObject tapa; // Asigna la tapa del cubo de basura en el Inspector
    public string itemTag = "TrashItem"; // Etiqueta de los objetos que abrirán la tapa

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en contacto tiene la etiqueta correcta
        if (other.CompareTag(itemTag))
        {
            // Desactiva la tapa
            tapa.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del contacto tiene la etiqueta correcta
        if (other.CompareTag(itemTag))
        {
            // Activa la tapa nuevamente
            tapa.SetActive(true);
        }
    }
}
