using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Activators; // Assign multiple activators here

    private BoxCollider2D col;
    private SpriteRenderer sr;
    private Animator animator;
    private bool playedSFX = false;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool anyActivated = false;

        foreach (GameObject activator in Activators)
        {
            if (activator.CompareTag("Activated"))
            {
                anyActivated = true;
                break;
            }
        }

        if (anyActivated)
        {
            col.enabled = false;
            animator.SetBool("isActivated", true);
            if (!playedSFX) {
                SFXScript.instance.openDoorSFX();   
                playedSFX = true;
            }
        }
        else
        {
            col.enabled = true;
            animator.SetBool("isActivated", false);
            if (playedSFX) {
                SFXScript.instance.closeDoorSFX();
                playedSFX = false;
            }
        }
    }
}
