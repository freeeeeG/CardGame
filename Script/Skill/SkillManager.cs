using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class SkillManager : MonoBehaviour
{
    bool effect103 = false;
    bool effect111 = false;
    bool effect112 = false;
    public void GetRealHurt(int damage)
    {
        // 伤害计算
        if (effect103)
        {
            damage *= 2;
            effect103 = true;
        }
        // 攻击前判断
        if (effect112)
        {
            Player.Instance.data.armor += damage > (Enemy.Instance.data.shield + Enemy.Instance.data.armor) ? damage - (Enemy.Instance.data.shield + Enemy.Instance.data.armor) : 0; 
            effect112 = false;
        }
        // 攻击
        if (effect111)
        {
            damage = damage * 2;
            GetHurt(damage);
        }
        else
        {
            Enemy.Instance.RealHurt(damage);
        }
        // 攻击后判断

    }
    public void GetHurt(int damage)
    {
        // 伤害计算
        
        
        if (effect103)
        {
            Enemy.Instance.RealHurt(damage);
            effect103 = false;
        }
        // 攻击前判断
        if (effect112)
        {
            Player.Instance.data.armor += damage > (Enemy.Instance.data.shield + Enemy.Instance.data.armor) ? damage - (Enemy.Instance.data.shield + Enemy.Instance.data.armor) : 0;  
            effect112 = false;
        }
        // 攻击
        Enemy.Instance.Hurt(damage);
        // 攻击后判断

    }

    public void CardDestroy()
    {
        //TODO:销毁   
    }
    #region 金
    public void id_101() //银
    {
        Debug.Log("银");
        Enemy.Instance.silver = BattleManager.Instance.turnCount + 2;
    }
    public void id_102() //铃
    {
        Debug.Log("铃");
        GetHurt(1);
    }
    public void id_103() //锋
    {
        Debug.Log("锋");
        effect103 = true;
    }
    public void id_104() //铛
    {
        Debug.Log("铛");

    }
    public void id_105() //锥
    {
        Debug.Log("锥");
        GetRealHurt(12);
    }
    public void id_106() //锥
    {
        Debug.Log("锥");
        GetRealHurt(20);

    }
    public void id_107() //销
    {
        GetRealHurt(Enemy.Instance.data.armor * 2);
        // 销毁
        CardDestroy();
    }
    public void id_108() //钦
    {
        Player.Instance.data.armor += Enemy.Instance.data.armor / 2;
    }
    public void id_109() //锢
    {
        Enemy.Instance.actionNoRound = BattleManager.Instance.turnCount;
    }
    public void id_110() //锅
    {
        GetRealHurt((Player.Instance.data.tempMaxHp - Player.Instance.data.hp) / 2);
    }
    public void id_111() //钝
    {
        effect111 = true;
    }
    public void id_112() //镀
    {
        effect112 = true;
        GetRealHurt(8);
    }
    public void id_113() //镖
    {
    }
    public void id_114() //镖
    {
        GetRealHurt(2);
        GetRealHurt(2);
        GetRealHurt(2);
    }
    #endregion





}
