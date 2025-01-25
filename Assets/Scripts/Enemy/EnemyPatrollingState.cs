using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{

    private Vector3 targetPosition;
    private float stoppingDistance = 0.1f;
    private float moveSpeed = 1f;


    public override void EnterState(Enemy enemy)
    {
        // targetPosition = enemy.GetRandomPosition();
    }

    public override void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, targetPosition) > stoppingDistance)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // targetPosition = enemy.GetRandomPosition();
        }
    }
}