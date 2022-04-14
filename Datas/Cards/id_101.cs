using UnityEngine;

public class id_101 : CombineCard
{



    public id_101() : base(101, "灵魂火焰", 3, "炎", "灵魂火焰", "灵魂火焰")
    {

    }

    public override void Skill()
    {
        Debug.Log("灵魂火焰");
        Player.Instance.playerStatus.hp -= 1;
    }
}
