using playerController;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController controller = col.GetComponent<PlayerController>();
        if (controller.isDashing)
        {
            if (gameObject.CompareTag("Activated")) gameObject.tag = "Deactivated";
            else if (gameObject.CompareTag("Deactivated")) gameObject.tag = "Activated";   
        }
        
    }   
}
