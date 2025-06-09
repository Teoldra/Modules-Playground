using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private InputReaderSO inputReader;

    private Vector2 movementDirection;
    private Vector2 mouseRotation;

    private bool isSprinting;
    private bool wantsToJump;
    private bool isInteracting;
    private bool isCrouching;

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

    private void ReadMovementInput(Vector2 direction)
    {
        movementDirection = direction;
    }
    private void ReadMouseMovement(Vector2 rotation)
    {
        mouseRotation = rotation;
    }
    private void ReadJumpInput(bool pressingJump)
    {
        wantsToJump = pressingJump;
    }
    private void ReadSprintInput(bool sprinting)
    {
        isSprinting = sprinting;
    }
    private void ReadInteractInput(bool interact)
    {
        isInteracting = interact;
    }
    private void ReadCrouchingInput(bool crouching)
    {
        isCrouching = crouching;
    }

    private void Update()
    {
        GetComponent<IMoveVelocity>()?.SetVelocity(movementDirection);
        GetComponent<ILookable>()?.MouseVelocity(mouseRotation);
        GetComponent<ISprintable>()?.IsSprinting(isSprinting);
        GetComponent<IJumpable>()?.WantToJump(wantsToJump);
        GetComponent<ICrouchable>()?.IsCrouching(isCrouching);
        GetComponent<ICanInteract>()?.IsInteracting(isInteracting);
    }
}
