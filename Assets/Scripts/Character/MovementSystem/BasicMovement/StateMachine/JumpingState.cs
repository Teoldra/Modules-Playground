using UnityEngine;

public class JumpingState : PlayerBaseState
{
    private float groundedCoolDown = 0.1f;
    public override void EnterState(PlayerStateManager player)
    {
        groundedCoolDown = 0.1f;
        player.State = MovementState.Jumping;
        player.JumpLogic.ProceedJump(player);
        player.Stats.UseStamina(player.StaminaJumpCosts);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.GravityLogic.ApplyGravity(player);
        player.MoveLogic.HandleMovement(player);
        player.RotationLogic.HandleRotation(player);

        groundedCoolDown -= Time.deltaTime;
        

        if (player.WantsToJump)
        {
            player.JumpLogic.ProceedJump(player);
            player.Stats.UseStamina(player.StaminaJumpCosts);
        }

        if (player.IsFalling)
        {
            player.SwitchState(player.Falling);
        }
        else if( player.IsGrounded && groundedCoolDown <= 0f)
        {
            player.SwitchState(player.Idle);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

}
