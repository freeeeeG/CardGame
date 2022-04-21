using UnityEngine;

public class id_109 : CombineCard
{



    public id_109() : base(109 , "锢"   ,1,  0, "金"   ,"固"   ,"禁锢：目标下回合无法行动"
)
    {

    }

    public override void Skill()
    {
        Debug.Log("锢");
        //TODO:禁锢：目标下回合无法行动

    }
}
