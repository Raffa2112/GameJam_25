using UnityEngine;

public class GunAimingSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    [SerializeField] private Transform gunRotationPoint; // The object to rotate (e.g., the gun or player's upper body)
    [SerializeField] private LayerMask aimLayerMask; // LayerMask for objects that can be aimed at

    [SerializeField] private BubbleGun bubbleGun; // Reference to the BubbleGun script
    void Update()
    {
        AimGun();
    }

    void AimGun()
    {
        // Get the mouse position as a ray from the camera
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits any object within the aimLayerMask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimLayerMask))
        {
            // Get the point where the ray hit
            Vector3 hitPoint = hit.point;

            // Calculate the direction from the gun to the hit point
            Vector3 aimDirection = (hitPoint - gunRotationPoint.position).normalized;

            // Rotate the gun to face the hit point
            Quaternion lookRotation = Quaternion.LookRotation(aimDirection);
            gunRotationPoint.rotation = Quaternion.Slerp(gunRotationPoint.rotation, lookRotation, Time.deltaTime * 10f); // Smooth rotation
        }
        //if player left clicks then shoot
        if (Input.GetMouseButtonDown(0))
        {
            bubbleGun.Shoot();
        }
    }
}
