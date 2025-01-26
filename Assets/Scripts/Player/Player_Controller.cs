using System;
using UnityEngine;

[RequireComponent(typeof(Player_Run), typeof(Player_Jump), typeof(Damageable))]
public class Player_Controller : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_Player _inputReader;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_DEAD = "IsDead";

    // Component References
    private CharacterController _controller;
    private Player_Run _playerRun;
    private Player_Jump _playerJump;
    private Damageable _damageable;

    private Vector3 _currentMovement = Vector3.zero;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerRun = GetComponent<Player_Run>();
        _playerJump = GetComponent<Player_Jump>();
        _damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        // _inputReader.EnableInput();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMovementInput;
        _inputReader.JumpEvent += OnJumpInput;
        _inputReader.JumpCancelledEvent += OnJumpReleaseInput;
        _damageable.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMovementInput;
        _inputReader.JumpEvent -= OnJumpInput;
        _inputReader.JumpCancelledEvent -= OnJumpReleaseInput;
        _damageable.OnDeath -= OnDeath;
    }

    private void OnMovementInput(Vector2 movement)
    {
        _playerRun.SetMoveDirection(movement);
        bool isMoving = movement.magnitude > 0;
        _animator.SetBool(IS_RUNNING, isMoving);
    }

    private void OnJumpInput()
    {
        _playerJump.JumpPressed();
    }

    private void OnJumpReleaseInput()
    {
        _playerJump.JumpReleased();
    }

    private void OnDeath()
    {
        _inputReader.DisableInput();
        _animator.SetTrigger(IS_DEAD);
    }

    private void Update()
    {
        // get all movement vectors
        _currentMovement = new(_playerRun.CurrentMovement.x, _playerJump.CurrentMovement.y, _playerRun.CurrentMovement.z);

        // rotate player toward the movement direction
        Vector3 horizontalMovement = new(_currentMovement.x, 0, _currentMovement.z);
        if (horizontalMovement.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalMovement, Vector3.up);
            _animator.gameObject.transform.rotation = Quaternion.Slerp(_animator.gameObject.transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        // apply movement
        _controller.Move(_currentMovement * Time.deltaTime);

        // check in air
        _animator.SetBool(IS_JUMPING, !_controller.isGrounded);
    }
}