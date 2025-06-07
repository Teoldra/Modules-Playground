using UnityEngine;

public class CeilingChecker : CharAccess
{
    [SerializeField] private LayerMask ceilingLayer;
    [SerializeField] private float objectHeight = 1f;
    [SerializeField] private float rayBegin = -0.5f;
    [SerializeField] private float sphereRadius = 0.5f;

    private bool lastCeilingState;


    private void Update()
    {
        CeilingCheck();
    }
    private void CeilingCheck()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayBegin;
        bool ceillingNow = Physics.SphereCast(rayOrigin, sphereRadius, Vector3.up, out RaycastHit hit, objectHeight, ceilingLayer);

        if (ceillingNow != lastCeilingState)
        {
            lastCeilingState = ceillingNow;
            charController.charControllerSO.Events.IsCeiling(ceillingNow);
        }

    }
}
