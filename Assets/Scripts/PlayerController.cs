    using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerController;

public class playerController : MonoBehaviour
{
#region Initial Variables

    /////////////// Components
    public Rigidbody2D playerRB;
    public ScriptableStats stats;
    public LogicScript logic;   
    private CapsuleCollider2D playerCollider;

    /////////////// Variables
    [SerializeField] private Vector2 frameVelocity;
    public FrameInput frameInput;
    public float facingDir = 1f;
    private float timeLastGrounded = float.MinValue;
    private float time;


#endregion
    
#region Initialization
    
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
    }
#endregion

#region Update Loops

    private void Update()
    {
        time += Time.deltaTime;
        if (grounded) timeLastGrounded = time;
        getInput();
        HandleDash();
    }
    private void FixedUpdate()
    {
        CheckCollisions();
        HandleJump();
        HandleDirection();
        ApplyMovement();
        HandleGravity();
    }
#endregion

#region Input Handling

    private void getInput()
    {
        frameInput = new FrameInput
        {
            jumpDown = logic.input.actions["Jump"].WasPressedThisFrame(),
            jumpHeld = logic.input.actions["Jump"].IsPressed(),
            move = logic.input.actions["Movement"].ReadValue<float>(),
            dashDown = logic.input.actions["Dash"].IsPressed(),
        };

        if (frameInput.jumpDown)
        {
            jumpToConsume = true;
            timeJumpPressed = time;
        }
    }

#endregion
    
#region Collisions

    private float frameLeftGrounded = float.MinValue;
    public bool grounded;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        bool groundHit = Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size, playerCollider.direction, 0, Vector2.down, stats.GrounderDistance, ~stats.PlayerLayer);
        groundHit = true;
        if (!grounded && groundHit)
        {
            grounded = true;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
        }
        else if (grounded && !groundHit)
        {
            grounded = false;
            frameLeftGrounded = time;
        }
    }


#endregion

#region Jumping

    private bool jumpToConsume;
    private bool bufferedJumpUsable;
    private bool endedJumpEarly;
    private bool coyoteUsable;
    private float timeJumpPressed;
    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpPressed + stats.JumpBuffer;
    private bool CanUseCoyote => coyoteUsable && !grounded && time < frameLeftGrounded + stats.CoyoteTime;

    private void HandleJump()
    {
        if (!endedJumpEarly && !grounded && !frameInput.jumpHeld && playerRB.linearVelocity.y > 0) endedJumpEarly = true;
        if (!jumpToConsume && !HasBufferedJump) return;
        if (!logic.isDashing)
        {
            if ((grounded || CanUseCoyote) && jumpToConsume) ExecuteJump();
        }

        jumpToConsume = false;
    }
    private void ExecuteJump()
    {
        Debug.Log("normal jump");
        endedJumpEarly = false;
        timeJumpPressed = 0;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = stats.JumpPower;
        timeLastGrounded = time;
    }
  
#endregion

#region Horizontal

    #region Movement
    private void HandleDirection()
    {
        if (logic.isDashing)
            return;
        if (frameInput.move == 0)
        {
            var deceleration = grounded ? stats.GroundDeceleration : stats.AirDeceleration;
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.move * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
            if (frameInput.move < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                facingDir = -1;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                facingDir = 1;
            }
        }
    }

    private void ApplyMovement()
    {
        playerRB.linearVelocity = frameVelocity;
    }
    #endregion

    #region Dash

    private float timeDashPressed = float.MinValue;
    private void HandleDash()
    {
        if (frameInput.dashDown && time - timeDashPressed >= stats.DashCooldown && timeLastGrounded >= timeDashPressed)
            ExecuteDash();

        if (logic.isDashing)
        {
            if (time - timeDashPressed >= stats.DashDuration)
            {
                Debug.Log("Stopped dashing");
                logic.isDashing = false;
                frameVelocity.x = 0;
            }
        }
    }

    private void ExecuteDash()
    {
        if (!logic.isDashing)
        {
            Debug.Log("Dash pressed");
            logic.isDashing = true;
            frameVelocity.x = stats.MaxDashSpeed * facingDir;
            timeDashPressed = time;
        }

    }
    #endregion

 #endregion

 #region Gravity

    private void HandleGravity()
    {
        if (logic.isDashing)
        {
            frameVelocity.y = 0;
            return;
        }
        if (grounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = stats.GroundingForce;
        }
        else
        {
            var inAirGravity = stats.FallAcceleration;
            if (endedJumpEarly && frameVelocity.y > 0) inAirGravity *= stats.JumpEndEarlyGravityModifier;
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

#endregion

}

public struct FrameInput
{
    public bool jumpDown;
    public bool jumpHeld;
    public bool dashDown;
    public float move;
}

