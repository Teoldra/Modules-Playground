using UnityEngine;

public class CeilingChecker : MonoBehaviour
{
    [SerializeField] private LayerMask ceilingLayer;
    [SerializeField] private float raycastPufferRange = 0.5f;
    [SerializeField] private float objectHeight = 1f;
    [SerializeField] private float rayBegin = -0.5f;
    [SerializeField] private float sphereRadius = 0.5f;

    private bool lastCeilingState;

    [Header("SO Event")]
    [SerializeField] private BoolGameEvent onCeilingChangedEvent;
    private void CeilingCheck()
    {
        //if (state == MovementState.Crouching)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * rayBegin;
            bool ceillingNow = Physics.SphereCast(rayOrigin, sphereRadius, Vector3.up, out RaycastHit hit, objectHeight, ceilingLayer);

            if (ceillingNow != lastCeilingState)
            {
                lastCeilingState = ceillingNow;
                onCeilingChangedEvent.Raise(ceillingNow);
            }
        }
    }
}
