using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float smoothRot = 0.05f;
    private float currentVel;

    [SerializeField] private LayerMask grabbableObjectLayer;
    [SerializeField] private GameObject grabBox;
    [SerializeField] private Transform grabPos;
    private Vector3 grabOffset;
    private Collider grabbedObject;

    private Animator animator;

    //First person camera rotation
    [SerializeField] private float sensX = 400;
    [SerializeField] private float sensY = 400;

    float xRotation;
    float yRotation;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera sceneCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sceneCamera.enabled = true;
        playerCamera.enabled = false;
      
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        movement();
        //Grab objects
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject != null)
            {
                // Soltar objeto si ya se está agarrando uno

                ReleaseObject();
            }
            else
            {

                // Intentar agarrar un nuevo objeto
                TryGrabObject();
            }
        }
        //Switch cameras
        if (Input.GetKeyDown(KeyCode.K))
        {
            sceneCamera.enabled = !sceneCamera.enabled;
            playerCamera.enabled = !playerCamera.enabled;
        }
        if (grabbedObject != null)
        {
            moveGrabbedObject();
        }
    }

    private void moveGrabbedObject()
    {
        if (grabbedObject != null)
        {
            Vector3 newPos = grabPos.position + grabOffset;
            grabbedObject.transform.position = grabPos.transform.position;
        }
    }

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

                Debug.Log("Objeto agarrado: " + grabbedObject.name);
            }
        }
    }

    void ReleaseObject()
    {
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Objeto soltado: " + grabbedObject.name);
        grabbedObject = null;
    }

    private void movement()
    {
        Vector3 movement;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.W)) { }
        if (Input.GetKeyDown(KeyCode.A)) { }
        if (Input.GetKeyDown(KeyCode.S)) { }
        if (Input.GetKeyDown(KeyCode.D)) { }

        


        movement = Vector3.zero;

        if (playerCamera.isActiveAndEnabled)
        {
            //Debug.Log("cameraActive");
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Math.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            movement = transform.forward * moveVertical + transform.right * moveHorizontal;
            rb.AddForce(movement.normalized * speed * 100, ForceMode.Force);
        }
        


        // Vector3 move = new Vector3(moveHorizontal, moveVertical).normalized; //hace que en diagonal vaya normal pero tarda en parar
        //Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized * speed;
        //Vector3 movement = new Vector3(moveHorizontal * speed, rb.velocity.y, moveVertical * speed);//.normalized * speed;
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            animator.SetFloat("speed", 0);
            rb.velocity = Vector3.zero;

        }
        else if (sceneCamera.isActiveAndEnabled)
        {
            //Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized * speed;
            movement = new Vector3(moveHorizontal, 0, moveVertical);
            rb.AddForce(movement.normalized * speed * 100, ForceMode.Force);

            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVel, smoothRot);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        }


        if (moveHorizontal != 0 || moveVertical != 0) { 
            rb.velocity = movement;

            animator.SetFloat("speed", 0.2f);
        }
    }
}
