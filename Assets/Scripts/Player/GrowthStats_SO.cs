using UnityEngine;

[CreateAssetMenu(fileName = "GrowthStats_SO", menuName = "GrowthStats_SO", order = 0)]
public class GrowthStats_SO : ScriptableObject
{
    [SerializeField] private int[] _collectiblesToNextLevel;

    public int[] CollectiblesToNextLevel => _collectiblesToNextLevel;
}