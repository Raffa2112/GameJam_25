using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Game_Controls;

[CreateAssetMenu(menuName = "InputReader_Player")]
public class InputReader_Player : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> AimEvent;
    public event Action JumpEvent;

    public event Action ShootEvent;

    public event Action JumpCancelledEvent;


    private Game_Controls _playerInput;

    private void OnEnable()
    {
        SetupInput();
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    private void SetupInput()
    {
        if (_playerInput == null)
        {
            _playerInput = new Game_Controls();
            _playerInput.Player.SetCallbacks(this);
        }
    }

    public void EnableInput()
    {
        _playerInput.Player.Enable();
    }

    public void DisableInput()
    {
        _playerInput.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            Debug.Log("Jump");
            JumpEvent?.Invoke();
        }
        else
        {
            Debug.Log("Jump Cancelled");
            JumpCancelledEvent?.Invoke();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
            ShootEvent?.Invoke();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
