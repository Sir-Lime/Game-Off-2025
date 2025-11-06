using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    //public FrameInput input;
    private Rigidbody2D rb;
    private InputSystem_Actions playerInput; // InputSystem_Actions was auto-generated from the asset InputSystem_Actions as a C# class
    private float horizontal;
    [Header("Player Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    // Here I'm just creating a new instance of the InputSystem_Actions class 
    // I subscribe the Move method to when the player is using the appropiate keybinds to move
    // I subscribe an anonymous or lambda function to when the player stops moving, setting the horizontal movement to 0
    void Awake()
    {
        playerInput = new InputSystem_Actions(); 
        playerInput.Player.Move.performed += Move;
        playerInput.Player.Move.canceled += func => { horizontal = 0; };
    }
    // For enabling player movement (enables by default)
     void OnEnable() 
    {
        playerInput.Player.Enable();
    }
    // For disabling player movement
    void OnDisable() 
    {
        playerInput.Player.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // For physics based movement math ✌️
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
    }
    // When subscribed to an action from InputSystem_Actions, it will be implicitely called with its appropiate parameters whenver the appropiate keybinds are used
    // Then we set the horizontal variable to its corresponding value (in the range [-1, 1] ofc) in the X direction
    public void Move(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<Vector2>().x;
    }
}

   /* private void GetInput()
    {
        input = new FrameInput
        {
            movementInput = playerInput.actions["Movement"].ReadValue<float>()
        };
    }
}*/

/*public struct FrameInput
{
    public float movementInput;


}*/
