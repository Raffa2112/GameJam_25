using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : MonoBehaviour
    {
        [Header("Bubble Gun Aim Mode")]
        // public bool lockOnAim = true;
        [Header("Bubble Bullet")]
        public Transform FirePoint;
        public Bubble BubbleBulletPrefab;

        [Header("Reload")]
        [Range(0, 2)]
        public float ReloadTime; // seconds
        private float ChosenReloadTime; 

        [Range(0, 1)]
        public float ReloadBoost;
        private float ReloadTimer; // seconds
        

        [Space]
        [Range(0, 50)]
        public float BulletSpeed; // m/s

        [Header("Sound")]
        public AudioSource AudioSource;
        public AudioClip ClipShooting;
        public AudioClip ClipCocking;

        [HideInInspector]
        public bool isPlayerGun = false;

        public delegate void OnBulletFired();
        public event OnBulletFired BulletFired;

        void Start()
        {
            ChosenReloadTime = ReloadTime;
        }
        public void ToggleReloadBoost(bool toggle)
        {
            if(toggle)
            {
                //Reduce reload Time
                ReloadTime -= ReloadBoost;
            }
            else
            {
                //reset reload time
                ReloadTime = ChosenReloadTime;
            }
        }
        public void Shoot(){
            if(ReloadTimer > 0)
            {
                // AudioSource.PlayOneShot(ClipCocking);
                return;
            }
            // Starts reloading
            ReloadTimer = ReloadTime;
            // Shoot!
            // Bubble bullet = Instantiate(BubbleBulletPrefab, FirePoint.position, FirePoint.rotation);
            // bullet.Speed = BulletSpeed;
            Bubble bullet = ProjectilePoolPlayer.Instance.GetProjectile();
            bullet.transform.position = FirePoint.position;
            bullet.transform.rotation = FirePoint.rotation;
            bullet.Speed = BulletSpeed;
            bullet.ShootProjectile();
            // Sound
            // AudioSource.PlayOneShot(ClipShooting);

            // Screenshake
            // Impulse.GenerateImpulse();

            // Event for player screen shake on shot
            if(isPlayerGun)
            {
                BulletFired?.Invoke();
            }
            
        }

      

        void Update()
        {
            ReloadTimer -= Time.deltaTime;
        }
    }