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
        private Rigidbody2D playerRB;
        private CapsuleCollider2D playerCol;
        private FrameInput frameInput;
        private Vector2 frameVelocity;
        private float time;
        private bool cachedQueryStartInColliders;
        #endregion

        #region Interface

        public Vector2 FrameInput => frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

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
        }
        #endregion

        #region Update Loops
        private void Update()
        {
            time += Time.deltaTime;
            GetInput();
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

        #region Input Handling

        private void GetInput()
        {
            frameInput = new FrameInput
            {
                JumpDown = input.actions["Jump"].WasPressedThisFrame(),
                JumpHeld = input.actions["Jump"].IsPressed(),
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

        #endregion

        #region Collisions
        
        private float frameleftgrounded = float.MinValue;
        private bool grounded;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            bool groundHit = Physics2D.CapsuleCast(playerCol.bounds.center, playerCol.size, playerCol.direction, 0, Vector2.down, stats.GrounderDistance, ~stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(playerCol.bounds.center, playerCol.size, playerCol.direction, 0, Vector2.up, stats.GrounderDistance, ~stats.PlayerLayer);

            if (ceilingHit) frameVelocity.y = Mathf.Min(0, frameVelocity.y);

            if (!grounded && groundHit)
            {
                grounded = true;
                coyoteUsable = true;
                bufferedJumpUsable = true;
                endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(frameVelocity.y));
            }
            // Left the Ground
            else if (grounded && !groundHit)
            {
                grounded = false;
                frameleftgrounded = time;
                GroundedChanged?.Invoke(false, 0);
            }

                Physics2D.queriesStartInColliders = cachedQueryStartInColliders;
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (frameInput.Move.x == 0)
            {
                var deceleration = grounded ? stats.GroundDeceleration : stats.AirDeceleration;
                frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.Move.x * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Jumping

        private bool jumpToConsume;
        private bool bufferedJumpUsable;
        private bool endedJumpEarly;
        private bool coyoteUsable;
        private float timeJumpWasPressed;

        private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + stats.JumpBuffer;
        private bool CanUseCoyote => coyoteUsable && !grounded && time < frameleftgrounded + stats.CoyoteTime;

        private void HandleJump()
        {
            if (!endedJumpEarly && !grounded && !frameInput.JumpHeld && playerRB.linearVelocity.y > 0) endedJumpEarly = true;

            if (!jumpToConsume && !HasBufferedJump) return;

            if (grounded || CanUseCoyote) ExecuteJump();

            jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            endedJumpEarly = false;
            timeJumpWasPressed = 0;
            bufferedJumpUsable = false;
            coyoteUsable = false;
            frameVelocity.y = stats.JumpPower;
            Jumped?.Invoke();
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
                var inAirGravity = stats.FallAcceleration;
                if (endedJumpEarly && frameVelocity.y > 0) inAirGravity *= stats.JumpEndEarlyGravityModifier;
                frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
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
        public bool JumpHeld;
        public Vector2 Move;
    }

}