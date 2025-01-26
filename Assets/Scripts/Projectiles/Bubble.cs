using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
    {
        [Header("Physics")]
        public Rigidbody Rigidbody;
        [HideInInspector]
        public float Speed; // m/s

        [Space]
        // When duration = 0, the bullets is destroyed
        [Range(1,10)]
        private float Duration; // seconds
        public float TimeToLive = 10;

        [Header("Impact")]
        [Range(0,1000)]
        public float Force = 100; // Netwons
        [Range(0.1f, 1)]
        public float Radius = 0.25f; // m

        // Start is called before the first frame update
        void Start()
        {
            // Debug.Log("Bubble created");
            // Debug.Log("Bubble speed: " + Speed);
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
                ProjectilePoolPlayer.Instance.ReturnProjectile(this.gameObject.GetComponent<Bubble>());
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
                // Debug.Log("hit!" + rigidbody.name);
            }

            // Destroy the bullet on collision
            // Destroy(gameObject);
        }

    }
