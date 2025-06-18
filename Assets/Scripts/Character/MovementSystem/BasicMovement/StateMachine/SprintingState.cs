using UnityEngine;

public class SprintingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.State = MovementState.Sprinting;
        player.CurrentSpeed = player.BaseMoveSpeed + player.SprintSpeedAdd;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.GravityLogic.ApplyGravity(player);
        player.MoveLogic.HandleMovement(player);
        player.RotationLogic.HandleRotation(player);

        player.Stats.UseStamina(player.StaminaCostPerSecond * Time.deltaTime);

        if (player.WantsToJump)
        {
            player.SwitchState(player.Jumping);
        }
        else if (player.WantsToCrouch)
        {
            player.SwitchState(player.Crouching);
        }
        else if (!player.IsMoving)
        {
            player.SwitchState(player.Idle);
        }
        else if (!player.WantsToSprint)
        {
            player.SwitchState(player.Walking);
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
