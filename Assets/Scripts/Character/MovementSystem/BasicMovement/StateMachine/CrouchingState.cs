using UnityEngine;

public class CrouchingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.State = MovementState.Crouching;
        player.CurrentSpeed = player.BaseMoveSpeed - player.CrouchSpeedSub;
        player.CrouchLogic.Crouching(player);        
    }

    public override void UpdateState(PlayerStateManager player)
    {        
        player.GravityLogic.ApplyGravity(player);
        player.MoveLogic.HandleMovement(player);
        player.RotationLogic.HandleRotation(player);

        if (!player.WantsToCrouch && !player.IsCeiling)
        {
            player.SwitchState(player.Idle);
        }
        else if (player.IsFalling)
        {
            player.SwitchState(player.Falling);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.CrouchLogic.StandUp(player);        
    }

}
