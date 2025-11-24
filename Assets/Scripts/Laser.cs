using NUnit.Framework.Constraints;
using UnityEngine;

public class Laser : MonoBehaviour
{
   [SerializeField] private float defaultDistance = 100f;
   private bool isActivated = true;
   private bool isFiring = true;
   private Level levelManager;

    void Start()
    {
        levelManager = FindAnyObjectByType<Level>();
    }

    void Update()
    {
        if(isActivated) {
            if(isFiring)
                transform.localScale += new Vector3(0f, 0.1f, 0f);
            else
                transform.localScale -= new Vector3(0f, 0.1f, 0f);
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
