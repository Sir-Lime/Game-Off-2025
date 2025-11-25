using playerController;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private float xOffset = 1.25f;

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            interactionUI.SetActive(true);
        }*/
        Vector3 entityPosition = other.gameObject.transform.position;
        if(other.gameObject.CompareTag("Player")) { // Make sure it's the player interacting with this
        // Change the position of the UI depending on where the player is
            if(entityPosition.x >= transform.position.x) 
                interactionUI.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z);
            else if(entityPosition.x <= transform.position.x)
                interactionUI.transform.position =  new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z);
            interactionUI.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        interactionUI.SetActive(false);
    }
}
