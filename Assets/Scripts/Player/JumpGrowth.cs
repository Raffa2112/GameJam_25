using UnityEngine;

[RequireComponent(typeof(Player_Jump))]
public class JumpGrowth : MonoBehaviour
{
    [SerializeField] private GrowthStats_SO _growthStats;
    [SerializeField] private JumpStats_SO[] _jumpStats;

    private Player_Jump _playerJump;

    private int _jumpLevel = 0;
    private int _collectablesThisLevel = 0;
    private int _totalCollectables = 0;

    private void Awake()
    {
        _playerJump = GetComponent<Player_Jump>();
        _playerJump.SetJumpStats(_jumpStats[_jumpLevel]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            GainCollectable();
            Destroy(other.gameObject);
        }
    }

    private void GainCollectable()
    {
        _collectablesThisLevel++;
        _totalCollectables++;
        if (_collectablesThisLevel >= _growthStats.CollectiblesToNextLevel[_jumpLevel])
        {
            _collectablesThisLevel = 0;
            _jumpLevel++;
            _playerJump.SetJumpStats(_jumpStats[_jumpLevel]);
            Debug.Log($"Jump level increased to {_jumpLevel}!");
        }
    }

}