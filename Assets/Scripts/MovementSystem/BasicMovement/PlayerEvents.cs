using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct PlayerEvents
{
    //State events
    public Action<bool> IsGrounded;
    public Action<bool> IsCeiling;
}
