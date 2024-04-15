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
    private GameObject grabBox;
    private Transform grabPos;
    private Vector3 grabOffset;
    private Collider grabbedObject;

    private Animator animator;

    //First person camera rotation
    [SerializeField] private float sensX = 400;
    [SerializeField] private float sensY = 400;

    float xRotation;
    float yRotation;

    private GameObject playerCamera;
    private GameObject sceneCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        grabBox = GameObject.Find("GrabCollider");
        grabPos = transform.Find("GrabPosition");

        playerCamera = GameObject.Find("PlayerCamera");
        sceneCamera = GameObject.Find("Main Camera");
        sceneCamera.SetActive(true);
        playerCamera.SetActive(false);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (!PauseMenu.isGamePaused)
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
            //Switch cameras
            if (Input.GetKeyDown(KeyCode.K))
            {
                //CameraSwitch.CameraSwitched();
                sceneCamera.SetActive(!sceneCamera.activeSelf);
                playerCamera.SetActive(!playerCamera.activeSelf);
            }
            if (grabbedObject != null)
            {
                moveGrabbedObject();
            }

            if (playerCamera.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
        //Switch cameras
        if (Input.GetKeyDown(KeyCode.K))
        {
            //CameraSwitch.CameraSwitched();
            sceneCamera.SetActive(!sceneCamera.activeSelf);
            playerCamera.SetActive(!playerCamera.activeSelf);
        }
        if (grabbedObject != null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
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
      
        if (colliders.Length > 0)
        {
            
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

               
            }
        }
    }

    void ReleaseObject()
    {

        RaycastHit hit;
        if (Physics.Raycast(grabbedObject.transform.position, Vector3.down, out hit))
        {
            // Check if the object below is named "belt"
            if (hit.collider.CompareTag("Belt"))
            {
                // Object is over the belt, so release it
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                
                grabbedObject = null;
            }
            else
            {
                
                // Object is not over the belt, so don't release it
                
            }
        }
        
        
        
    }

    private void movement()
    {
        Vector3 movement;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = Vector3.zero;

        if (playerCamera.activeSelf)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Math.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            movement = transform.forward * moveVertical + transform.right * moveHorizontal;
            rb.AddForce(100 * speed * movement.normalized, ForceMode.Force);
        }


        if (moveHorizontal == 0 && moveVertical == 0)
        {
            animator.SetFloat("speed", 0);
            rb.velocity = Vector3.zero;

        }

        else if (sceneCamera.activeSelf)
        {
          
            movement = new Vector3(moveHorizontal, 0, moveVertical);
            rb.AddForce(100 * speed * movement.normalized, ForceMode.Force);

            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVel, smoothRot);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        }


        if (moveHorizontal != 0 || moveVertical != 0)
        {
            rb.velocity = movement;

            animator.SetFloat("speed", 0.2f);
        }
    }
}
