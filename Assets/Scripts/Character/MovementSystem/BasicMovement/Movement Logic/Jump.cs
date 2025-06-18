using UnityEngine;

namespace MovementLogic
{
    public class Jump
    {
        public void ProceedJump(PlayerStateManager player)
        {
            player.VerticalVelocity.y = Mathf.Sqrt(player.JumpForce * -2f * player.Gravity);
            player.numberOfJumps--;
        }
    }
}