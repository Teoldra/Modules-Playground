using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;

    private Vector2 movementDirection;
    private Vector2 mouseRotation;

    private bool isSprinting;
    private bool wantsToJump;
    private bool isInteracting;
    private bool isCrouching;
    private bool releasedJumpButton = true;

    private IMoveVelocityInput moveInput;
    private ILookableInput lookInput;
    private ISprintInput sprintInput;
    private IJumpInput jumpInput;
    private ICrouchInput crouchInput;
    private IInteractInput interactInput;

    private void Awake()
    {
        moveInput = GetComponent<IMoveVelocityInput>();
        lookInput = GetComponent<ILookableInput>();
        sprintInput = GetComponent<ISprintInput>();
        jumpInput = GetComponent<IJumpInput>();
        crouchInput = GetComponent<ICrouchInput>();
        interactInput = GetComponent<IInteractInput>();
    }

    private void OnEnable()
    {
        inputReader.Movement += ReadMovementInput;
        inputReader.Rotation += ReadMouseMovement;
        inputReader.Jump += ReadJumpInput;
        inputReader.Sprint += ReadSprintInput;
        inputReader.Interact += ReadInteractInput;
        inputReader.Crouching += ReadCrouchingInput;
    }

    private void OnDisable()
    {
        inputReader.Movement -= ReadMovementInput;
        inputReader.Rotation -= ReadMouseMovement;
        inputReader.Jump -= ReadJumpInput;
        inputReader.Sprint -= ReadSprintInput;
        inputReader.Interact -= ReadInteractInput;
        inputReader.Crouching -= ReadCrouchingInput;
    }

    private void ReadMovementInput(Vector2 direction) => movementDirection = direction;
    private void ReadMouseMovement(Vector2 rotation) => mouseRotation = rotation;
    private void ReadJumpInput(bool pressingJump)
    {
        wantsToJump = pressingJump;
        if (!wantsToJump) releasedJumpButton = true;
    }
    private void ReadSprintInput(bool sprinting) => isSprinting = sprinting;
    private void ReadInteractInput(bool interact) => isInteracting = interact;
    private void ReadCrouchingInput(bool crouching) => isCrouching = crouching;

    private void Update()
    {
        moveInput?.SetVelocity(movementDirection);
        lookInput?.MouseVelocity(mouseRotation);
        sprintInput?.IsSprinting(isSprinting);
        crouchInput?.IsCrouching(isCrouching);
        interactInput?.IsInteracting(isInteracting);

        HandleJumpInput();
    }

    private void HandleJumpInput()
    {
        if (releasedJumpButton && wantsToJump)
        {
            jumpInput?.WantToJump(true);
            releasedJumpButton = false;
        }
        else
        {
            jumpInput?.WantToJump(false);
        }
    }
}
