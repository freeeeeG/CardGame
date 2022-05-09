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
    
}
