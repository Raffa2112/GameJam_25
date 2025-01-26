using UnityEngine;

public class Anim_Death : MonoBehaviour
{
    public void DeathAnimEnd()
    {
        GameManager.Instance.OnGameOver();
    }
}