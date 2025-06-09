using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MovementStateHandler : PlayerControllerAccess, ICrouchable, ISprintable
{
    private bool isGrounded;
    private bool isCeiling;
    private bool wantsToCrouch;
    private bool wantsToSprint;
    private bool isMoving;

    [SerializeField] private MovementState state;

    private void OnEnable()
    {
        Player.Get.Events.IsGrounded += GetIsGrounded;
        Player.Get.Events.IsCeiling += GetIsCeiling;
    }
    private void OnDisable()
    {
        Player.Get.Events.IsGrounded -= GetIsGrounded;
        Player.Get.Events.IsCeiling -= GetIsCeiling;
    }

    private void Update()
    {
        StateHandle();
        isMoving = Player.Get.CharacterController.velocity.magnitude != 0f;
    }

    public void GetIsGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
    public void GetIsCeiling(bool ceiling)
    {
        isCeiling = ceiling;
    }
    private void StateHandle()
    {
        var curentState = state;
        if (wantsToCrouch && isGrounded)
        {
            state = MovementState.Crouching;
        }
        else if (wantsToSprint && !wantsToCrouch && isGrounded && !isCeiling && isMoving)
        {
            state = MovementState.Sprinting;
        }
        else if (!isGrounded)
        {
            state = MovementState.Air;
        }
        else if (!wantsToCrouch && !isCeiling && isMoving)
        {
            state = MovementState.Walking;
        }
        else if (!isMoving && isGrounded)
        {
            state = MovementState.Standing;
        }
        else
        {
            state = MovementState.Standing;
        }

        if (curentState != state)
        {
            Player.Get.Events.State.Invoke(state);
        }
    }

    public void IsCrouching(bool crouching)
    {
        wantsToCrouch = crouching;
    }

    public void IsSprinting(bool pressSprint)
    {
        wantsToSprint = pressSprint;
    }
}
