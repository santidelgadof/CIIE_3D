using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines a TrashItem and describes its TrashType
/// </summary>
public class TrashItem : MonoBehaviour
{   
    public TrashType trashType;
    
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}    
}

/// <summary>
/// Enum that contains the TrashTypes that will exist in the game.
/// </summary>
public enum TrashType {
    Organic,
    Inorganic,
    Paper,
    Glass
}
