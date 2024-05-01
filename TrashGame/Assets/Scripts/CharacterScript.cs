using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;



public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 originalGrabbedObjectPosition;

     
    [SerializeField] private List<GameObject> heartImages;
    [SerializeField] private List<Belt> belts;

    [SerializeField] private float speed = 10;
    [SerializeField] private float smoothRot = 0.05f;
    private float currentVel;

    [SerializeField] private LayerMask grabbableObjectLayer;
    private GameObject grabBox;
    public GameObject loseCanvas;
    private Transform grabPos;
    private Vector3 grabOffset;
    private Collider grabbedObject;

    [SerializeField] private FloatOs scoreSo;
    [SerializeField] private LivesCounter lives;
    [SerializeField] private TextMeshProUGUI scoreText;
   
    [SerializeField]private GameObject MouseSOS;

    [SerializeField] private float mouseActiveTime = 0f;
    private bool isMouseActive = false;

    [SerializeField] private TextMeshProUGUI timerText;

    private Animator animator;
    private MouseAI mouseAI;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        mouseAI = FindObjectOfType<MouseAI>();

        grabBox = GameObject.Find("GrabCollider");
        grabPos = transform.Find("GrabPosition");
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            scoreSo.Value = 0;
        }

    }
    private void Start()
    {
        UpdateHeartsUI();
    }


    void Update()
    {
        if (!PauseMenu.isGamePaused)
        {
            if (mouseAI != null)
            {   
                MouseSOS.SetActive(true); 
                // Incrementar el tiempo si el ratón está activo
                if (mouseAI.gameObject.activeSelf)
                {
                    mouseActiveTime += Time.deltaTime;

                    // Actualizar el temporizador en la interfaz de usuario
                    float timeRemaining = Mathf.Max(0, 15f - mouseActiveTime);
                    if (timeRemaining < 10)
                    {
                        timerText.text = "0" + timeRemaining.ToString("F0") ;
                    } else
                    {
                        timerText.text = "" + timeRemaining.ToString("F0") ;
                    }    


                    // Restar puntos si el ratón está activo por más de 15 segundos
                    if (mouseActiveTime > 15f && !isMouseActive)
                    {
                        scoreSo.Value -= 100;
                        isMouseActive = true; // Marcar que se restaron puntos para evitar restarlos continuamente
                        mouseAI.DestroyMouse();
                        MouseSOS.SetActive(false);
                    }
                }
                else
                {
                    // Reiniciar el tiempo si el ratón está desactivado
                    mouseActiveTime = 0f;
                    isMouseActive = false;
                    MouseSOS.SetActive(false); 

                    
                }

                // Restablecer el tiempo si el ratón está desactivado
                if (!mouseAI.gameObject.activeSelf)
                {
                    mouseActiveTime = 0f;
                    isMouseActive = false;
                    MouseSOS.SetActive(false); 
                }
            }    
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
                    MouseSOS.SetActive(false);
                }
            }

            if (grabbedObject != null)
            {
                moveGrabbedObject();
            }
            UpdateScoreUI();

            /*
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
            }*/

        }
        /*else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }*/
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
        //Debug.Log(colliders.Length);
        if (colliders.Length > 0)
        {
            //Debug.Log("objs encontrados");
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
                grabbedObject.GetComponent<BoxCollider>().isTrigger = true;
                grabOffset = grabbedObject.transform.position - grabPos.position;
                originalGrabbedObjectPosition = grabbedObject.transform.position;

                //Debug.Log("Objeto agarrado: " + grabbedObject.name);
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
            // Si el objeto tiene la etiqueta "Escoba" o "bag", lo soltamos independientemente de su posición
            if (grabbedObject.CompareTag("escoba")||grabbedObject.CompareTag("bag") && !hit.collider.CompareTag("Belt"))
            {
                if (grabbedObject.CompareTag("escoba"))
                {
                    grabbedObject.transform.position = originalGrabbedObjectPosition;
                }
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<BoxCollider>().isTrigger = false;
                grabbedObject = null;
                return;
            } 
        
            // Check if the object below is named "belt"
            if (grabbedObject.CompareTag("TrashItem")) {
                if (hit.collider.CompareTag("Belt"))
                {
                    // Object is over the belt, so release it
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                    grabbedObject.GetComponent<BoxCollider>().isTrigger = false;
                    //Debug.Log("Objeto soltado sobre la cinta: " + grabbedObject.name);
                    grabbedObject = null;
                }
                else
                {
                    
                    // Object is not over the belt, so don't release it
                    
                }
            }
        }
        
        
        
    }

    /// <summary>
    /// Calculates and sets the movement of the character.
    /// </summary>
    private void movement()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;
        Vector3 movement = Vector3.zero;
        float inputThreshold = 0.1f; // Umbral para considerar la entrada de movimiento

        // Verificar tecla "A"
        if (Input.GetKey(KeyCode.A)) { moveHorizontal = -1f; }
        // Verificar tecla "D"
        if (Input.GetKey(KeyCode.D)) { moveHorizontal = 1f; }
        // Verificar tecla "W"
        if (Input.GetKey(KeyCode.W)) { moveVertical = 1f; }
        // Verificar tecla "S"
        if (Input.GetKey(KeyCode.S)) { moveVertical = -1f; }

        /*if (playerCamera.activeSelf)
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
        }*/

        if (Mathf.Abs(moveHorizontal) < inputThreshold && Mathf.Abs(moveVertical) < inputThreshold)
        {
            animator.SetFloat("speed", 0);
            rb.velocity = Vector3.zero;
            //rb.AddForce(0, 0, 0);
        }
        else
        {

            movement = new Vector3(moveHorizontal, 0, moveVertical).normalized * speed;
            rb.velocity = movement;

            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVel, smoothRot);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            animator.SetFloat("speed", 0.2f);
        }
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
        scoreSo.Value += 100;
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (scoreSo.Value % 500 == 0)
            {
                Time.timeScale += 0.05f;
                for (int i = 0; i < belts.Count; i++)
                    belts[i].addSpeed(0.1f);
            }
        }
    }

    public float GetTotalPuntuation() {
        return scoreSo.Value;
    }

    public GameObject GetGrabbedObject() {
        if (grabbedObject != null){
        return grabbedObject.gameObject;
        } else {
            return null;
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            if (SceneManager.GetActiveScene().buildIndex == 4) {
                scoreText.text = scoreSo.Value.ToString();
            }
            else
            {
                scoreText.text = "Score\n" + scoreSo.Value.ToString();
            }
            
        else
            Debug.LogError("Score TextMeshProUGUI reference not set in the inspector.");
    }

    public void LoseLife()
    {
        
        if (lives.Lives > 1)
        {
            lives.Lives--;
            UpdateHeartsUI();
        }
        else
        {
            lives.Lives--;
            UpdateHeartsUI();
            loseCanvas.SetActive(true);
            Time.timeScale = 0;
        }

        
    }

    public void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].SetActive(i < lives.Lives);
        }
    }
}
