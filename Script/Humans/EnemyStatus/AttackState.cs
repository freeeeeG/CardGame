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
        enemy.animator.SetTrigger("attack");
        // enemy.actionTime = enemy.animator.GetCurrentAnimatorStateInfo(0).length;
        // StartCoroutine(AttackOver(enemy));
        Hurt(enemy.data.attack);
        CamareManager.Instance.FollowPlayer(1f);
        
    }
    public void Hurt(int damage)
    {
        Player.Instance.data.hp -= damage;

    }
}
