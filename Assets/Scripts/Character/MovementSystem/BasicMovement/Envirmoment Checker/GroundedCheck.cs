
using UnityEngine;

public class GroundedCheck
{
    public bool CheckGrounded(PlayerStateManager player)
    {
        float raycastDistance = player.ObjectHeight + player.RaycastPufferRange;
        Vector3 rayOrigin = player.transform.position + Vector3.up * 0.1f;
        bool grounded = Physics.SphereCast(rayOrigin, player.SphereRadiusGround, Vector3.down, out RaycastHit hit, raycastDistance, player.GroundLayer);

        Color color = grounded ? Color.green : Color.red;
        Debug.DrawLine(rayOrigin, rayOrigin + Vector3.down * raycastDistance, color);
        DebugExtension.DebugWireSphere(rayOrigin, color, player.SphereRadiusGround);
        if (grounded)
        {
            Vector3 hitPoint = rayOrigin + Vector3.down * hit.distance;
            DebugExtension.DebugWireSphere(hitPoint, color, player.SphereRadiusGround);
        }
        return grounded;
    }
}
