using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public EnemyStatusData datas;
    //TODO: 数据读取
    public int hp {
        get {
            return datas.hp;
        }
        set {
            datas.hp = value;
        }
    }
    public int maxHp {
        get {
            return datas.maxHp;
        }
        set {
            datas.maxHp = value;
        }
    }
    public int money {
        get {
            return datas.money;
        }
        set {
            datas.money = value;
        }
    }
    public int armor {
        get {
            return datas.armor;
        }
        set {
            datas.armor = value;
        }
    }
    public int shield {
        get {
            return datas.shield;
        }
        set {
            datas.shield = value;
        }
    }
    public int attack {
        get {
            return datas.attack;
        }
        set {
            datas.attack = value;
        }
    }
    
}
