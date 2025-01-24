using UnityEngine;

[RequireComponent(typeof(Player_Move))]
public class Player_Controller : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_Player _inputReader;

    // Component References
    private Player_Move _playerMove;

    private void Awake()
    {
        _playerMove = GetComponent<Player_Move>();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMovementInput;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMovementInput;
    }

    private void OnMovementInput(Vector2 movement)
    {
        _playerMove.SetMoveDirection(movement);
    }

    private void Update()
    {
        // noop
    }
}