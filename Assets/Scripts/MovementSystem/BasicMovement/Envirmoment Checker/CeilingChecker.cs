using UnityEngine;

public class CeilingChecker : PlayerControllerAccess
{
    private bool lastCeilingState;

    private void Update()
    {
        CeilingCheck();
    }
    private void CeilingCheck()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * Player.CrouchHeight;
        bool ceillingNow = Physics.SphereCast(rayOrigin, Player.SphereRadiusCeiling, Vector3.up, out RaycastHit hit, Player.ObjectHeight, Player.CeilingLayer);

        if (ceillingNow != lastCeilingState)
        {
            lastCeilingState = ceillingNow;
            Player.Get.Events.IsCeiling(ceillingNow);
        }

    }
}
