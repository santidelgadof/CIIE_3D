using UnityEngine;

public class Tapa : MonoBehaviour
{
    public GameObject tapa; // Asigna la tapa del cubo de basura en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Cuando otro objeto colisiona con el cubo de basura
        if (other.CompareTag("cubo")) // Cambia "Jugador" por la etiqueta del objeto que se acerca
        {
            // Desactiva la tapa
            tapa.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el objeto sale de la colisión con el cubo de basura
        if (other.CompareTag("cubo"))
        {
            // Activa la tapa nuevamente
            tapa.SetActive(true);
        }
    }
}
