using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public PatrolState patrolState;
    public bool hasPatrolPoints;
    public override State RunCurrentState()
    {
        if (hasPatrolPoints)
        {
            return patrolState;
        }
        else
        {
            return this;
        }
        
    }
}
