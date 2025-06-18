using UnityEngine;

public class IdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.State = MovementState.Idle;
        player.CurrentSpeed = player.BaseMoveSpeed;
        player.numberOfJumps = player.BaseNumberOfJumps;
    }

    public override void UpdateState(PlayerStateManager player)
    {        
        player.GravityLogic.ApplyGravity(player);
        player.MoveLogic.HandleMovement(player);
        player.RotationLogic.HandleRotation(player);

        if (player.IsMoving)
        {
            player.SwitchState(player.Walking);
        }
        else if (player.WantsToJump)
        {
            player.SwitchState(player.Jumping);
        }
        else if (player.WantsToCrouch)
        {
            player.SwitchState(player.Crouching);
        }
        else if (player.IsFalling)
        {
            player.SwitchState(player.Falling);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        
    }

}
