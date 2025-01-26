using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }
    public Tooth projectilePrefab;
    public int poolSize = 10;

    private Queue<Tooth> TeethPool;
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

        TeethPool = new Queue<Tooth>();
        // ActivatedProjectilePool = new Queue<Tooth>();

        // Initialize pool with deactivated projectiles
        for (int i = 0; i < poolSize; i++)
        {
            Tooth projectile = Instantiate(projectilePrefab);
            projectile.gameObject.SetActive(false);
            TeethPool.Enqueue(projectile);
        }
    }

    public Tooth GetProjectile()
    {
        // Check if there are any deactivated projectiles available
        if (TeethPool.Count > 0)
        {
            Tooth projectile = TeethPool.Dequeue();
            projectile.gameObject.SetActive(true);
            // ActivatedProjectilePool.Enqueue(projectile);
            TeethPool.Enqueue(projectile);
            return projectile;
        }

        // If the deactivated pool is empty, instantiate a new projectile
        Tooth newProjectile = Instantiate(projectilePrefab);
        // ActivatedProjectilePool.Enqueue(newProjectile);
        TeethPool.Enqueue(newProjectile);
        return newProjectile;
    }

    public void ReturnProjectile(Tooth projectile)
    {
        // Deactivate the projectile and move it back to the deactivated pool
        if (TeethPool.Contains(projectile))
        {
            TeethPool.Dequeue();
        }

        projectile.gameObject.SetActive(false);
        TeethPool.Enqueue(projectile);
    }
}
