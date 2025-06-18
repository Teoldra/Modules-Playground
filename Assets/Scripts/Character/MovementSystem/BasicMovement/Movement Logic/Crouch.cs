using UnityEngine;

namespace MovementLogic
{
    public class Crouch
    {
        public void Crouching(PlayerStateManager player)
        {

            player.Controller.center = new Vector3(player.BaseCharacterCenter.x, player.CrouchHeight, player.BaseCharacterCenter.z);
            player.Controller.height = (player.BaseCharacterHeight / 2);
        }

        public void StandUp(PlayerStateManager player)
        {
            player.Controller.center = player.BaseCharacterCenter;
            player.Controller.height = player.BaseCharacterHeight;
        }
    }
}