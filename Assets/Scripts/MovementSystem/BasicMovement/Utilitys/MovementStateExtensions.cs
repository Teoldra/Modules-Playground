public static class MovementStateExtensions
{
    public static bool IsGrounded(this MovementState state)
    {
        return state == MovementState.Walking || state == MovementState.Sprinting || state == MovementState.Crouching;
    }
    public static bool IsSprinting(this MovementState state)
    {
        return state == MovementState.Sprinting;
    }
    public static bool IsCrouching(this MovementState state)
    {
        return state == MovementState.Crouching;
    }
    public static bool IsAirborne(this MovementState state)
    {
        return state == MovementState.Air;
    }
    public static bool IsStanding(this MovementState state)
    {
        return state == MovementState.Standing;
    }
}