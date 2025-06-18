using UnityEngine;

namespace MovementLogic
{
    public class Move
    {
        public void HandleMovement(PlayerStateManager player)
        {
            Vector3 moveDir = player.transform.forward * player.MovementInput.y + player.transform.right * player.MovementInput.x;
            Vector3 move = moveDir.normalized * player.CurrentSpeed;

            player.Controller.Move((move + player.VerticalVelocity) * Time.deltaTime);
        }
    }
}