using UnityEngine;

public class enemyPatrolScript : MonoBehaviour {

    [SerializeField] private Transform[] patrolPoint;
    private int targetPoint = 0;
    [SerializeField] private float speed = 1;

    void Start()
    {
        
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoint[targetPoint].position, speed * Time.deltaTime); 

        if (transform.position == patrolPoint[targetPoint].position) {
            ++targetPoint;
            if (targetPoint == patrolPoint.Length) targetPoint = 0;
        }
    }
}
