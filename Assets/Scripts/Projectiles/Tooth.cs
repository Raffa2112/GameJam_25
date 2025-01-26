using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    [Header("Physics")]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public float Speed; // m/s

    [Space]
    // When duration = 0, the bullets is destroyed
    [Range(1, 10)]
    public float Duration = 5f; // seconds

    [Header("Impact")]
    [Range(0, 1000)]
    public float Force = 100; // Netwons
    [Range(0.1f, 1)]
    public float Radius = 0.25f; // m
    public int TimeToLive = 6;
    [SerializeField] private int _damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody.velocity = transform.forward * Speed;
    }

    public void ShootProjectile()
    {
        Rigidbody.velocity = transform.forward * Speed;
    }

    void Update()
    {
        // If the bullet has no more time to live
        // it gets destroyed
        Duration -= Time.deltaTime;
        if (Duration <= 0)
            ProjectilePool.Instance.ReturnProjectile(this.gameObject.GetComponent<Tooth>());
    
    }
    private void OnEnable() {
        Duration = TimeToLive;
    }

    // Bullets die on collision
    void OnCollisionEnter(Collision collision)
    {
        // Add explosive force to objects (if they have a rigidbody)
        Rigidbody rigidbody = collision.collider.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(Force, transform.position, Radius);
            Debug.Log("hit!" + rigidbody.name);
        }

        if (collision.gameObject.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(_damage);
        }

        // Destroy the bullet on collision
        // Destroy(gameObject);
    }
}
