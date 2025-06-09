using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CrouchController : PlayerControllerAccess
{
    private Vector3 baseCharacterCenter;
    private float baseCharacterHeight;
    private MovementState state;

    private void OnEnable()
    {
        Player.Get.Events.State += GetState;
    }
    private void OnDisable()
    {
        Player.Get.Events.State -= GetState;
    }
    private void GetState(MovementState movementState)
    {
        state = movementState;
        CrouchingPlayerHeight();
    }

    private void Start()
    {
        baseCharacterCenter = Player.Get.CharacterController.center;
        baseCharacterHeight = Player.Get.CharacterController.height;
    }
    private void CrouchingPlayerHeight()
    {
        if (state.IsCrouching())
        {
            Player.Get.CharacterController.center = new Vector3(baseCharacterCenter.x, Player.CrouchHeight, baseCharacterCenter.z);
            Player.Get.CharacterController.height = (baseCharacterHeight / 2);
        }
        else
        {
            Player.Get.CharacterController.center = baseCharacterCenter;
            Player.Get.CharacterController.height = baseCharacterHeight;
        }
    }
}
