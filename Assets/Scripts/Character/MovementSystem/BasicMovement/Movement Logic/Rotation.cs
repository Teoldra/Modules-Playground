using UnityEngine;

namespace MovementLogic
{
    public class Rotation
    {
        private float yaw;
        private float pitch;
        public void HandleRotation(PlayerStateManager player)
        {

            yaw += player.LookInput.x * player.RotationSpeed;
            pitch -= player.LookInput.y * player.RotationSpeed;
            pitch = Mathf.Clamp(pitch, player.MaxLookUp, player.MaxLookDown);

            player.transform.rotation = Quaternion.Euler(0, yaw, 0);
            if (player.HeadPivot != null)
                player.HeadPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}