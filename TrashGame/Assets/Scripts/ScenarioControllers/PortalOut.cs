using UnityEngine;

public class PortalOut : MonoBehaviour
{
    [SerializeField] private CharacterScript characterScript;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TrashItem"))
        {
            Destroy(other.gameObject);

            if (characterScript != null)
            {
                characterScript.LoseLife();
            }
            else
            {
                Debug.LogError("CharacterScript reference not set in PortalOut.");
            }
        }
    }
}
