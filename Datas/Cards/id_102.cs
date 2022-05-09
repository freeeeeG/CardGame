using UnityEngine;

public class id_102 : CombineCard
{



    public id_102() : base(102, "铃", 0, 0, "金", "令", "暂无")
    {

    }

    public override void Skill()
    {

        Enemy.Instance.data.hp -= 1;
        Debug.Log("我打出了铃");
        //TODO:

    }
}
