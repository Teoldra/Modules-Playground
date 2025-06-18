
using UnityEngine;

public class CeilingCheck
{
    public bool CheckCeiling(PlayerStateManager player)
    {
        Vector3 rayOrigin = player.transform.position + Vector3.up * player.CrouchHeight;
        bool ceiling = Physics.SphereCast(rayOrigin, player.SphereRadiusCeiling, Vector3.up, out RaycastHit hit, player.ObjectHeight, player.CeilingLayer);

        Color color = ceiling ? Color.green : Color.red;
        Debug.DrawLine(rayOrigin, rayOrigin + Vector3.up * player.ObjectHeight, color);
        DebugExtension.DebugWireSphere(rayOrigin, color, player.SphereRadiusCeiling);
        if (ceiling)
        {
            Vector3 hitPoint = rayOrigin + Vector3.up * hit.distance;
            DebugExtension.DebugWireSphere(hitPoint, color, player.SphereRadiusCeiling);
        }
        return ceiling;
    }
}
