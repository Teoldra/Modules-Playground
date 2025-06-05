using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public partial class CharController : MonoBehaviour, IMoveVelocity, ILookable, ISprintable, IJumpable, ICrouchable
{
    [Header("Movement Settings")]
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private MovementState state;
    [SerializeField] private Vector3 baseCharacterCenter;
    [SerializeField] private float baseCharacterHeight;
    private CharacterController characterController;
    private Vector3 velocity;
    private Vector2 movementInput;
    private Vector2 lookInput;
    private float yaw;
    private float pitch;
    private bool isSprinting;


    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private bool isCeiling;
    [SerializeField] private LayerMask ceilingLayer;
    float raycastDistanceCrouching;
    float crouchRadius;
    float rayBegin;
    private bool isCrouching;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int baseNumberOfJumps = 1;
    private bool jumpOnCooldown = false;
    private bool realeasedJumpButton = true;
    private int numberOfJumps;
    private bool pressJump;

    [Header("Camera Settings")]
    [SerializeField] private Transform headPivot; // Camera Look At
    [SerializeField] private float maxLookUp = -60f;
    [SerializeField] private float maxLookDown = 40f;
    [SerializeField] private float rotationSpeed = 2f;

    [Header("Physics Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float fallMultiplier = 2.5f;
    private bool isGrounded;

    [SerializeField] private BoolGameEvent onGroundedChangedEvent;
    [SerializeField] private BoolGameEvent onCeilingChangedEvent;
    private void OnEnable()
    {
        onGroundedChangedEvent.RegisterListener(GetIsGrounded);
        onCeilingChangedEvent.RegisterListener(GetIsCeiling);
    }

    private void OnDisable()
    {
        onGroundedChangedEvent.UnregisterListener(GetIsGrounded);
        onCeilingChangedEvent.UnregisterListener(GetIsCeiling);
    }

    public void GetIsGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
    public void GetIsCeiling(bool ceiling)
    {
        isCeiling = ceiling;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        baseCharacterCenter = characterController.center;
        baseCharacterHeight = characterController.height;
        Cursor.lockState = CursorLockMode.Locked;

        raycastDistanceCrouching = (characterController.height / 2);
        crouchRadius = characterController.radius;
        rayBegin = -0.5f;
    }

    private void Update()
    {
        CharacterMovement();
        RotateView();
        StateHandle();
        SpeedControl(state);
        CrouchingPlayerHeight();
        ApplyGravity();
        Jumping();
    }

    #region Interface Methods
    public void SetVelocity(Vector2 input)
    {
        movementInput = input;
    }

    public void MouseVelocity(Vector2 rotation)
    {
        lookInput = rotation;
    }

    public void IsSprinting(bool pressSprint)
    {
        isSprinting = pressSprint;
    }

    public void WantToJump(bool pressJump)
    {
        this.pressJump = pressJump;
        if (!pressJump)
        {
            realeasedJumpButton = true;
        }
    }

    public void IsCrouching(bool crouching)
    {
        isCrouching = crouching;
    }

    #endregion
    private void CharacterMovement()
    {
        Vector3 moveDir = transform.forward * movementInput.y + transform.right * movementInput.x;
        Vector3 move = moveDir.normalized * currentSpeed;

        characterController.Move((move + velocity) * Time.deltaTime);
    }
    private void RotateView()
    {
        yaw += lookInput.x * rotationSpeed;
        pitch -= lookInput.y * rotationSpeed;
        pitch = Mathf.Clamp(pitch, maxLookUp, maxLookDown);

        transform.rotation = Quaternion.Euler(0, yaw, 0);
        if (headPivot != null)
            headPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
    private void SpeedControl(MovementState state)
    {
        switch (state)
        {
            case MovementState.Walking: currentSpeed = baseMoveSpeed; break;
            case MovementState.Sprinting: currentSpeed = baseMoveSpeed + sprintSpeed; break;
            case MovementState.Crouching: currentSpeed = baseMoveSpeed - crouchSpeed; break;
            case MovementState.Air: break;
            default: currentSpeed = baseMoveSpeed; break;
        }
    }
    private void StateHandle()
    {
        if (isCrouching && isGrounded)
        {
            state = MovementState.Crouching;
        }
        else if (isSprinting && !isCrouching && isGrounded && !isCeiling)
        {
            state = MovementState.Sprinting;
        }
        else if (!isGrounded && !isCeiling)
        {
            state = MovementState.Air;
        }
        else if(!isCrouching && !isCeiling)
        {
            state = MovementState.Walking;
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            numberOfJumps = baseNumberOfJumps;
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
    }
    private void Jumping()
    {
        if (pressJump && numberOfJumps > 0 && !jumpOnCooldown && realeasedJumpButton && state != MovementState.Crouching)
        {
            isGrounded = false;
            numberOfJumps--;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpOnCooldown = true;
            StartCoroutine(JumpCoolDown());
            realeasedJumpButton = false;
        }
    }
    private void CrouchingPlayerHeight()
    {
        if (state == MovementState.Crouching)
        {
            characterController.center = new Vector3(baseCharacterCenter.x, -0.5f, baseCharacterCenter.z);
            characterController.height = (baseCharacterHeight / 2);
        }
        else
        {
            characterController.center = baseCharacterCenter;
            characterController.height = baseCharacterHeight;
        }
    }
    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        jumpOnCooldown = false;
    }
}
