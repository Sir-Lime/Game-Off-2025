using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] activators;
    [SerializeField] private GameObject destination;
    private Camera mainCamera;
    public bool isActivated;
    public GameObject portal;
    private bool playedSFX = false;

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
                portal.SetActive(true);
                gameObject.tag = "Activated"; 
                if (!playedSFX) {
                    SFXScript.instance.portalSFX();
                    playedSFX = true;
                }
                break;
            }
            else
            {
                isActivated = false;
                portal.SetActive(false);
                gameObject.tag = "Deactivated";
                playedSFX = false;
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
