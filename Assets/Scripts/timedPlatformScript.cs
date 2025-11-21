using Unity.VisualScripting;
using UnityEngine;

public class timedPlatformScript : MonoBehaviour {

    private float time = 0;
    [SerializeField] private float offTime = 3;
    [SerializeField] private float onTime = 2;
    private float cycleTime;
    [SerializeField] private Sprite offSprite; 
    [SerializeField] private Sprite onSprite;

    void Start() {
        cycleTime = offTime + onTime; 
    }

    void Update() {
        time += Time.deltaTime;
        if ((time > cycleTime)) time -= cycleTime; 

        if (time < offTime) {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = offSprite; 
        } else {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = onSprite; 
        }
    }
}
