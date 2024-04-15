using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Helps detect when the player is close to a TrashContainer.
/// </summary>
public class PlayerDetector : MonoBehaviour
{
    public bool IsUserHere = false;
    
    void Start(){}

    void Update(){}

    private void OnTriggerEnter(Collider other){ IsUserHere = true; }

    private void OnTriggerExit(Collider other){ IsUserHere = false; }
}
