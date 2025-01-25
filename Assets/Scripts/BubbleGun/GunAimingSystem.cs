using System;
using UnityEngine;

using UnityEngine;

public class GunAimingSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    [SerializeField] private Transform gunRotationPoint; // The object to rotate (e.g., gun or player's upper body)
    [SerializeField] private BubbleGun bubbleGun; // Reference to the BubbleGun script
    [SerializeField] private LayerMask enemyLayerMask; // LayerMask for enemies
    [SerializeField] private float maxAimDistance = 100f; // Maximum distance to consider enemies
    [SerializeField] private InputReader_Player _inputReader; // Custom input reader
    [SerializeField] private float joystickDeadZone = 0.1f; // Threshold to ignore small joystick movements

    private Vector2 aimInput = Vector2.zero;

    void Update()
    {
        // Get joystick or mouse-based aiming input
        // aimInput = _inputReader.GetAimInput(); // This should return a Vector2 (-1 to 1 for X, Y)

        if (aimInput.sqrMagnitude > joystickDeadZone * joystickDeadZone)
        {
            AimGunWithJoystick();
        }
        else
        {
            AimGunAtClosestEnemyToMouse();
        }
    }

    void OnEnable()
    {
        _inputReader.ShootEvent += OnShoot;
        _inputReader.AimEvent += OnAim;
    }

    void OnDisable()
    {
        _inputReader.ShootEvent -= OnShoot;
        _inputReader.AimEvent -= OnAim;
    }
    private void OnAim(Vector2 aim)
    {
        aimInput = aim;
    }
    private void OnShoot()
    {
        bubbleGun.Shoot();
    }

    void AimGunWithJoystick()
    {
        // Convert joystick input to world direction
        Vector3 aimDirection = new Vector3(aimInput.x, 0f, aimInput.y);
        aimDirection = mainCamera.transform.TransformDirection(aimDirection);
        aimDirection.y = 0f; // Ignore vertical offset to keep aiming level

        // Find closest enemy in the joystick direction
        Collider[] enemiesInRange = Physics.OverlapSphere(gunRotationPoint.position, maxAimDistance, enemyLayerMask);

        if (enemiesInRange.Length > 0)
        {
            Transform closestEnemy = null;
            float smallestAngle = float.MaxValue;

            foreach (Collider enemyCollider in enemiesInRange)
            {
                Vector3 directionToEnemy = (enemyCollider.transform.position - gunRotationPoint.position).normalized;
                float angle = Vector3.Angle(aimDirection, directionToEnemy);

                if (angle < smallestAngle)
                {
                    smallestAngle = angle;
                    closestEnemy = enemyCollider.transform;
                }
            }

            if (closestEnemy != null)
            {
                // Rotate the gun to aim at the closest enemy
                Vector3 targetDirection = (closestEnemy.position - gunRotationPoint.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
                gunRotationPoint.rotation = Quaternion.Slerp(gunRotationPoint.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }

    void AimGunAtClosestEnemyToMouse()
    {
        // Existing mouse-based aiming logic
        Collider[] enemiesInRange = Physics.OverlapSphere(gunRotationPoint.position, maxAimDistance, enemyLayerMask);

        if (enemiesInRange.Length == 0)
            return;

        Transform closestEnemy = null;
        float smallestScreenDistance = float.MaxValue;

        Vector2 mousePosition = Input.mousePosition;

        foreach (Collider enemyCollider in enemiesInRange)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(enemyCollider.transform.position);

            if (screenPos.z < 0)
                continue;

            float screenDistance = Vector2.Distance(mousePosition, screenPos);

            if (screenDistance < smallestScreenDistance)
            {
                smallestScreenDistance = screenDistance;
                closestEnemy = enemyCollider.transform;
            }
        }

        if (closestEnemy != null)
        {
            Vector3 aimDirection = (closestEnemy.position - gunRotationPoint.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(aimDirection);
            gunRotationPoint.rotation = Quaternion.Slerp(gunRotationPoint.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}


// public class GunAimingSystem : MonoBehaviour
// {
//     [SerializeField] private Camera mainCamera; // Reference to the main camera
//     [SerializeField] private Transform gunRotationPoint; // The object to rotate (e.g., gun or player's upper body)
//     [SerializeField] private BubbleGun bubbleGun; // Reference to the BubbleGun script
//     [SerializeField] private LayerMask enemyLayerMask; // LayerMask for enemies
//     [SerializeField] private float maxAimDistance = 100f; // Maximum distance to consider enemies
//     [SerializeField] private InputReader_Player _inputReader;
//     void Update()
//     {
//         AimGunAtClosestEnemyToMouse();
//         // if (Input.GetMouseButtonDown(0))
//         // {
//         //     bubbleGun.Shoot();
//         // }
//     }

//     void OnEnable()
//     {
//         _inputReader.ShootEvent += OnShoot;
//     }


//     void OnDisable()
//     {
//         _inputReader.ShootEvent -= OnShoot;
//     }

//     private void OnShoot()
//     {
//         bubbleGun.Shoot();
//     }

//     void AimGunAtClosestEnemyToMouse()
//     {
//         // Find all enemies within a certain range
//         Collider[] enemiesInRange = Physics.OverlapSphere(gunRotationPoint.position, maxAimDistance, enemyLayerMask);

//         if (enemiesInRange.Length == 0)
//             return;

//         Transform closestEnemy = null;
//         float smallestScreenDistance = float.MaxValue;

//         Vector2 mousePosition = Input.mousePosition;

//         foreach (Collider enemyCollider in enemiesInRange)
//         {
//             // Project enemy position to screen space
//             Vector3 screenPos = mainCamera.WorldToScreenPoint(enemyCollider.transform.position);

//             // Skip enemies behind the camera
//             if (screenPos.z < 0)
//                 continue;

//             // Calculate distance between mouse and enemy in screen space
//             float screenDistance = Vector2.Distance(mousePosition, screenPos);

//             if (screenDistance < smallestScreenDistance)
//             {
//                 smallestScreenDistance = screenDistance;
//                 closestEnemy = enemyCollider.transform;
//             }
//         }

//         if (closestEnemy != null)
//         {
//             // Rotate the gun to aim at the closest enemy
//             Vector3 aimDirection = (closestEnemy.position - gunRotationPoint.position).normalized;
//             Quaternion lookRotation = Quaternion.LookRotation(aimDirection);
//             gunRotationPoint.rotation = Quaternion.Slerp(gunRotationPoint.rotation, lookRotation, Time.deltaTime * 10f);
//         }
//     }
// }


