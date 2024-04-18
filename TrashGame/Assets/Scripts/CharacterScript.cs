using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;



public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 originalGrabbedObjectPosition;

    [SerializeField] private float speed;
    [SerializeField] private float smoothRot = 0.05f;
    private float currentVel;

    [SerializeField] private LayerMask grabbableObjectLayer;
    [SerializeField] private GameObject grabBox;
    [SerializeField] private Transform grabPos;

    private int totalPuntuation;
    private Vector3 grabOffset;
    private Collider grabbedObject;

    private Animator animator;
    private MouseAI mouseAI;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mouseAI = FindObjectOfType<MouseAI>();
        totalPuntuation = 0;
    }

    
    void Update()
    {
        movement();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject != null)
            {
                // Soltar objeto si ya se est� agarrando uno

                ReleaseObject();
            }
            else
            {

                // Intentar agarrar un nuevo objeto
                TryGrabObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && grabbedObject != null && grabbedObject.CompareTag("escoba"))
        {
            // Verificar si el ratón está cerca
            if (Vector3.Distance(transform.position, mouseAI.transform.position) < 2f)
            {
                // "Matar" al ratón deteniendo su movimiento
                mouseAI.SetMouseStopped(true);
                Debug.Log("¡Ratón muerto!");
            }
        }

        if (grabbedObject != null)
        {
            moveGrabbedObject();
        }
    }

    /// <summary>
    /// Calculates and sets the new position for the Grabbed Object
    /// </summary>
    private void moveGrabbedObject()
    {
        if (grabbedObject != null)
        {
            Vector3 newPos = grabPos.position + grabOffset;
            grabbedObject.transform.position = grabPos.transform.position;
        }
    }

    /// <summary>
    /// The character tries to grab an object. 
    /// </summary>
    private void TryGrabObject()
    {
        Collider[] colliders = Physics.OverlapBox(grabBox.transform.position, grabBox.transform.lossyScale, grabBox.transform.rotation, grabbableObjectLayer);
        Debug.Log(colliders.Length);
        if (colliders.Length > 0)
        {
            Debug.Log("objs encontrados");
            float closestDist = 20;

            Collider closestObject = null; 
            foreach (var collider in colliders)
            {

                closestDist = Math.Min(closestDist, Vector3.Distance(transform.position, collider.transform.position));
                collider.TryGetComponent(out closestObject);

            }
            if (closestObject != null)
            {
                grabbedObject = closestObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabOffset = grabbedObject.transform.position - grabPos.position;
                originalGrabbedObjectPosition = grabbedObject.transform.position;

                Debug.Log("Objeto agarrado: " + grabbedObject.name);
            }
        }
    }

    /// <summary>
    /// The character tries to release an object.
    /// </summary>
    void ReleaseObject()
    {

        RaycastHit hit;
        if (Physics.Raycast(grabbedObject.transform.position, Vector3.down, out hit))
        {
            // Si el objeto tiene la etiqueta "Escoba", lo soltamos independientemente de su posición
            if (grabbedObject.CompareTag("escoba")||grabbedObject.CompareTag("bag") && !hit.collider.CompareTag("Belt"))
            {
                if (grabbedObject.CompareTag("escoba"))
                {
                    grabbedObject.transform.position = originalGrabbedObjectPosition;
                }
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject = null;
                return;
            } 


            // Check if the object below is named "belt"
            if (hit.collider.CompareTag("Belt") && !grabbedObject.CompareTag("bag"))
            {
                // Object is over the belt, so release it
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                Debug.Log("Objeto soltado sobre la cinta: " + grabbedObject.name);
                grabbedObject = null;
            }
            else
            {
                    
                // Object is not over the belt, so don't release it
                    
            }
            
        }
        
        
        
    }

    /// <summary>
    /// Calculates and sets the movement of the character.
    /// </summary>
    private void movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            animator.SetFloat("speed", 0);
            return;
        }
        
        // Vector3 move = new Vector3(moveHorizontal, moveVertical).normalized; //hace que en diagonal vaya normal pero tarda en parar
        Vector3 movement = new Vector3(moveHorizontal * speed, rb.velocity.y, moveVertical * speed);

        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVel, smoothRot);

        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        rb.velocity = movement;

        animator.SetFloat("speed", 0.2f);
    }

    /// <summary>
    /// Ignores collisions with TrashItems.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TrashItem"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    public void Point() {
        this.totalPuntuation += 100;
    }

    public int GetTotalPuntuation() {
        return totalPuntuation;
    }
}
