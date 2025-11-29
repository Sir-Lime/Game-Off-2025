using UnityEngine;

public class Doo : MonoBehaviour {

    [SerializeField] private GameObject Activator;
    private BoxCollider2D col;
    private bool playedSFX = false;

    void Start() {
        col = GetComponent<BoxCollider2D>();
    }

    void Update() 
    {
        if (Activator.CompareTag("Activated") ) 
        {
            col.enabled = false; 
            if (!playedSFX) {
                SFXScript.instance.openDoorSFX();
                playedSFX = true;
            }
        }
        else if (Activator.CompareTag("Deactivated")) {
            col.enabled = true; 
            if (playedSFX) {
                SFXScript.instance.closeDoorSFX();
                playedSFX = false;
            }
        }
    }
}
