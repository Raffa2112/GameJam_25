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
    private float Duration; // seconds

    [Header("Impact")]
    [Range(0, 1000)]
    public float Force = 100; // Netwons
    [Range(0.1f, 1)]
    public float Radius = 0.25f; // m
    public int TimeToLive = 6;
    [SerializeField] private int _damage = 10;

    public GameObject FracturedTooth;
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody.velocity = transform.forward * Speed;
        Duration = TimeToLive;
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
    private void OnEnable()
    {
        Duration = TimeToLive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            ProjectilePool.Instance.ReturnProjectile(this.gameObject.GetComponent<Tooth>());
            GameObject fracturedTooth = Instantiate(FracturedTooth, transform.position, transform.rotation);
            fracturedTooth.GetComponent<FractureExplode>().Explode();

            if (other.gameObject.TryGetComponent(out Damageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }


    }
}
