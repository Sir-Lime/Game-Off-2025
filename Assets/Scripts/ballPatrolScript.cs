using UnityEngine;

public class ballPatrolScript : MonoBehaviour {

    [SerializeField] private GameObject Ball;
    private Transform[] patrolPoints;
    private int targetPoint = 0;

    [SerializeField] private float travelTime = 3.0f; 
    private float startTime;
    private Vector3 startPosition;

    void Start() {
        patrolPoints = new Transform[gameObject.transform.childCount - 1];
        for (int i = 0; i < patrolPoints.Length; ++i) {
            patrolPoints[i] = gameObject.transform.GetChild(i+1).gameObject.transform;
        }

        startPosition = Ball.transform.position;
        startTime = Time.time;
    }

    void Update() {
        float timeElapsed = Time.time - startTime;
        float rawT = timeElapsed / travelTime;

        float smoothT = Mathf.SmoothStep(0.0f, 1.0f, rawT);

        Ball.transform.position = Vector3.Lerp(startPosition, patrolPoints[targetPoint].position, smoothT); 

        if (rawT >= 1.0f) {
            Ball.transform.position = patrolPoints[targetPoint].position;
            ++targetPoint;

            if (targetPoint == patrolPoints.Length) targetPoint = 0;
            startPosition = Ball.transform.position;
            startTime = Time.time;
        }
    }
}