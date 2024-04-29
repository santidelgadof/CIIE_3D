using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public float speed = 2.9f;
    private float beltWidth; 

    private float beltHeight;
    // Start is called before the first frame update
    void Start()
    {

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            beltWidth = collider.bounds.size.x;
            beltHeight = collider.bounds.size.z;
        }
        else
        {
            Debug.LogError("No collider found on the belt object!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSpeed(float sp)
    {
        speed += sp;
    }

    /// <summary>
    /// Handles the movement of the items over the belt.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay(Collision collision) {

        if ( collision.gameObject.tag == "TrashItem") {

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate the direction in which the belt is moving
                Vector3 movementDirection = transform.right;

                // Calculate the position to move the object to along the belt's surface
                Vector3 newPosition = collision.transform.position + movementDirection * speed * Time.deltaTime;

                // Clamp the object's position within the bounds of the belt
                
                

                // Move the object
                rb.MovePosition(newPosition);
            }

        }

    }
}
