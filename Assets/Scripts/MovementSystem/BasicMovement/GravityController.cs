using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class GravityController : PlayerControllerAccess
{
    MovementState state;
    private Vector3 fallVelocity;

    private void OnEnable()
    {
        Player.Get.Events.State += GetMoveState;
        Player.Get.Events.FallVelocity += GetFallVelocity;
    }
    private void OnDisable()
    {
        Player.Get.Events.State -= GetMoveState;
        Player.Get.Events.FallVelocity -= GetFallVelocity;
    }

    private void Update()
    {
        ApplyGravity();
    }

    private void GetFallVelocity(Vector3 vector)
    {
        fallVelocity = vector;
    }

    private void GetMoveState(MovementState movementState)
    {
        state = movementState;
    }
    private void ApplyGravity()
    {
        if (state.IsAirborne())
        {
            fallVelocity.y += Player.gravity * Player.fallMultiplier * Time.deltaTime;
            if(fallVelocity.y < -50f)
            {
                fallVelocity.y = -50f; // Limit fall speed to prevent excessive falling speed
            }
        }
        else
        {
            if (fallVelocity.y < 0)
            {
                fallVelocity.y = -5f;
            }
        }
        Player.Get.Events.FallVelocity.Invoke(fallVelocity);
    }
}
