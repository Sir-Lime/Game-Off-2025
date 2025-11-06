using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    //public FrameInput input;
    private Rigidbody2D rb;
    private InputSystem_Actions playerInput; // InputSystem_Actions was auto-generated from the asset InputSystem_Actions as a C# class
    private float horizontal;

    [Header("Player Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Settings")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float groundDist = 5f;
    private bool isGrounded = true;

    [Header("Response Settings")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimer;
    [SerializeField] private float inputBuffer = 0.2f;
    private float bufferTimer;


    // Here I'm just creating a new instance of the InputSystem_Actions class 
    // I subscribe the Move method to when the player is using the appropiate keybinds to move
    // I subscribe an anonymous or lambda function to when the player stops moving, setting the horizontal movement to 0
    void Awake()
    {
        playerInput = new InputSystem_Actions(); 
        playerInput.Player.Move.performed += Move;
        playerInput.Player.Move.canceled += func => { horizontal = 0; };

        playerInput.Player.Jump.performed += Jump;
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

    void Update()
    {
        CollisionChecks();
        if (isGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;
    }
    // When subscribed to an action from InputSystem_Actions, it will be implicitely called with its appropiate parameters whenver the appropiate keybinds are used
    // Then we set the horizontal variable to its corresponding value (in the range [-1, 1] ofc) in the X direction
    public void Move(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<Vector2>().x;
    }
    
    public void Jump(InputAction.CallbackContext ctx)
    {
        if(coyoteTimer > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimer = 0;
        }
    }
    // This is for visual purposes (visualizing the line that determines if the player is grounded)
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundDist));
    }
    // Called every frame in the Update() method, checking if the player is colliding with a ground layer object
    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundDist, groundLayer);
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
