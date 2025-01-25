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
                enemy.transform.LookAt(enemy.PlayerTransform);
                enemy.Shoot();
            }
            
        }
        
        
    }
}