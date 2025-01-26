using UnityEngine;

[CreateAssetMenu(fileName = "JumpStats_SO", menuName = "JumpStats_SO", order = 0)]
public class JumpStats_SO : ScriptableObject
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpHeight = 10f;

    [Header("Gravity Settings")]
    [SerializeField] private float _gravityInAir = -9.81f;

    public float JumpHeight => _jumpHeight;
    public float GravityInAir => _gravityInAir;
}
