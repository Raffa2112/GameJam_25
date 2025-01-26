using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    private bool _isDead = false;
    public bool IsDead => _isDead;

    public event Action OnDeath;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;

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