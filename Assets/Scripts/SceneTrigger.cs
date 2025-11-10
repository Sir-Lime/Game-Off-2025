using UnityEngine;

public class SceneTrigger : MonoBehaviour
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
            Debug.Log("Player has entered!");
            level.LoadNextScene();
        }
    }
}
