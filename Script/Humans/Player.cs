using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : Singleton<Player>
{

    public const int _drawCardCount = 3;
    public PlayerData data;
    public bool isDead = false;
    Animator animator;
    public int drawCardCount = 3;
    private void Start()
    {
        animator = GetComponent<Animator>();
        AddProp((data, i) => { data.maxHp += i; }, 1);
        BattleManager.Instance.playerTurnEnd += TurnEnd;

    }


    #region 动画
    public void UseCard(string name, int id, int skillid, float time, GameObject card)
    {
        AnimatorPlay(name, skillid, 0);
        AnimatorPlay(name, 0, 1);
    }

    public void AnimatorPlay(string name, int num, float time)
    {
        StartCoroutine(WaitForSeconds(name, num, time));

    }
    IEnumerator WaitForSeconds(string name, int num, float time)
    {
        {
            yield return new WaitForSeconds(time);
            animator.SetInteger(name, num);
        }
    }
    #endregion

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
    public void AddProp(Action<PlayerData, int> func, int valve)
    {
        func(data, valve);
    }
    #endregion


    #region 生命周期

    public void TurnEnd()
    {
        
    }

    #endregion



}