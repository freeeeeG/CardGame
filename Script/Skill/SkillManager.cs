using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class SkillManager : MonoBehaviour
{
    bool effect103 = false;
    bool effect111 = false;
    bool effect112 = false;

    bool effect204 = false;
    bool effect209 = false;

    bool effect301 = false;
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
        if(effect209)
        {
            damage = damage * 2;
            GetHurt(damage);
            effect209 = false;
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
        if(effect204)
        {
            Player.Instance.Hurt(4);
            effect204 = false;
        }
        if(effect209)
        {
            damage = damage * 2;
            effect209 = false;
        }
        // 攻击
        Enemy.Instance.Hurt(damage);
        // 攻击后判断

    }

    public void CardDestroy()
    {
        //TODO:销毁   
    }
    public void CardReturn()
    {
        //TODO:返回手牌
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
    #region 木
    public void id_201() //楮
    {
        Player.Instance.data.mo += 2;
        BattleManager.Instance.DrawCard(0,1);
        BattleManager.Instance.DrawCard(0,1);
        Debug.Log("楮");
    }
    public void id_202() //梢
    {
        if(BattleManager.Instance.playerHandsCounts==1 && Player.Instance.data.mo==0)
        {
            BattleManager.Instance.playerDeckList = BattleManager.Instance.currentPlayerUsedCards;
            CardDestroy();
        }
        Debug.Log("梢");

    }
    public void id_203()//梧
    {
        Enemy.Instance.weak = BattleManager.Instance.turnCount += 2;
        Debug.Log("梧");
    }
    public void id_204()//椎
    {
        GetHurt(4);
        Player.Instance.data.mo +=Player.Instance.data.mo;
        effect204 = true;
        Debug.Log("椎");
    }
    public void id_205()//根
    {
        Enemy.Instance.parasitic = BattleManager.Instance.turnCount += 2;
        Debug.Log("根");
    }
    public void id_206()//梗
    {
        Enemy.Instance.healBlock = BattleManager.Instance.turnCount += 2;
        Debug.Log("梗");
    }
    public void id_207()//极
    {
        //TODO:下一张牌打出俩次
        Debug.Log("极 还没做好");
    }
    public void id_208()//柘
    {
        //效果暂无
        Debug.Log("柘");
    }
    public void id_209()//標
    {
        effect209 = true;
        CardDestroy();
        Debug.Log("標");
    }
    public void id_210()//样
    {
        //TODO:本回合不消耗mo，改为消耗血*5
        Debug.Log("样");
    }
    #endregion
    #region 火
    public void id_301() //烽
    {
        Enemy.Instance.onFire += BattleManager.Instance.turnCount += 3;
        
    }
    public void id_302()//炮
    {
        GetHurt(18);
    }
    public void id_303() //炮
    {
        GetHurt(32);
    }
    public void id_304()//炒
    {
        GetHurt(6);
        CardReturn();
    }
    public void id_305()//烟
    {
        
        //TODO：给敌人加张牌，敌人抽到后扣血
    }
    public void id_306()//焐
    {
        GetHurt(BattleManager.Instance.playerHandsCounts*2);
    }
    public void id_307()//炖
    {
        GetHurt(6 + BattleManager.Instance.currentPlayerUsedCards.Count * 2);
    }

    #endregion
    #region 水
    public void id_401() //沙
    {
        Enemy.Instance.blind = BattleManager.Instance.turnCount += 1;
    }
    public void id_402()//冷
    {
        Enemy.Instance.frostbite = BattleManager.Instance.turnCount += 2;
        //将卡牌效果修改为：敌人每次攻击就会受到2伤害，持续2回合
    }
    public void id_403()//消
    {
        GetHurt(Player.Instance.data.mo * 7);
        Player.Instance.data.mo = 0;
    }
    public void id_404()//焦
    {
        GetHurt(12);
    }
    #endregion
    #region 土
    public void id_501() //埂
    {
        Player.Instance.data.armor += 16;
        //TODO:一回合后添加的16护甲消失
    }
    public void id_502()//垠
    {
        //TODO:去除负面效果
    }
    public void id_503()//圾
    {
        BattleManager.Instance.playerDeckList.Clear();

    }
    public void id_504()//坎
    {
        Player.Instance.data.armor += 5;
        //TODO:一回合后添加的5点护甲消失
        Player.Instance.kan = BattleManager.Instance.turnCount += 2;
    }
    public void id_505()//堆
    {
        Player.Instance.data.armor =Player.Instance.data.armor*2;
    }
    #endregion

}
