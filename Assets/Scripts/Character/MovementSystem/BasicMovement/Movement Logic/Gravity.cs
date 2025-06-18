using UnityEngine;

namespace MovementLogic
{
    public class Gravity
    {
        public void ApplyGravity(PlayerStateManager player)
        {
            player.VerticalVelocity.y += player.Gravity * player.FallMultiplier * Time.deltaTime;
            if (player.VerticalVelocity.y < -50f)
            {
                player.VerticalVelocity.y = -50f;
            }
        }
    }
}