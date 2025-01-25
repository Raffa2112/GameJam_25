using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private CharacterController _controller;

    [Header("Jump Settings")]
    [SerializeField] private float _initialJumpVelocity = 10f;
    [SerializeField] private float _jumpBufferTime = 0.1f;
    [SerializeField] private float _coyoteTime = 0.1f;

    [Header("Gravity Settings")]
    [SerializeField] private float _gravityInAir = -9.81f;
    [SerializeField] private float _gravityOnGround = -0.05f;

    private float _jumpBufferTimer = 0f;
    private float _coyoteTimer = 0f;

    private Vector3 _currentMovement = Vector3.zero;
    public Vector3 CurrentMovement => _currentMovement;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_jumpBufferTimer > 0)
            _jumpBufferTimer -= Time.deltaTime;
        if (_coyoteTimer > 0)
            _coyoteTimer -= Time.deltaTime;

        if (!_controller.isGrounded)
        {
            _currentMovement.y += _gravityInAir * Time.deltaTime;
        }
        else
        {
            _currentMovement.y = _gravityOnGround;
            _coyoteTimer = _coyoteTime;
        }

        // was jump pressed in buffer timeframe & is still in coyote time
        if (_jumpBufferTimer > 0 && _coyoteTimer > 0)
        {
            _currentMovement.y += Mathf.Sqrt(_initialJumpVelocity * 2 * -_gravityInAir);
            _jumpBufferTimer = 0;
        }
    }

    public void JumpPressed()
    {
        _jumpBufferTimer = _jumpBufferTime;
    }
}