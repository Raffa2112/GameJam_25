using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float _initialJumpVelocity = 10f;
    [SerializeField] private float _gravityInAir = -9.81f;
    [SerializeField] private float _gravityOnGround = -0.05f;
    // [SerializeField] private float _maxJumpHeight = 1.5f;
    // [SerializeField] private float _maxJumpTime = 0.5f;

    private Vector3 _currentMovement = Vector3.zero;
    public Vector3 CurrentMovement => _currentMovement;

    // public bool JumpPressedThisFrame { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!_controller.isGrounded)
        {
            _currentMovement.y += _gravityInAir * Time.deltaTime;
        }
        else
        {
            _currentMovement.y = _gravityOnGround;
        }
    }

    public void JumpPressed()
    {
        if (_controller.isGrounded)
        {
            _currentMovement.y += Mathf.Sqrt(_initialJumpVelocity * 2 * -_gravityInAir);
        }
    }
}