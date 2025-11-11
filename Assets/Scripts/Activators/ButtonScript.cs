using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private bool touching = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        touching = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        touching = false;
        }
    void Update()
    {
        if (input.actions["Interact"].WasPressedThisFrame() && touching)
        {
            if (gameObject.CompareTag("Activated")) gameObject.tag = "Deactivated";
            else if (gameObject.CompareTag("Deactivated")) gameObject.tag = "Activated";
        }
    }
}
