using UnityEngine;

public class GroundChecker : CharAccess
{
    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastPufferRange = 0.5f;
    [SerializeField] private float objectHeight = 1f;


    private bool lastGroundedState;

    private void Update()
    {
        CheckGrounded();
    }
    private void CheckGrounded()
    {
        float raycastDistance = objectHeight + raycastPufferRange;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        bool groundedNow = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);

        if (groundedNow != lastGroundedState)
        {
            lastGroundedState = groundedNow;
            charController.charControllerSO.Events.IsGrounded.Invoke(groundedNow);
            Debug.Log("Fired");
        }

        // Debug-Ray visualisieren
        Vector3 rayDirection = Vector3.down * raycastDistance;
        Debug.DrawRay(rayOrigin, rayDirection, groundedNow ? Color.green : Color.red);
    }
}
