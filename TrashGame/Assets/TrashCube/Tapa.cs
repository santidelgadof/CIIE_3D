using UnityEngine;

public class Tapa : MonoBehaviour
{
    public GameObject tapa; // Asigna la tapa del cubo de basura en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Cuando cualquier objeto colisiona con el cubo de basura
        // (debido a que el collider es un trigger)
        // Desactiva la tapa
        tapa.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el objeto sale de la colisión con el cubo de basura
        // Activa la tapa nuevamente
        tapa.SetActive(true);
    }
}
