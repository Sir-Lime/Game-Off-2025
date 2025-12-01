using NUnit.Framework.Constraints;
using UnityEngine;

public class Laser : MonoBehaviour
{
   [SerializeField] GameObject[] activators;
   private bool isActivated = true;
   private bool isFiring = true;
   private Level levelManager;
   private BoxCollider2D boxCol;
   private SpriteRenderer sprite;

    void Start()
    {
        levelManager = FindAnyObjectByType<Level>();
        boxCol = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(activators.Length != 0) {
            foreach (GameObject activator in activators)
            {
                if (activator.CompareTag("Deactivated"))
                {
                    isActivated = false;
                    break;
                }
                if(activator.CompareTag("Activated")) 
                {
                    isActivated = true;
                }
            }
        }

        if(isActivated) {
            sprite.enabled = true;
            boxCol.enabled = true;

            if(isFiring)
                transform.localScale += new Vector3(0f, 0.1f, 0f);
            else if(!isFiring)
                transform.localScale -= new Vector3(0f, 0.1f, 0f);
        }
        else
        {
            sprite.enabled = false;
            boxCol.enabled = false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isFiring = false;
        if(collision.gameObject.CompareTag("Player"))
        {
            levelManager.KillPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isFiring = true;   
    }
    
}
