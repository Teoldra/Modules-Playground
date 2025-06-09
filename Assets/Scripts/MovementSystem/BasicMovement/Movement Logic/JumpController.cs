using System.Collections;
using UnityEngine;

public class JumpController : PlayerControllerAccess, IJumpable
{
    private bool jumpOnCooldown = false;
    private bool realeasedJumpButton = true;
    private int numberOfJumps;
    private bool pressJump;

    MovementState state;
    private Vector3 fallVelocity;

    private void OnEnable()
    {
        Player.Get.Events.State += GetMoveState;
        Player.Get.Events.FallVelocity += GetFallVelocity;
    }
    private void OnDisable()
    {
        Player.Get.Events.State -= GetMoveState;
        Player.Get.Events.FallVelocity -= GetFallVelocity;
    }

    public void WantToJump(bool pressJump)
    {
        this.pressJump = pressJump;
        if (!pressJump)
        {
            realeasedJumpButton = true;
        }
        else
        {
            Jumping();
        }
    }

    private void GetFallVelocity(Vector3 vector)
    {
        fallVelocity = vector;
    }

    private void GetMoveState(MovementState movementState)
    {
        state = movementState;
        if (state.IsGrounded())
        {
            numberOfJumps = Player.BaseNumberOfJumps;
        }
    }
    private void Jumping()
    {
        if (pressJump && numberOfJumps > 0 && !jumpOnCooldown && realeasedJumpButton && !state.IsCrouching())
        {
            numberOfJumps--;
            fallVelocity.y = Mathf.Sqrt(Player.JumpForce * -2f * Player.gravity);
            jumpOnCooldown = true;
            StartCoroutine(JumpCoolDown());
            realeasedJumpButton = false;
            Player.Get.Events.FallVelocity.Invoke(fallVelocity);
        }
    }
    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        jumpOnCooldown = false;
    }

}
