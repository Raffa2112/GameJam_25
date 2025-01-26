using UnityEngine;

public class Player_Shadow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _shadowReach = 100f;
    [SerializeField, Range(0, 1)] private float _initialShadowAlpha = 0.5f;

    private SpriteRenderer _spriteRenderer;

    private Vector3 _shadowHit;
    private float _shadowDistanceFactor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float alpha = Mathf.Lerp(0f, _initialShadowAlpha, _shadowDistanceFactor);
        _spriteRenderer.color = new Color(0, 0, 0, alpha);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.position.x, _shadowHit.y, _player.position.z);
    }

    private void FixedUpdate()
    {
        // raycast from player to below
        if (Physics.Raycast(_player.position, Vector3.down, out RaycastHit hit, _shadowReach))
        {
            _shadowHit = hit.point + Vector3.up * 0.1f;
            _spriteRenderer.enabled = true;
            _shadowDistanceFactor = 1 - Mathf.Clamp01(hit.distance / _shadowReach);
        }
        else
        {
            _spriteRenderer.enabled = false;
            _shadowDistanceFactor = 0;
        }
    }
}