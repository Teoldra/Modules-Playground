using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerControllerSO Get;

    [Header("Movement Settings")]
    public float BaseMoveSpeed = 5f;
    public float CurrentSpeed;
    public float SprintSpeed = 2f;
    public float CrouchSpeed = 2f;
    public float SlopeMultiplier = 1f;

    [Header("Camera Settings")]
    public Transform HeadPivot; // Camera Look At
    public float MaxLookUp = -60f;
    public float MaxLookDown = 40f;
    public float RotationSpeed = 2f;

    [Header("Jump Settings")]
    public float JumpForce = 5f;
    public int BaseNumberOfJumps = 1;

    [Header("Crouch Settings")]
    public float CrouchHeight = -0.5f;

    [Header("Ground Check Settings")]
    public LayerMask GroundLayer;
    public float RaycastPufferRange = 0.5f;
    public float ObjectHeight = 1f;
    public float SphereRadiusGround = 0.5f;

    [Header("Ceiling Check Settings")]
    public LayerMask CeilingLayer;
    public float CeilingObjectHeight = 1f;
    public float SphereRadiusCeiling = 0.5f;

    [Header("Gravity Settings")]
    public float gravity = -9.81f;
    public float fallMultiplier = 2.5f;

    private void Awake()
    {
        Get.CharacterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
