using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}

    /// <summary>
    /// Ignores collisions with Hole and TrashItems
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TrashItem") || collision.gameObject.CompareTag("Hole") )
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
