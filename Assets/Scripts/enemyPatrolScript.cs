using UnityEngine;

public class enemyPatrolScript : MonoBehaviour {

    [SerializeField] private Transform[] patrolPoint;
    private int targetPoint = 0;
    [SerializeField] private float speed = 1;
    private Level level;

    void Start() {
        level = FindFirstObjectByType<Level>();
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoint[targetPoint].position, speed * Time.deltaTime); 

        if (transform.position == patrolPoint[targetPoint].position) {
            ++targetPoint;
            if (targetPoint == patrolPoint.Length) targetPoint = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            level.KillPlayer();
        }
    }
}
