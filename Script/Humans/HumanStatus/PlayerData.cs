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
    public int tempMaxHp {
        get {
            return datas.tempMaxHp;
        }
        set {
            datas.tempMaxHp = value;
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

    public int mo {
        get {
            return datas.mo;
        }
        set {
            datas.mo = value;
        }
    }
    public int maxMo {
        get {
            return datas.maxMo;
        }
        set {
            datas.maxMo = value;
        }
    }
    public int tempMaxMo {
        get {
            return datas.tempMaxMo;
        }
        set {
            datas.tempMaxMo = value;
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

    public int armor {
        get {
            return datas.armor;
        }
        set {
            datas.armor = value;
        }
    }

}
