using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isFloating = false;
    [SerializeField] private float floatHeight = 2f; // Height above the current position
    [SerializeField] private float floatSpeed = 0.5f; // Speed of floating motion
    [SerializeField] private float floatDuration = 5f; // How long the enemy floats before stopping

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //on trigger collider if collided by bubble then activate bubble child object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Bubble hit enemy");
            // Start floating effect
            if (!isFloating)
            {
                StartCoroutine(FloatEffect());
            }
        }

        //destroy other gameobject
        Destroy(other.gameObject);
    }

    private IEnumerator FloatEffect()
    {
        isFloating = true;
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * floatHeight;

        // Float upwards
        while (elapsedTime < floatDuration / 2f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / (floatDuration / 2f)));
            elapsedTime += Time.deltaTime * floatSpeed;
            yield return null;
        }

        elapsedTime = 0f;

        // Float downwards slightly for a "hover" effect
        while (elapsedTime < floatDuration / 2f)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, (elapsedTime / (floatDuration / 2f)));
            elapsedTime += Time.deltaTime * floatSpeed;
            yield return null;
        }

        // Return to original position (optional)
        transform.position = startPosition;
        isFloating = false;
    }
}
