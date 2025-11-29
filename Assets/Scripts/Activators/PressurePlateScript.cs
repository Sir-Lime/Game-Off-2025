using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;

    public SpriteRenderer ind_sr;
    public Sprite ind_activatedSprite;
    public Sprite ind_deactivatedSprite;
    public Color ind_originalColor;

    public float resetDelay = 1.5f; 

    private bool touching = false;
    private float resetTimer = -1f;

    void Awake()
    {
        ind_originalColor = ind_sr.color;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            touching = true;
            resetTimer = resetDelay;

            if (gameObject.tag == "Deactivated")
                SFXScript.instance.pressurePlateSFX();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            touching = false;
        }
    }

    void Update()
    {
        if (touching)
        {
            ActivatePlate();
            resetTimer = resetDelay;
        }
        else
        {
            if (resetTimer > 0f)
            {
                resetTimer -= Time.deltaTime;

                if (resetTimer <= 0f)
                    DeactivatePlate();
            }
        }
    }

    void ActivatePlate()
    {
        gameObject.tag = "Activated";
        sr.sprite = activatedSprite;
        ind_sr.sprite = ind_activatedSprite;
        ind_sr.color = Color.green;
    }

    void DeactivatePlate()
    {
        gameObject.tag = "Deactivated";
        sr.sprite = deactivatedSprite;
        ind_sr.sprite = ind_deactivatedSprite;
        ind_sr.color = ind_originalColor;
    }

    public void LockActivatedState()
    {
        touching = false;
        enabled = false;

        gameObject.tag = "Activated";
        sr.sprite = activatedSprite;
        ind_sr.sprite = ind_activatedSprite;
        ind_sr.color = Color.green;
    }
}
