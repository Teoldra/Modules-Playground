using UnityEngine;
using MovementLogic;

public class PlayerStateManager : MonoBehaviour, ICrouchInput, ISprintInput, IJumpInput, IMoveVelocityInput, ILookableInput
{

    public PlayerStats Stats;


    [Header("Movement Settings")]
    public MovementState State;
    public float BaseMoveSpeed = 5f; // Base speed of the player
    public float SprintSpeedAdd = 2f; // Speed increase when sprinting
    public float CrouchSpeedSub = 2f; // Speed reduction when crouching
    public float StaminaCostPerSecond = 5f; // Stamina cost per second when sprinting
    [HideInInspector] public float CurrentSpeed; // Current speed of the player, modified by sprinting or crouching
    [HideInInspector] public Vector3 VerticalVelocity;
    [HideInInspector] public Vector2 MovementInput;
    [HideInInspector] public Vector2 LookInput;

    [Header("Camera Settings")]
    public Transform HeadPivot; // Camera Look At
    public float MaxLookUp = -60f; // Maximum camera look angles
    public float MaxLookDown = 40f; // Maximum camera look angles
    public float RotationSpeed = 2f; // Speed of camera rotation
    public float CamTransitionSpeed = 10f; // Speed of camera transition between standing and crouching positions
    public Vector3 CamStandPos = new(0f, 0.75f, 0f); // Camera position when standing
    public Vector3 CamCrouchPos = new(0f, 0f, 0f); // Camera position when crouching

    [Header("Jump Settings")]
    public float JumpForce = 5f; // Force applied when jumping
    public int BaseNumberOfJumps = 1; // Base number of jumps available
    public float StaminaJumpCosts = 10f; // Stamina cost for each jump
    [HideInInspector] public int numberOfJumps;

    [Header("Crouch Settings")]
    public float CrouchHeight = -0.5f; // Height adjustment when crouching
    [HideInInspector] public float BaseCharacterHeight;
    [HideInInspector] public Vector3 BaseCharacterCenter;


    [Header("Ground Check Settings")]
    public LayerMask GroundLayer; // Layer mask for ground detection
    public float RaycastPufferRange = 0.5f; // Additional distance for raycast checks
    public float ObjectHeight = 1f; // Height of the player object
    public float SphereRadiusGround = 0.5f; // Radius for ground checks using sphere cast

    [Header("Ceiling Check Settings")]
    public LayerMask CeilingLayer; // Layer mask for ceiling detection
    public float CeilingObjectHeight = 1f; // Height of the player object for ceiling checks
    public float SphereRadiusCeiling = 0.5f; // Radius for ceiling checks using sphere cast

    [Header("Gravity Settings")]
    public float Gravity = -9.81f; // Gravity force applied to the player
    public float FallMultiplier = 2.5f; // Multiplier for fall speed

    [HideInInspector] public CharacterController Controller; // Reference to the CharacterController component
    public bool IsGrounded { get; private set; }
    public bool IsCeiling { get; private set; }
    public bool WantsToCrouch { get; private set; }
    public bool WantsToSprint { get; private set; }
    public bool WantsToJump { get; private set; }
    public bool IsMoving => MovementInput.magnitude > 0.1f;
    public bool IsFalling => VerticalVelocity.y < 0f && !IsGrounded;

    PlayerBaseState currentState;
    public IdleState Idle = new();
    public WalkingState Walking = new();
    public SprintingState Sprinting = new();
    public JumpingState Jumping = new();
    public FallingState Falling = new();
    public CrouchingState Crouching = new();

    public Move MoveLogic = new();
    public Rotation RotationLogic = new();
    public Jump JumpLogic = new();
    public Crouch CrouchLogic = new();
    public Gravity GravityLogic = new();
    private readonly GroundedCheck groundedChecker = new();
    private readonly CeilingCheck ceilingChecker = new();
    public void SwitchState(PlayerBaseState player)
    {
        ExitState();
        currentState = player;
        player.EnterState(this);
    }

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        InitialStartValues();

        currentState.EnterState(this);
    }

    private void Update()
    {
        UpdateCheckerLogic();
        CamHandling();
        currentState.UpdateState(this);
    }
    private void ExitState()
    {
        currentState.ExitState(this);
    }
    private void UpdateCheckerLogic()
    {
        IsGrounded = groundedChecker.CheckGrounded(this);
        IsCeiling = ceilingChecker.CheckCeiling(this);
    }
    private void InitialStartValues()
    {
        currentState = Idle;
        BaseCharacterHeight = Controller.height;
        BaseCharacterCenter = Controller.center;
        CurrentSpeed = BaseMoveSpeed;
        numberOfJumps = BaseNumberOfJumps;
        WantsToCrouch = false;
        WantsToSprint = false;
        WantsToJump = false;
    }
    private void CamHandling()
    {
        if (currentState == Crouching)
        {
            HeadPivot.localPosition = Vector3.Lerp(HeadPivot.localPosition, CamCrouchPos, CamTransitionSpeed * Time.deltaTime);
        }
        else
        {
            HeadPivot.localPosition = Vector3.Lerp(HeadPivot.localPosition, CamStandPos, CamTransitionSpeed * Time.deltaTime);
        }
    }

    #region Interfaces Implementation
    public void IsCrouching(bool crouching)
    {
        WantsToCrouch = crouching;
    }

    public void IsSprinting(bool pressSprint)
    {
        if (Stats.CanUseStamina())
        {
            WantsToSprint = pressSprint;
        }
        else
        {
            WantsToSprint = false;
        }
    }

    public void WantToJump(bool pressJump)
    {
        if (numberOfJumps > 0 && Stats.CanUseStamina(10f))
        {
            WantsToJump = pressJump;
        }
        else
        {
            WantsToJump = false;
        }
    }

    public void SetVelocity(Vector2 direction)
    {
        MovementInput = direction;
    }

    public void MouseVelocity(Vector2 rotation)
    {
        LookInput = rotation;
    }
    #endregion


}

