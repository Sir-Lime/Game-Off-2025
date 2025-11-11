using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private Level level;
    void Start()
    {
        level = FindFirstObjectByType<Level>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            level.KillPlayer();
        }
    }
}
