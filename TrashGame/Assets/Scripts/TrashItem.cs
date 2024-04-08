using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItem : MonoBehaviour
{
    
    public TrashType trashType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    
}

public enum TrashType {
    Organic,
    Inorganic,
    Paper,
    Glass
}
