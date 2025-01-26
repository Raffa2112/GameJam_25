using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundCooldown : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sound;
    [SerializeField] private float _cooldown = 0.1f;
    [SerializeField] private float _pitchMin = 1f;
    [SerializeField] private float _pitchMax = 1f;

    protected AudioSource _audioSource;

    private float _lastPlayedTime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _lastPlayedTime = -_cooldown;
    }

    public void PlaySound()
    {
        // if enough time has passed since the last sound was played
        if (Time.time - _lastPlayedTime > _cooldown)
        {
            AudioUtils.PlaySound(_audioSource, _sound, _pitchMin, _pitchMax);
            _lastPlayedTime = Time.time;
        }
    }
}