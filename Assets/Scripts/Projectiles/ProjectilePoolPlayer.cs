using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolPlayer : MonoBehaviour
{
    public static ProjectilePoolPlayer Instance { get; private set; }
    public Bubble projectilePrefab;
    public int poolSize = 10;

    private Queue<Bubble> BubblePool;
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

        BubblePool = new Queue<Bubble>();
        // ActivatedProjectilePool = new Queue<Tooth>();

        // Initialize pool with deactivated projectiles
        for (int i = 0; i < poolSize; i++)
        {
            Bubble projectile = Instantiate(projectilePrefab);
            projectile.gameObject.SetActive(false);
            BubblePool.Enqueue(projectile);
        }
    }

    public Bubble GetProjectile()
    {
        // Check if there are any deactivated projectiles available
        if (BubblePool.Count > 0)
        {
            Bubble projectile = BubblePool.Dequeue();
            projectile.gameObject.SetActive(true);
            // ActivatedProjectilePool.Enqueue(projectile);
            BubblePool.Enqueue(projectile);
            return projectile;
        }

        // If the deactivated pool is empty, instantiate a new projectile
        Bubble newProjectile = Instantiate(projectilePrefab);
        // ActivatedProjectilePool.Enqueue(newProjectile);
        BubblePool.Enqueue(newProjectile);
        return newProjectile;
    }


}
