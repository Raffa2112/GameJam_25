using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] CharacterController _player;
    [SerializeField] Transform _respawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.enabled = false;
            _player.transform.position = _respawnPoint.position + new Vector3(0, 2, 0);
            _player.enabled = true;
        }
    }
}
