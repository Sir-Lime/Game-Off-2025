        using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonScript : MonoBehaviour, IInteractable
{
    [SerializeField] private float activeDuration = 3f;
    [SerializeField] private float deactiveDuration = 3f; 
    [SerializeField] private bool isActive = false;
    [Header("Auto Activation/Deactivation")]
    [Tooltip("One should be true and the other should be false. Both can be false but not true. If both are true, then auto deactivation is prioritized.")]
    [SerializeField] private bool autoDeactivation = true;
    [Tooltip("One should be true and the other should be false. Both can be false but not true. If both are true, then auto deactivation is prioritized.")]
    [SerializeField] private bool autoActivation = false;
    //private bool touching = false;
    private float timer = 0f;   
    public SpriteRenderer spriteRenderer;
    private Color AColor; 
    private Color DColor; 

    public void Awake()
    {
        AColor = Color.green;
        DColor = Color.red;

        if(isActive)
            spriteRenderer.color = AColor;
        else
            spriteRenderer.color = DColor;
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
        if (isActive && autoDeactivation)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                DeactivateButton();
        }

        if(!isActive && autoActivation)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
                ActivateButton();
        }
    }

    private void ActivateButton()
    {
        Debug.Log("Is Activated!");
        isActive = true;
        gameObject.tag = "Activated";
        timer = activeDuration;
        spriteRenderer.color = AColor;
        SFXScript.instance.buttonSFX();
    }

    private void DeactivateButton()
    {
        isActive = false;
        gameObject.tag = "Deactivated";
        timer = autoActivation ? deactiveDuration : 0f;
        spriteRenderer.color = DColor;
        SFXScript.instance.buttonSFX();
    }
}
