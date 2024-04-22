using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOut : MonoBehaviour
{

    [SerializeField] private CharacterScript characterScript;
    private void Start()
    {
    characterScript = GameObject.Find("Player").GetComponent<CharacterScript>();
    }
    // Called when another collider exits the trigger collider attached to this object
    private void OnTriggerExit(Collider other)
    {
        if (characterScript.GetGrabbedObject() != null ) {
            if (!GameObject.ReferenceEquals(other.gameObject, characterScript.GetGrabbedObject())) {
            // Check if the exiting collider belongs to a TrashItem
                if (other.CompareTag("TrashItem"))
                {
                    // Destroy the TrashItem game object
                    Destroy(other.gameObject);
                    if (characterScript != null)
                    {
                        characterScript.LoseLife();
                    }
                    
                }
            }
        } else {
            if (other.CompareTag("TrashItem"))
        {
            // Destroy the TrashItem game object
            Destroy(other.gameObject);
            if (characterScript != null)
            {
                characterScript.LoseLife();
            }
        }
        }
    }
}
