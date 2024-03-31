using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOut : MonoBehaviour
{
    // Called when another collider exits the trigger collider attached to this object
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider belongs to a TrashItem
        if (other.CompareTag("TrashItem"))
        {
            // Destroy the TrashItem game object
            Destroy(other.gameObject);
        }
    }
}
