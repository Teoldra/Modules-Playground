using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReaderSO : ScriptableObject, PlayerInputActions.IPlayerActions
{
    private PlayerInputActions inputActions;

    public event UnityAction<Vector2> Movement;
    public event UnityAction<Vector2> Rotation;
    public event UnityAction<bool> Jump;
    public event UnityAction<bool> Sprint;
    public event UnityAction<bool> Interact;
    public event UnityAction<bool> Crouching;

    private void OnEnable()
    {
        inputActions ??= new PlayerInputActions();
        inputActions.Player.SetCallbacks(this);
        EnablePlayerInputAction();
    }

    private void OnDisable()
    {
        DisablePlayerInputAction();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Jump?.Invoke(true);
        }
        else if (context.canceled)
        {
            Jump?.Invoke(false);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Rotation?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Sprint?.Invoke(true);
        }
        else if (context.canceled)
        {
            Sprint?.Invoke(false);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Interact?.Invoke(true);
        }
        if (context.canceled)
        {
            Interact?.Invoke(false);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Crouching?.Invoke(true);
        }
        else if (context.canceled)
        {
            Crouching?.Invoke(false);
        }
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void EnablePlayerInputAction()
    {
        inputActions.Player.Enable();
    }
    public void DisablePlayerInputAction()
    {
        inputActions.Player.Disable();
    }
}
