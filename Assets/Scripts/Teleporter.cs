using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject destination;
    [SerializeField] private float teleportCooldown;
    private float teleportTimer;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        teleportTimer -= Time.deltaTime;
    }

    public void Interact(GameObject sender)
    {
        if(sender.CompareTag("Player") && teleportTimer <= 0)
        {
            sender.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            sender.transform.position = destination.transform.position;
            mainCamera.transform.position = new Vector3(sender.transform.position.x, sender.transform.position.y, mainCamera.transform.position.z);

            teleportTimer = teleportCooldown;
        }
    }
}
