using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] Player_Controller _Controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _Controller.OnWin();
            GameManager.Instance.OnWin();
        }
    }
}