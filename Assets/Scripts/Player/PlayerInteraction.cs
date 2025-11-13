using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;

    // A circle will be casted from the center of the player to its surroundings, with a radius of interactionDistance
    // It returns an array of colliders that are encompassed within the circle
    // For each of those colliders within that circle, we will test if they're interactable
    // If so, we call their Interact function, which is provided by the IInteractable interface
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactionDistance);
            foreach (Collider2D collider in colliderArray)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(this.gameObject);
                }
            }
        }
    }
}