public enum MovementState
{
    Walking = 0,
    Sprinting = 1,
    Crouching = 2,
    Air = 3,
}

public static class MovementStateResolver
{
    public static MovementState Resolve(
        bool isGrounded,
        bool isSprinting,
        bool isCrouching,
        bool isCeiling)
    {
        if (isCrouching && isGrounded)
            return MovementState.Crouching;

        if (isSprinting && !isCrouching && isGrounded && !isCeiling)
            return MovementState.Sprinting;

        if (!isGrounded && !isCeiling)
            return MovementState.Air;

        if (!isCrouching && !isCeiling)
            return MovementState.Walking;

        return MovementState.Walking; // Fallback
    }
}
