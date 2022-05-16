using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy>
{
    public EnemyBaseState currentState = new AttackState();
    public bool isDead = false;
    public Sprite sprite;
    public Animator animator;
    public EnemyData data;
    public float actionTime;
    public int actionNoRound = 0;
    #region Buff持续回合
    public int silver = 0;

    #endregion
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public float EnemyAction()
    {
        if(actionNoRound >= BattleManager.Instance.turnCount)
        {
            // 跳过回合
            return 0;
        }
        AiTree();
        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        return actionTime;
    }
    public virtual void AiTree()
    {
        currentState.EnterState(this);
    }
    #region 受伤
    public void RealHurt(int damage)
    {
        if (data.shield > 0)
        {
            if (data.shield > damage)
            {
                data.shield -= damage;
                return;
            }
            else
            {
                damage -= data.shield;
                data.shield = 0;
            }
        }
        else
        {
            data.hp -= damage;
        }
        if (data.hp <= 0)
        {
            data.hp = 0;
            isDead = true;
        }
    }
    public void Hurt(int damage)
    {
        data.armor = data.armor - damage > 0 ? data.armor - damage : 0;
        RealHurt(data.armor - damage > 0 ? data.armor - damage : damage - data.armor);

    }

    #endregion

    #region Buff
    public void SilverBuff(int turnCount)
    {
        if (turnCount <= silver)
        {
            RealHurt(2);
        }
    }


    #endregion

    #region 生命周期

    public void TurnEnd()
    {
        Player.Instance.data.armor = 0;
    }
    #endregion


}
