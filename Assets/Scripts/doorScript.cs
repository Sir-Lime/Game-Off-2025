using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class doorScript : MonoBehaviour {

    [SerializeField] private bool bo = false; // @lime feel free to delete it when you integrate your activators thingy
    private BoxCollider2D collider;
    private float time = 0;
    [SerializeField] private float countDown = 3;
    private float lastActivated;

    void Start() {
        collider = GetComponent<BoxCollider2D>();
        lastActivated = -countDown;
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        if (bo && (time - lastActivated) >= countDown) {
            lastActivated = time;
            if (collider.isTrigger) collider.isTrigger = false;
            else collider.isTrigger = true;
        }
    }
}
