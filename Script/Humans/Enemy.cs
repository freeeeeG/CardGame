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
    #region Buff判断
    public bool ifParasitic = false;   //判断敌人是否处于寄生状态
    public bool ifHealBlock = false;  //判断敌人是否处于梗塞状态
    public bool ifOnFire = false;     //判断敌人是否处于烽火状态
    public bool ifBlind = false;     //判断敌人是否处于失明状态
    public bool ifFrostbite = false; //判断敌人是否处于冻伤状态
    #endregion
    #region Buff持续回合
    public int silver = 0;
    public int weak = 0;
    public int parasitic = 0;
    public int healBlock = 0;
    public int onFire = 0;
    public int blind = 0;
    public int frostbite = 0;
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
        if(ifParasitic)
        {
            Player.Instance.data.hp += damage;  //如果处于寄生状态，玩家还会回复伤害值的生命值
        }
        if(ifOnFire)
        {
            damage += 2;
        }
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
        if(ifParasitic)
        {
            Player.Instance.data.hp += damage;//如果处于寄生状态，玩家还会回复伤害值的生命值
        }
        if (ifOnFire)
        {
            damage += 2;
        }
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
    public void WeakBuff(int turnCount)  //虚弱
    {
        if(turnCount<=weak)
        {
            data.attack = (int)(data.attack / 2);
        }else if(turnCount>weak)
        {
            data.attack = data.attack * 2;
        }
    }
    public void ParasiticBuff(int turnCount) //寄生
    {
        if(turnCount<=parasitic)
        {
            ifParasitic = true;
        }
        else
        {
            ifParasitic = false;
        }
    }
    public void HealBlock(int turnCount) //梗塞
    {
        if(turnCount<=healBlock)
        {
            ifHealBlock = true;
        }
        else
        {
            ifHealBlock = false;
        }
    }
    public void OnFire(int turnCount)  //烽火
    {
        if(turnCount<=onFire)
        {
            ifOnFire = true;
        }
        else
        {
            ifOnFire = false;
        }
    }
    public void Blind(int turnCount) //失明
    {
        if(turnCount<=blind)
        {
            ifBlind = true;
        }
        else
        {
            ifBlind = false;
        }
    }
    public void Frostbite(int turnCount) //冻伤
    {
        if(turnCount<= frostbite)
        {
            ifFrostbite = true;
        }
        else
        {
            ifFrostbite = false;
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
