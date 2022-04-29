using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusData : ScriptableObject
{
    public int _maxHp;
    public int maxHp;
    public int hp;
    public int _attack;
    public int attack;
    public int money;
    public void Init()
    {
        maxHp = _maxHp;
        hp = maxHp;
    }
}
