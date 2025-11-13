using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject destination;
    
    public void Interact(GameObject sender)
    {
        if(sender.CompareTag("Player"))
        {
            sender.transform.position = destination.transform.position;
        }
    }
}
