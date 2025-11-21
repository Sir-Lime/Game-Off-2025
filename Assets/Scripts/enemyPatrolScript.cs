using UnityEngine;

public class enemyPatrolScript : MonoBehaviour {

    [SerializeField] private GameObject enemy;
    private Transform[] patrolPoints;
    private int targetPoint = 0;
    [SerializeField] private float speed = 1;

    void Start() {
        patrolPoints = new Transform[gameObject.transform.childCount - 1];
        for (int i = 0; i < patrolPoints.Length; ++i) {
            patrolPoints[i] = gameObject.transform.GetChild(i+1).gameObject.transform;
        }
    }

    void Update() {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime); 

        if (enemy.transform.position == patrolPoints[targetPoint].position) {
            ++targetPoint;
            if (targetPoint == patrolPoints.Length) targetPoint = 0;
        }
    }
}
