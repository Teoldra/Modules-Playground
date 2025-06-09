using System;
using UnityEngine;


public class MovementController : PlayerControllerAccess, IMoveVelocity
{
    private Vector3 velocity;
    private Vector2 movementInput;
    MovementState state;
    private float speedVelocity;
    private void OnEnable()
    {
        Player.Get.Events.State += GetMoveState;
        Player.Get.Events.FallVelocity += GetFallVelocity;
    }
    private void OnDisable()
    {
        Player.Get.Events.State -= GetMoveState;
        Player.Get.Events.FallVelocity += GetFallVelocity;
    }
    private void GetFallVelocity(Vector3 vector)
    {
        velocity = vector;
    }

    private void GetMoveState(MovementState movementState)
    {
        state = movementState;
    }

    private void Update()
    {
        CharacterMove();
        SpeedControl(state);
        UpdateSlopeMultiplier();
    }
    public void SetVelocity(Vector2 input)
    {
        movementInput = input;
    }

    private void CharacterMove()
    {
        Vector3 moveDir = transform.forward * movementInput.y + transform.right * movementInput.x;
        Vector3 move = moveDir.normalized * Player.CurrentSpeed;

        Player.Get.CharacterController.Move((move + velocity) * Time.deltaTime);
    }

    private void SpeedControl(MovementState state)
    {
        float targetSpeed;
        if (state.IsSprinting())
            targetSpeed = (Player.BaseMoveSpeed + Player.SprintSpeed) * Player.SlopeMultiplier;
        else if (state.IsCrouching())
            targetSpeed = (Player.BaseMoveSpeed - Player.CrouchSpeed) * Player.SlopeMultiplier;
        else if (state.IsGrounded())
            targetSpeed = Player.BaseMoveSpeed * Player.SlopeMultiplier;
        else if (state.IsAirborne())
            targetSpeed = Player.BaseMoveSpeed;
        else
            targetSpeed = Player.BaseMoveSpeed;

        float speedSmoothTime = 0.3f;
        Player.CurrentSpeed = (float)Math.Round(Mathf.SmoothDamp(Player.CurrentSpeed, targetSpeed, ref speedVelocity, speedSmoothTime),2);
    }

    private void UpdateSlopeMultiplier()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        float raycastDistance = Player.ObjectHeight + 0.5f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, raycastDistance))
        {
            var angle = Vector3.Angle(hit.normal, Vector3.up);

            // Bestimme "bergab"-Richtung
            Vector3 slopeDirection = Vector3.Cross(Vector3.Cross(hit.normal, Vector3.down), hit.normal).normalized;

            // Spielerbewegungsrichtung (auf horizontaler Ebene)
            Vector3 moveDirection = Player.Get.CharacterController.velocity; // oder andere Bewegungsquelle
            moveDirection.y = 0;
            moveDirection.Normalize();

            // Dot-Produkt zur Bestimmung der Laufrichtung relativ zum Hang
            float directionDot = Vector3.Dot(moveDirection, slopeDirection);

            Debug.DrawRay(hit.point, hit.normal, Color.green); // Normale (senkrecht zum Boden)
            Debug.DrawRay(hit.point, slopeDirection, Color.red); // Bergab-Richtung
            Debug.DrawRay(rayOrigin, moveDirection, Color.blue); // Spielerbewegung

            if (angle > 10f)
            {
                // Optional: untersch. Multiplier für bergauf / bergab
                if (directionDot > 0)
                {
                    // Bergab
                    Player.SlopeMultiplier = 1 + (angle / 100f); // Optional: z.B. schneller bergab
                }
                else
                {
                    // Bergauf
                    Player.SlopeMultiplier = 1 - (angle / 100f); // Langsamer bergauf
                }
            }
            else
            {
                Player.SlopeMultiplier = 1f;
            }
        }
        else
        {
            Player.SlopeMultiplier = 1f;
        }
    }

}

