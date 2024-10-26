using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private Animator animator;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Start()
    {
        animator = transform.parent.parent.GetComponent<Animator>(); //Access grandparent animator component
    }
    
    public override State RunCurrentState()
    {
        animator.SetBool(IsAttacking, true);
        Debug.Log("I have Attacked!");
        return this;
    }
}
