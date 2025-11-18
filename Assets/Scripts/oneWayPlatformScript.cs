using UnityEngine;
using UnityEngine.InputSystem;

public class oneWayPlatformScript : MonoBehaviour {

    private float time = 0;
    private float lastInteraction = 0;
    [SerializeField] private float countDown = 0.1f;
    [SerializeField] private PlayerInput input;
    private void Update() {
        time += Time.deltaTime;

        if ((time - lastInteraction) > countDown) {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            
        }
    }
}
