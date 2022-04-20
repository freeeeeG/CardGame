using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "Player Data/PlayerStatusData", order = 0)]
public class PlayerStatusData : ScriptableObject 
{
    public int level;
    const int maxLevel = 10;
    public int exp;
    public int maxExp = 100;
    public int maxHp;
    public int hp;
    public int attack;
    public int speed;
    public int money;
    public int cardNum;
    public int maxCardNum;
    
    public void Init(){
        level = 1;
        exp = 0;
        hp = maxHp;
        attack = 10;
        speed = 10;
        money = 0;
        cardNum = 0;
        maxCardNum = 5;
    }
    
}
