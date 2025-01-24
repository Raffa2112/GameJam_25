using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Game_Controls;

[CreateAssetMenu(menuName = "InputReader_Player")]
public class InputReader_Player : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MoveEvent;

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
}
