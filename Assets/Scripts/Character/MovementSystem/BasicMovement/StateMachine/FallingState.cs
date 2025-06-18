using UnityEngine;

public class FallingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.State = MovementState.Falling;
        player.VerticalVelocity.y = 0f;
    }
    public override void UpdateState(PlayerStateManager player)
    { 
        player.GravityLogic.ApplyGravity(player);
        player.MoveLogic.HandleMovement(player);
        player.RotationLogic.HandleRotation(player);

        if (player.Controller.velocity.y == 0)
        {
            player.VerticalVelocity.y = 0f;
        }

        if (player.WantsToJump)
        {
            player.SwitchState(player.Jumping);
        }
        else if (player.IsGrounded)
        {
            player.SwitchState(player.Idle);
        }
    }
    public override void ExitState(PlayerStateManager player)
    {
        
    }

}
