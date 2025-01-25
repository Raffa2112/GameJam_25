using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }
    public Tooth projectilePrefab;
    public int poolSize = 10;

    private Queue<Tooth> DeactivatedProjectilePool;
    // private Queue<Tooth> ActivatedProjectilePool;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DeactivatedProjectilePool = new Queue<Tooth>();
        // ActivatedProjectilePool = new Queue<Tooth>();

        // Initialize pool with deactivated projectiles
        for (int i = 0; i < poolSize; i++)
        {
            Tooth projectile = Instantiate(projectilePrefab);
            projectile.gameObject.SetActive(false);
            DeactivatedProjectilePool.Enqueue(projectile);
        }
    }

    public Tooth GetProjectile()
    {
        // Check if there are any deactivated projectiles available
        if (DeactivatedProjectilePool.Count > 0)
        {
            Tooth projectile = DeactivatedProjectilePool.Dequeue();
            projectile.gameObject.SetActive(true);
            // ActivatedProjectilePool.Enqueue(projectile);
            DeactivatedProjectilePool.Enqueue(projectile);
            return projectile;
        }

        // If the deactivated pool is empty, instantiate a new projectile
        Tooth newProjectile = Instantiate(projectilePrefab);
        // ActivatedProjectilePool.Enqueue(newProjectile);
        DeactivatedProjectilePool.Enqueue(newProjectile);
        return newProjectile;
    }

    // public void ReturnProjectile(Tooth projectile)
    // {
    //     // Deactivate the projectile and move it back to the deactivated pool
    //     if (ActivatedProjectilePool.Contains(projectile))
    //     {
    //         ActivatedProjectilePool.Dequeue();
    //     }

    //     projectile.gameObject.SetActive(false);
    //     DeactivatedProjectilePool.Enqueue(projectile);
    // }
}
