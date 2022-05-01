using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy>
{
    public EnemyBaseState currentState = new AttackState();
    public Sprite sprite;
    public Animator animator;
    public float actionTime;
    public float EnemyAction()
    {
        AiTree();
        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        return actionTime;
    }
    public virtual void AiTree(){
        currentState.EnterState(this);
    }

}
