using playerController;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController controller = col.GetComponent<PlayerController>();
        if (controller.isDashing)
        {
            if (gameObject.CompareTag("Activated")) 
            {
                gameObject.tag = "Deactivated";
                spriteRenderer.sprite = deactivatedSprite;
            }
            else if (gameObject.CompareTag("Deactivated")) 
            {
                gameObject.tag = "Activated"; 
                spriteRenderer.sprite = activatedSprite;
            }   
        }
        
    }   
}
