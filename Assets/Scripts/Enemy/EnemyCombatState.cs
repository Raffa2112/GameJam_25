using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : EnemyBaseState
{
    private float attackRange = 2f;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    public override void EnterState(Enemy enemy)
    {
        Debug.Log("Entering Combat State");
    }

    public override void UpdateState(Enemy enemy)
    {
        
        float distance = Vector3.Distance(enemy.transform.position, enemy.PlayerTransform.position);
        if (distance > attackRange)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.PlayerTransform.position, enemy.MoveSpeed * Time.deltaTime);
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                // enemy.Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }
}