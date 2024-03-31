using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class CSVtoSO
{
    //[MenuItem("Utilities/Generate SkillData")]
    public static void GenerateEnemies()
    {
        List<Dictionary<string, object>> skilldata = CSVReader.Read("SkillData");
        SkillSo skillSo = ScriptableObject.CreateInstance<SkillSo>();

        for (int i = 0; i < skilldata.Count; i++)
        {
            Skill skill = new Skill();
            skill.skillGrade = (SkillGrade)skilldata[i]["percent"].GetHashCode();
            skill.skillName = skilldata[i]["skillName"].ToString();
            skill.damage = skilldata[i]["damage"].GetHashCode();
            skill.coolTime = (float)skilldata[i]["coolTime"].GetHashCode();
            skill.level = skilldata[i]["level"].GetHashCode();
            skill.percent = (float)skilldata[i]["percent"].GetHashCode();
            

            skillSo.skillData.Add(skill);
        }
        
        //AssetDatabase.CreateAsset(skillSo, "Assets/01. Festison/06. Data/SkillData.asset");
        //AssetDatabase.SaveAssets();
    }
}
