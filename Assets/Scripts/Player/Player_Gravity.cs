using UnityEngine;

public class Player_Gravity : MonoBehaviour
{
    [SerializeField] private float _gravityInAir = -9.81f;
    [SerializeField] private float _gravityOnGround = -0.05f;

    private CharacterController _controller;

    private Vector3 _currentMovement = Vector3.zero;
    public Vector3 CurrentMovement => _currentMovement;

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
}