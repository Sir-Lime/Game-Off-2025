using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] activators;
    [SerializeField] private GameObject destination;
    private Camera mainCamera;
    public bool isActivated;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {

        foreach (GameObject activator in activators)
        {
            if (activator.CompareTag("Activated"))
            {
                isActivated = true;
                break;
            }
            else
            {
                isActivated = false;
            }
        }
    }

    public void Interact(GameObject sender)
    {
        if(sender.CompareTag("Player") && isActivated)
        {
            sender.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            sender.transform.position = destination.transform.position;
            mainCamera.transform.position = new Vector3(sender.transform.position.x, sender.transform.position.y, mainCamera.transform.position.z);
        }
    }
}
