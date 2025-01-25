using UnityEngine;

[RequireComponent(typeof(Player_Run))]
public class Player_Controller : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_Player _inputReader;

    // Component References
    private CharacterController _controller;
    private Player_Run _playerRun;
    private Player_Jump _playerJump;

    private Vector3 _currentMovement = Vector3.zero;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerRun = GetComponent<Player_Run>();
        _playerJump = GetComponent<Player_Jump>();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMovementInput;
        _inputReader.JumpEvent += OnJumpInput;
        _inputReader.JumpCancelledEvent += OnJumpReleaseInput;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMovementInput;
        _inputReader.JumpEvent -= OnJumpInput;
        _inputReader.JumpCancelledEvent -= OnJumpReleaseInput;
    }

    private void OnMovementInput(Vector2 movement)
    {
        _playerRun.SetMoveDirection(movement);
    }

    private void OnJumpInput()
    {
        _playerJump.JumpPressed();
    }

    private void OnJumpReleaseInput()
    {
        _playerJump.JumpReleased();
    }

    private void Update()
    {
        // get all movement vectors
        _currentMovement = new(_playerRun.CurrentMovement.x, _playerJump.CurrentMovement.y, _playerRun.CurrentMovement.z);
        // _currentMovement.y = _playerJump.CurrentMovement.y;

        // apply movement
        _controller.Move(_currentMovement * Time.deltaTime);

    }
}