using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct PlayerEvents
{
    //State events
    public Action<bool> IsGrounded;
    public Action<bool> IsCeiling;
    public Action<MovementState> State;
    public Action<Vector3> FallVelocity;
}
