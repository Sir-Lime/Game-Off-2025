using UnityEngine;
using UnityEngine.InputSystem;

public class oneWayPlatformScript : MonoBehaviour {

    private float time = 0;
    private float lastInteraction = 0;
    [SerializeField] private float countDown = 1f;
    [SerializeField] private PlayerInput input;
    private void Update() {
        time += Time.deltaTime;

        if ((time - lastInteraction) > countDown) {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("collision?");
            if (input.actions["Down"].IsPressed()) {
                Debug.Log("It works!!!");
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                lastInteraction = time;
            }
        }
    }
}
