using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private CharacterController _controller;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpBufferTime = 0.1f;
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField, Range(0, 1)] private float _fallMultiplier = 0.5f;

    [Header("Gravity Settings")]
    [SerializeField] private float _gravityOnGround = -0.05f;

    // Growth Stats //
    private float _jumpHeight = 2f;
    private float _gravityInAir = -9.81f;

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
        if (_jumpBufferTimer > 0f)
            _jumpBufferTimer -= Time.deltaTime;
        if (_coyoteTimer > 0f)
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
        if (_jumpBufferTimer > 0f && _coyoteTimer > 0f)
        {
            _currentMovement.y = Mathf.Sqrt(_jumpHeight * 2f * -_gravityInAir);
            _jumpBufferTimer = 0f;
            _coyoteTimer = 0f;
        }
    }

    public void JumpPressed()
    {
        _jumpBufferTimer = _jumpBufferTime;
    }

    public void JumpReleased()
    {
        if (_currentMovement.y > 0f)
            _currentMovement.y *= _fallMultiplier;
    }

    #region Growth Stats

    public void SetJumpStats(JumpStats_SO jumpStats)
    {
        _jumpHeight = jumpStats.JumpHeight;
        _gravityInAir = jumpStats.GravityInAir;
    }

    #endregion
}