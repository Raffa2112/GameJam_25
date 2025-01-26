using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _invincibilityTime = 0.5f;

    private int _currentHealth;
    private bool _isDead = false;
    public bool IsDead => _isDead;

    private float _invincibilityTimer = 0f;

    public event Action OnDeath;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_invincibilityTimer > 0)
            _invincibilityTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead || _invincibilityTimer > 0) return;

        _currentHealth -= damage;
        _invincibilityTimer = _invincibilityTime;
        Debug.Log($"{gameObject.name} health: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        OnDeath?.Invoke();
    }
}