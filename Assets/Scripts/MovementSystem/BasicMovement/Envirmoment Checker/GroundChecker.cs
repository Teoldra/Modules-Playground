using UnityEngine;

public class GroundChecker : PlayerControllerAccess
{
    private bool lastGroundedState;

    private void Update()
    {
        CheckGrounded();
    }
    private void CheckGrounded()
    {
        float raycastDistance = Player.ObjectHeight + Player.RaycastPufferRange;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        bool groundedNow = Physics.SphereCast(rayOrigin,Player.SphereRadiusGround, Vector3.down, out RaycastHit hit, raycastDistance, Player.GroundLayer);

        if (groundedNow != lastGroundedState)
        {
            lastGroundedState = groundedNow;
            Player.Get.Events.IsGrounded.Invoke(groundedNow);
        }


        Color color = groundedNow ? Color.green : Color.red;
        Debug.DrawLine(rayOrigin, rayOrigin + Vector3.down * raycastDistance, color);
        DebugExtension.DebugWireSphere(rayOrigin, color, Player.SphereRadiusGround);
        if (groundedNow)
        {
            Vector3 hitPoint = rayOrigin + Vector3.down * hit.distance;
            DebugExtension.DebugWireSphere(hitPoint, color, Player.SphereRadiusGround);
        }
    }
}
