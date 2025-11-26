using UnityEngine;
using UnityEngine.InputSystem;

public class PressurePlateScript : MonoBehaviour
{
    private bool touching = false;
    public SpriteRenderer sr;
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;
    public SpriteRenderer ind_sr;
    public Sprite ind_activatedSprite;  
    public Sprite ind_deactivatedSprite;
    public Color ind_originalColor;
    void Awake()
    {
        ind_originalColor = ind_sr.color;
        
    }
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
        if (touching) 
        {
            gameObject.tag = "Activated";
            sr.sprite = activatedSprite;
            ind_sr.sprite = ind_activatedSprite;
            ind_sr.color = Color.green;
        }
    }
}
