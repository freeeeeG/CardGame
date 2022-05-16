using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatusData", menuName = "Enemy Data/EnemyStatusData", order = 0)]
public class EnemyStatusData : ScriptableObject
{
    public int maxHp;
    public int hp;
    public int money;
    public int armor;
    public int shield;
    public int attack;
    public void Init()
    {
        hp = maxHp;
    }
}
