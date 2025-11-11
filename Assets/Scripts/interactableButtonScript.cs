using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactableButtonScript : MonoBehaviour
{
    [SerializeField] private LogicScript logic;
    [SerializeField] private PlayerInput input;
    [SerializeField] private int buttonID; 

    void Start() {
    }

    void Update() {
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (input.actions["Interactable Button Press"].WasPressedThisFrame()) {
                Debug.Log("Interactable Button Pressed");
                logic.ineractableObjects[buttonID] = true;
            }
        }
    }
}
