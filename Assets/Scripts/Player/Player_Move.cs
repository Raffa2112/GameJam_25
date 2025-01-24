using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Move : MonoBehaviour
{
    [Header("Pivot")]
    [SerializeField] private Transform _towerCenter;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 8f;

    private CharacterController _controller;

    private float _moveDirectionX;
    private float _moveDirectionY;
    private Vector3 velocity = Vector3.zero;

    private Vector3 _towerRightDirection;
    private Vector3 _directionToTower;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 towerCenterAlignedWithPlayer = new(_towerCenter.position.x, transform.position.y, _towerCenter.position.z);
        _directionToTower = (towerCenterAlignedWithPlayer - transform.position).normalized;
        _towerRightDirection = Vector3.Cross(Vector3.up, _directionToTower);

        DoMove();

        // Rotate the player to align with the cylinder surface
        Vector3 upDirection = (transform.position - _towerCenter.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(-_towerRightDirection, upDirection), upDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    private void DoMove()
    {
        Vector3 moveDirection = _moveDirectionX * _towerRightDirection + _moveDirectionY * _directionToTower;
        velocity += moveDirection * _moveSpeed;
        velocity -= velocity * 0.2f;
        _controller.SimpleMove(moveDirection * _moveSpeed);
    }

    public void SetMoveDirection(Vector2 movement)
    {
        _moveDirectionX = movement.x;
        _moveDirectionY = movement.y;
    }
}
