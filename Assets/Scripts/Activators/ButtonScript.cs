        using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonScript : MonoBehaviour, IInteractable
{
    [SerializeField] private float activeDuration = 3f; // how long it stays active automatically
    //private bool touching = false;
    private float timer = 0f;   
    private bool isActive = false;
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    public void Awake()
    {
        originalColor = spriteRenderer.color;
    }
    public void Interact(GameObject sender)
    {
        if(sender.CompareTag("Player")) {
            if(!isActive) 
                ActivateButton();
            else
                DeactivateButton(); 
        }
    }
    void Update()
    {
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
        Debug.Log("Is Activated!");
        isActive = true;
        gameObject.tag = "Activated";
        timer = activeDuration;
        spriteRenderer.color = Color.green;
    }

    private void DeactivateButton()
    {
        isActive = false;
        gameObject.tag = "Deactivated";
        timer = 0f;
        spriteRenderer.color = originalColor;
    }
}
