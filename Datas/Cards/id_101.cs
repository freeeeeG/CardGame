using UnityEngine;

public class id_101 : CombineCard
{



    public id_101() : base(101, "银", 0,0
, "金", "艮", "对目标造成水银")
    {

    }

    public override void Skill()
    {
        Debug.Log("银");
        //TODO:水银（造成：2穿甲伤害/回合，持续：3回合）

    }
}
