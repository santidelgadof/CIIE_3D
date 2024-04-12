using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool IsUserHere = false;
    private string itemTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en contacto tiene la etiqueta correcta
        
        
            // Desactiva la tapa
            IsUserHere = true;
        

        
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del contacto tiene la etiqueta correcta
        
            // Activa la tapa nuevamente
            IsUserHere = false;
        

        
    }
}
