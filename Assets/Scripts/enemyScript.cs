using UnityEngine;

public class enemyScript : MonoBehaviour
{

    private Level level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = FindFirstObjectByType<Level>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            level.KillPlayer();
        }
    }
}
