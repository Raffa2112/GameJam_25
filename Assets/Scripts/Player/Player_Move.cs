using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Move : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 8f;
    // [SerializeField] private float _rotationSpeed = 4f;
    // [SerializeField] private float _pullForce = 5f;

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
        Vector3 towerCenterAlignedWithPlayer = new(CentreOfUniverse.Position.X, transform.position.y, CentreOfUniverse.Position.Z);
        _directionToTower = (towerCenterAlignedWithPlayer - transform.position).normalized;
        _towerRightDirection = Vector3.Cross(Vector3.up, _directionToTower);

        DoMove();

        // Rotate the player to align with the cylinder surface
        // Vector3 upDirection = (transform.position - _towerCenter.position).normalized;
        // Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(-_towerRightDirection, upDirection), upDirection);
        // transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    private void DoMove()
    {
        Vector3 moveDirection = _moveDirectionX * _towerRightDirection + _moveDirectionY * _directionToTower;
        // Vector3 moveDirection = _moveDirectionY * _directionToTower;
        velocity += moveDirection * _moveSpeed;
        velocity -= velocity * 0.2f;

        // Vector3 centripalForce = Vector3.zero;
        // if (_moveDirectionY == 0 && _moveDirectionX != 0)
        // {
        //     // Apply centripetal force to keep the player near the tower center
        //     Vector3 toCenter = (_towerCenter.position - transform.position).normalized;
        //     centripalForce = toCenter * _pullForce;
        //     _controller.SimpleMove(centripalForce);
        // }

        // ROTATE WORLD INSTEAD OF MOVING PLAYER
        // if (_moveDirectionX != 0)
        // {
        //     int direction = _moveDirectionX > 0 ? 1 : -1;
        //     _towerCenter.Rotate(Vector3.up, direction * (_rotationSpeed * Mathf.Rad2Deg) * Time.deltaTime);
        // }

        _controller.Move(moveDirection * _moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 movement)
    {
        _moveDirectionX = movement.x;
        _moveDirectionY = movement.y;
    }
}
