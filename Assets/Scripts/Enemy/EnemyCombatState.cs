using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : EnemyBaseState
{

    public override void EnterState(Enemy enemy)
    {
        Debug.Log("Entering Combat State");
    }

    public override void UpdateState(Enemy enemy)
    {
        if(enemy.Type == EnemyType.Plant)
        {
            if(enemy.AttackRange >= Vector3.Distance(enemy.transform.position, enemy.PlayerTransform.position))
            {

            // Make the head pivot look at the player
                Vector3 lookDirection = enemy.PlayerTransform.position - enemy.EnemyHeadPivot.transform.position;

                // Calculate the target rotation
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                // Extract the euler angles of the target rotation
                Vector3 targetEuler = targetRotation.eulerAngles;

                // Limit the X-axis rotation
                targetEuler.x = Mathf.Clamp(targetEuler.x, -30f, 30f); // Adjust these values as needed

                // Apply the constrained rotation back to the HeadPivot
                enemy.EnemyHeadPivot.transform.rotation = Quaternion.Euler(targetEuler);

                // Copy the Y-axis rotation of the head pivot to the enemy's body
                Vector3 headRotation = enemy.EnemyHeadPivot.transform.eulerAngles;
                Vector3 enemyRotation = enemy.transform.eulerAngles;
                enemyRotation.y = headRotation.y; // Copy only the Y rotation
                enemy.transform.eulerAngles = enemyRotation;

                // enemy.Shoot();
                // Perform a raycast to check for obstacles
                RaycastHit hit;
                Vector3 rayDirection = enemy.PlayerTransform.position - enemy.ProjectileSpawnPoint.position;

                if (Physics.Raycast(enemy.ProjectileSpawnPoint.position, rayDirection, out hit, enemy.AttackRange))
                {
                    // Check if the raycast hit the player
                    if (hit.transform == enemy.PlayerTransform)
                    {
                        // No obstacles, shoot
                        enemy.Shoot();
                    }
                    else
                    {
                        Debug.Log("Obstacle detected, cannot shoot.");
                    }
                }
            }
            
        }if(enemy.Type == EnemyType.Toothling)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.PlayerTransform.position, enemy.MoveSpeed * Time.deltaTime);
        }
        
        
    }
}