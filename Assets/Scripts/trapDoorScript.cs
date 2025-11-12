using UnityEngine;

public class Doo : MonoBehaviour {

    [SerializeField] private GameObject Activator;
    private BoxCollider2D col;

    void Start() {
        col = GetComponent<BoxCollider2D>();
    }

    void Update() 
    {
        if (Activator.CompareTag("Activated") ) col.enabled = false;
        else if (Activator.CompareTag("Deactivated")) col.enabled = true;
    }
}
