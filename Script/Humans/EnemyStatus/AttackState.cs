using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
public class AttackState : EnemyBaseState
{
    
    public override void EnterState(Enemy enemy)
    {
        //TODO: enemy attack animation
        enemy.animator.SetBool("Attack", true);
        enemy.actionTime = enemy.animator.GetCurrentAnimatorStateInfo(0).length;
        // StartCoroutine(AttackOver(enemy));    
    }
    IEnumerator AttackOver(Enemy enemy)
    {
        yield return new WaitForSeconds(0.5f);
        enemy.animator.SetBool("Attack", false);

    }
    
    
}
