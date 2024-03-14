using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVtoSO
{
    private static string skillCSVPath = "/CSV/SkillData.Text";

    [MenuItem("Utilities/Generate SkillData")]
    public static void GenerateEnemies()
    {
        string[] csvText = File.ReadAllLines(Application.dataPath + skillCSVPath);
        SkillSo skillSo = ScriptableObject.CreateInstance<SkillSo>();

        foreach (var text in csvText)
        {
            string[] stats = text.Split(',');

            if (stats.Length != 4) Debug.LogError("This File Data Count isn't 4 count");

            Skill skill = new Skill();

            skill.skillName = stats[0];
            skill.damage = int.Parse(stats[1]);
            skill.coolTime = float.Parse(stats[2]);
            skill.level = int.Parse(stats[3]);

            skillSo.skillData.Add(skill);
        }
        AssetDatabase.CreateAsset(skillSo, "Assets/CSV/SkillData.asset");
        AssetDatabase.SaveAssets();
    }
}
