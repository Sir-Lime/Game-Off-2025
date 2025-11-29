using playerController;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;
    private bool isTouching = false;
    private PlayerController pc;
    private bool executed = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        pc = obj.GetComponent<PlayerController>();
    }
    void Update()
    {
        if (pc.isDashing && isTouching && !executed)
        {
            if (gameObject.CompareTag("Activated")) 
            {
                gameObject.tag = "Deactivated";
                spriteRenderer.sprite = deactivatedSprite;
                SFXScript.instance.leverSFX();
            }
            else if (gameObject.CompareTag("Deactivated")) 
            {
                gameObject.tag = "Activated"; 
                spriteRenderer.sprite = activatedSprite;
                SFXScript.instance.leverSFX();
            } 
            executed = true;
        }   
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        isTouching = true;
        pc = col.GetComponent<PlayerController>();

    }   
    void OnTriggerExit2D(Collider2D col)
    {
        isTouching = false;
        executed = false;
    } 
}
