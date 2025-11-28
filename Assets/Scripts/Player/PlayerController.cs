using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace playerController
{
    public class PlayerController : MonoBehaviour
    {
        #region Initial Variables
        [SerializeField] private ScriptableStats stats;
        [SerializeField] private LogicScript logic;
        [SerializeField] private PlayerInput input;
        [SerializeField] private Wave jumpWave;
        [SerializeField] private Wave dashWave;
        private Rigidbody2D playerRB;
        private CapsuleCollider2D playerCol;
        private FrameInput frameInput;
        private Vector2 frameVelocity;
        private Animator animator;
        private float time;
        private bool cachedQueryStartInColliders;
        #endregion

        #region Initialization
        private void Awake()
        {
            playerRB = GetComponent<Rigidbody2D>();
            playerCol = GetComponent<CapsuleCollider2D>();
            
            cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        #endregion

        #region Update Loops
        private void Update()
        {
            time += Time.deltaTime;
            GetInput();
            HandleDash();
            HandleAnimations();
            Debug.Log(jumpWave.GetWaveValue());
        }

        private void FixedUpdate()
        {
            CheckCollisions();
            HandleJump();
            HandleDirection();
            HandleGravity();
            ApplyMovement();
        }
        #endregion

        #region Input Handling & Animation Handling

        private void GetInput()
        {
            frameInput = new FrameInput
            {
                JumpDown = input.actions["Jump"].WasPressedThisFrame(),
                dashDown = input.actions["Dash"].WasPressedThisFrame(),
                Move = input.actions["Move"].ReadValue<Vector2>(),
            };

            if (stats.SnapInput)
            {
                frameInput.Move.x = Mathf.Abs(frameInput.Move.x) < stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(frameInput.Move.x);
                frameInput.Move.y = Mathf.Abs(frameInput.Move.y) < stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(frameInput.Move.y);
            }

            if (frameInput.JumpDown)
            {
                jumpToConsume = true;
                timeJumpWasPressed = time;
            }
        }

        private void HandleAnimations()
        {
            animator.SetFloat("xVelocity", frameInput.Move.x);
            animator.SetFloat("yVelocity", frameVelocity.y);
            animator.SetBool("isGrounded", grounded);
            animator.SetBool("isDash", isDashing);
        }

        #endregion

        #region Collisions
        
        private float frameleftgrounded = float.MinValue;
        private bool grounded;
        private float timeLastGrounded = float.MinValue;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            bool groundHit = Physics2D.CapsuleCast(playerCol.bounds.center, playerCol.size, playerCol.direction, 0, Vector2.down, stats.GrounderDistance, ~stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(playerCol.bounds.center, playerCol.size, playerCol.direction, 0, Vector2.up, stats.GrounderDistance, ~stats.PlayerLayer);

            if (ceilingHit) frameVelocity.y = Mathf.Min(0, frameVelocity.y);
            if (grounded) timeLastGrounded = time;
            if (!grounded && groundHit)
            {
                grounded = true;
                coyoteUsable = true;
                bufferedJumpUsable = true;
            }
            else if (grounded && !groundHit)
            {
                grounded = false;
                frameleftgrounded = time;
            }

            Physics2D.queriesStartInColliders = cachedQueryStartInColliders;
        }

        #endregion

        #region Horizontal

        public int facingDir = 1;
        public int FacingDir {get { return facingDir; }}

        private void HandleDirection()
        {
            if (frameInput.Move.x == 0)
            {
                var deceleration = grounded ? stats.GroundDeceleration : stats.AirDeceleration;
                frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else if (frameInput.Move.x < 0)
            {
                facingDir = -1;
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.Move.x * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
            }
            else
            {
                facingDir = 1;  
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.Move.x * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Jumping

        private bool jumpToConsume;
        private bool bufferedJumpUsable;
        private bool coyoteUsable;
        private float timeJumpWasPressed;

        private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + stats.JumpBuffer;
        private bool CanUseCoyote => coyoteUsable && !grounded && time < frameleftgrounded + stats.CoyoteTime;

        private void HandleJump()
        {
            if (!jumpToConsume && !HasBufferedJump) return;

            if (grounded || CanUseCoyote) ExecuteJump();

            jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            timeJumpWasPressed = 0;
            bufferedJumpUsable = false;
            coyoteUsable = false;
            frameVelocity.y = stats.minJumpPower + stats.maxJumpPower * jumpWave.GetWaveValue();
            timeLastGrounded = time;
        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (grounded && frameVelocity.y <= 0f)
            {
                frameVelocity.y = stats.GroundingForce;
            }
            else
            {
                frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -stats.MaxFallSpeed, stats.FallAcceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Dash

        private float timeDashPressed = float.MinValue;
        public bool isDashing = false;
        private void HandleDash()
        {
            if (frameInput.dashDown && time - timeDashPressed >= stats.DashCooldown && timeLastGrounded >= timeDashPressed)
                ExecuteDash();

            if (isDashing)
            {
                if (time - timeDashPressed >= stats.DashDuration)
                {
                    isDashing = false;
                    frameVelocity.x = 0;
                }
                else frameVelocity.y = -stats.GroundingForce;
            }
        }

        private void ExecuteDash()
        {
            if (!isDashing)
            {
                isDashing = true;
                frameVelocity.x = facingDir * (stats.minDashSpeed + stats.maxDashSpeed * dashWave.GetWaveValue());
                timeDashPressed = time;
                frameVelocity.y = -stats.GroundingForce;
            }

        }
        
        #endregion
    
        #region Movement

        private void ApplyMovement() => playerRB.linearVelocity = frameVelocity;

        #endregion
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public Vector2 Move;
        public bool dashDown;
    }
}
