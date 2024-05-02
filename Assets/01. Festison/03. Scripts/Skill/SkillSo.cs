using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillGrade { A, B, C, D }

[System.Serializable]
public class SkillData
{
    public SkillGrade skillGrade;
    public string skillName;
    public string skillExplanation;
    public int damage;
    public float coolTime;
    public int level;
    public int count;
    public int LevelUpCount = 2;
    public float percent;
    public bool isGetSkill;
    public bool isUseSkill;
}

[CreateAssetMenu(menuName = "Player/SkillData", order = int.MaxValue)]
public class SkillSo : ScriptableObject
{
    public List<SkillData> skillData = new List<SkillData>();
}
