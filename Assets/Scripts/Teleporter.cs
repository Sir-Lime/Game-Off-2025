using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject destination;
    [SerializeField] private float teleportCooldown;
    private float teleportTimer;

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

            teleportTimer = teleportCooldown;
        }
    }
}
