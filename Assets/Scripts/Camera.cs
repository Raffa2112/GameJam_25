using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform cameraPivot; // Reference to the camera pivot
    public float fixedDistance = 15f; // Fixed distance from the camera pivot to the camera

    void LateUpdate()
    {
        // Calculate the direction from the camera pivot to the player
        Vector3 directionToPlayer = (player.position - cameraPivot.position).normalized;

        // Set the camera's position at a fixed distance from the camera pivot along the same direction
        transform.position = cameraPivot.position + directionToPlayer * fixedDistance;

        // Make the camera look at the player
        transform.LookAt(player);
    }
}