using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private float activeDuration = 3f; // how long it stays active automatically

    private bool touching = false;
    private float timer = 0f;
    private bool isActive = false;

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
        // Handle manual toggle when player presses Interact
        if (input.actions["Interact"].WasPressedThisFrame() && touching)
        {
            if (!isActive)
                ActivateButton();
            else
                DeactivateButton(); // manual deactivation
        }

        // Handle automatic deactivation after timer runs out
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                DeactivateButton();
        }
    }

    private void ActivateButton()
    {
        isActive = true;
        gameObject.tag = "Activated";
        timer = activeDuration;
    }

    private void DeactivateButton()
    {
        isActive = false;
        gameObject.tag = "Deactivated";
        timer = 0f;
    }
}
