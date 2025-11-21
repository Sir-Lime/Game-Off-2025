using UnityEngine;
using UnityEngine.InputSystem;

public class PressurePlateScript : MonoBehaviour
{
    private bool touching = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        touching = true;    
    }   
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        touching = false;
    }
    void Update()
    {
        if (touching) gameObject.tag = "Activated";
        else gameObject.tag = "Deactivated";
    }
}
