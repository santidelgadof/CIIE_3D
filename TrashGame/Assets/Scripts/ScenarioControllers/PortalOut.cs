using UnityEngine;

public class PortalOut : MonoBehaviour
{
    public GameObject[] hearts;
    public AudioClip lifeLostSound;
    private int life;
    

    private void Start()
    {
        life = hearts.Length;
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider belongs to a TrashItem
        if (other.CompareTag("TrashItem"))
        {
            // Destroy the TrashItem game object
            Destroy(other.gameObject);
            life--; 
            UpdateHearts();
            AudioSource.PlayClipAtPoint(lifeLostSound, transform.position);
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= life)
            {
                Destroy(hearts[i]);
            }
        }
    }
}
