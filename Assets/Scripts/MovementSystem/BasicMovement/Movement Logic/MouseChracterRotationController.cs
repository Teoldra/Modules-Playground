using UnityEngine;

public class MouseChracterRotationController : PlayerControllerAccess, ILookable
{
    private Vector2 lookInput;
    private float yaw;
    private float pitch;
    public void MouseVelocity(Vector2 rotation)
    {
        lookInput = rotation;
        RotateView();
    }
    private void RotateView()
    {
        yaw += lookInput.x * Player.RotationSpeed;
        pitch -= lookInput.y * Player.RotationSpeed;
        pitch = Mathf.Clamp(pitch, Player.MaxLookUp, Player.MaxLookDown);

        transform.rotation = Quaternion.Euler(0, yaw, 0);
        if (Player.HeadPivot != null)
            Player.HeadPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
