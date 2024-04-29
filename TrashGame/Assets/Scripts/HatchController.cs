using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public AudioSource correctSong;
    private bool isOpen = false;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    
    public float rotationAngle = 60f;
    public float movementOffset = 0.5f;
     private CharacterScript player;
    void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        player = GameObject.Find("Player").GetComponent<CharacterScript>();
    }

    
    void Update()
    {
        UpdatePosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("TrashBag") || other.CompareTag("bag"))
        {
            correctSong.volume = 1;
            correctSong.Play();
            isOpen = true;
            Destroy(other.gameObject);
            player.Point();
            StartCoroutine(ResetIsOpenAfterDelay(1.5f)); 
            
        }

        
    }

    /// <summary>
    ///  Changes the state of the hatch with a delay
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator ResetIsOpenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isOpen = false; // After waiting for 1 second, set isOpen back to false
    }

    /// <summary>
    /// Updates the position and rotation of the hatch.
    /// </summary>
    private void UpdatePosition() {
        if (isOpen) {
            // Rotate the hatch 
            transform.rotation = Quaternion.Euler(defaultRotation.eulerAngles + new Vector3(0, 0, rotationAngle));

            // Move the hatch on the x axis
            transform.position = defaultPosition + new Vector3(-1.05f, 0f, 0f);;
        } else {
            // Return to the default position and rotation
            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
        }
    }
}
