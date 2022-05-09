using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "Player Data/PlayerStatusData", order = 0)]
public class PlayerStatusData : ScriptableObject
{
    const int maxLevel = 10;
    public int level;
    public int maxExp = 100;
    public int exp;
    public int maxHp;
    public int tempMaxHp;
    public int hp;

    public int money;
    public int cardNum;
    public int maxCardNum;
    public int mo;
    public int tempMaxMo;
    public int maxMo;
    public int shield;
    public int armor; 


    public void Init()
    {
        maxMo = 10;
        maxHp = 100;
        maxMo = 5;
        maxCardNum = 5;
        level = 1;
        exp = 0;
        money = 0;
        cardNum = 0;
    }

    public void BattleStart()
    {
        hp = maxHp;
        mo = maxMo;
        cardNum = maxCardNum;
        shield = 0;
        armor = 0;
    }

}
