using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerControllerAccess : MonoBehaviour
{
    protected PlayerManager Player;

    protected virtual void Awake()
    {
        Player = GetComponent<PlayerManager>();
    }
}

