
public class WalkingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.State = MovementState.Walking;
        player.CurrentSpeed = player.BaseMoveSpeed;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.GravityLogic.ApplyGravity(player);
        player.MoveLogic.HandleMovement(player);
        player.RotationLogic.HandleRotation(player);

        if (player.WantsToJump)
        {
            player.SwitchState(player.Jumping);
        }
        else if (player.WantsToCrouch)
        {
            player.SwitchState(player.Crouching);
        }
        else if (player.WantsToSprint)
        {
            player.SwitchState(player.Sprinting);
        }
        else if (!player.IsMoving)
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
        
    }

}
