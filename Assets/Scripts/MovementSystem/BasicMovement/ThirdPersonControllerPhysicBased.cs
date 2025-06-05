using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonControllerPhysicBased : MonoBehaviour, IMoveVelocity, ILookable, ISprintable, IJumpable
{
    [Header("MovmentSettings")]
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private int baseNumberOfJumps = 1;

    [Header("CamSettings")]
    [SerializeField] private Transform headPivot;
    [SerializeField] private float maxLookUp = -60f;
    [SerializeField] private float maxLookDown = 40f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("PhysicSettings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float ascendMultiplier = 2f;

    private Rigidbody rb;
    private Vector2 movementInput;
    private Vector2 lookInput;
    private float yaw = 0f;
    private float pitch = 0f;
    private float moveSpeed = 5f;
    private float playerHeight;
    private float raycastDistance;

    private int numberOfJumps;

    private bool isSprinting;
    private bool pressJump;
    private bool jumpOnCoolDown = false;
    private bool jumpedRecenty = false;

    private void Awake()
    {
        numberOfJumps = baseNumberOfJumps;
        rb = GetComponent<Rigidbody>();
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        SpeedControl();
    }

    private void FixedUpdate()
    {
        Movement();
        JumpSetUp();
    }

    #region Interfaces
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
    }
    #endregion

    #region Movement
    private void Movement()
    {
        SetMovespeed();
        RotateHeadUpDown();
        MoveCharacterTowardsCamera();
    }
    private void MoveCharacterTowardsCamera()
    {
        yaw += lookInput.x * rotationSpeed * Time.fixedDeltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, yaw, 0);
        Vector3 forward = targetRotation * Vector3.forward;
        Vector3 right = targetRotation * Vector3.right;
        Vector3 moveDir = forward * movementInput.y + right * movementInput.x;
        rb.MoveRotation(targetRotation);

        if (moveDir.magnitude > 0.05f)
        {
            rb.AddForce(10f * moveSpeed * moveDir.normalized, ForceMode.Force);
        }
        else if (isGrounded && movementInput.magnitude < 0.1f)
        {
            Vector3 vel = rb.linearVelocity;
            rb.linearVelocity = new Vector3(0f, vel.y, 0f);
        }

        lookInput = Vector2.zero;
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
    private void RotateHeadUpDown()
    {
        pitch -= lookInput.y * rotationSpeed * Time.fixedDeltaTime;
        pitch = Mathf.Clamp(pitch, maxLookUp, maxLookDown);

        if (headPivot != null)
        {
            headPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
    private void SetMovespeed()
    {
        if (isSprinting)
        {
            moveSpeed = baseMoveSpeed + sprintSpeed;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
    }
    #endregion

    #region JumpPhysics
    private void JumpSetUp()
    {
        IsGrounded();
        Jump();
        RestNumberOfJumps();
        ApplyJumpPhysics();
    }
    private void Jump()
    {
        if (numberOfJumps != 0 && pressJump && !jumpOnCoolDown)
        {
            jumpedRecenty = true;
            StartCoroutine(GroundedCoolDown());
            isGrounded = false;
            numberOfJumps--;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpOnCoolDown = true;
            StartCoroutine(JumpCoolDown());
        }
    }
    private void IsGrounded()
    {
        if (!jumpedRecenty)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
    }
    private void RestNumberOfJumps()
    {
        if (isGrounded)
        {
            numberOfJumps = baseNumberOfJumps;
        }
    }
    private void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0.5f)
        {
            // Falling: Apply fall multiplier to make descent faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.linearVelocity.y > 0.5f)
        {
            // Rising: Change multiplier to make player reach peak of jump faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }
    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        jumpOnCoolDown = false;
    }
    private IEnumerator GroundedCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        jumpedRecenty = false;
    }
    #endregion


}
