using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Activators; // Assign multiple activators here

    private BoxCollider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
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
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        }
        else
        {
            col.enabled = true;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }
}
