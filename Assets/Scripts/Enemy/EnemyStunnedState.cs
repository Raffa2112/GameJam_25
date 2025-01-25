using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyBaseState
{

    public override void EnterState(Enemy enemy)
    {
        Debug.Log("Entering Stunned State");
    }

    public override void UpdateState(Enemy enemy)
    {
       Debug.Log("Stunned");
    }
}