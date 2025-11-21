using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class oneWayPlatformScript : MonoBehaviour {

    private float time = 0;
    private float lastInteraction = 0;
    [SerializeField] private float countDown = 1f;
    [SerializeField] private PlayerInput input;
    private void Update() {
        time += Time.deltaTime;

        if ((time - lastInteraction) > countDown) {
            gameObject.GetComponent<TilemapCollider2D>().enabled = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("collision?");
            if (input.actions["Down"].IsPressed()) {
                Debug.Log("It works!!!");
                gameObject.GetComponent<TilemapCollider2D>().enabled = false;
                lastInteraction = time;
            }
        }
    }
}
