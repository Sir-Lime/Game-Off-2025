using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public FrameInput input;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        getInput();
        float movement = input.movementInput;
        Debug.Log("Movement Input: " + movement);
    }

    private void getInput()
    {
        input = new FrameInput
        {
            movementInput = playerInput.actions["Movement"].ReadValue<float>()
        };
    }
}

public struct FrameInput
{
    public float movementInput;
}
