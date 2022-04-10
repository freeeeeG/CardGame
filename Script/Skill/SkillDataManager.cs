using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class SkillDataManager : Singleton<SkillDataManager>
{
    // Start is called before the first frame update
    List<string> datas = new List<string>();
    List<List<string>> skillDatas = new List<List<string>>();


    
    void Start()
    {
        string skillPath = Application.dataPath + "/Datas/Skills.csv";
        datas = File.ReadAllLines(skillPath).ToList();
        foreach (var item in datas)
        {
            string[] data = item.Split(',');
            skillDatas.Add(data.ToList());
        }
        foreach (var item in skillDatas)
        {
            foreach (var i in item)
            {
                Debug.Log(i);
            }
        }
    }
}
