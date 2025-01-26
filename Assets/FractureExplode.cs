using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureExplode : MonoBehaviour
{
    
    public float explosionForce = 500f;
    public float explosionRadius = 2f;
    public Vector3 explosionOffset = new Vector3(0, 0, 0);
    public float timeToLive = 2f; // Time before the object is destroyed
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(){
        Debug.Log("Exploding tooth set active");
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rigidbodies){
            rb.AddExplosionForce(explosionForce, transform.position + explosionOffset, explosionRadius);
        }

        // Destroy the game object after the specified time
        Destroy(gameObject, timeToLive);
    }
}
