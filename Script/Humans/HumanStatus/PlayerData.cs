using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public PlayerStatusData datas;

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
    public int attack {
        get {
            return datas.attack;
        }
        set {
            datas.attack = value;
        }
    }
    public int speed {
        get {
            return datas.speed;
        }
        set {
            datas.speed = value;
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
    public int cardNum {
        get {
            return datas.cardNum;
        }
        set {
            datas.cardNum = value;
        }
    }
    public int maxCardNum {
        get {
            return datas.maxCardNum;
        }
        set {
            datas.maxCardNum = value;
        }
    }
    public int level {
        get {
            return datas.level;
        }
        set {
            datas.level = value;
        }
    }
    public int exp {
        get {
            return datas.exp;
        }
        set {
            datas.exp = value;
        }
    }
    public int maxExp {
        get {
            return datas.maxExp;
        }
        set {
            datas.maxExp = value;
        }
    }

    

}
