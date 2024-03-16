using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillGrade { A, B, C, D }

[System.Serializable]
public class Skill
{
    public SkillGrade skillGrade;
    public string skillName;
    public string skillExplanation;
    public int damage;
    public float coolTime;
    public int level;
    public int count;
    public float percent;
    public bool isGetSkill;
    public bool isUseSkill;
    public Sprite icon;

    public Skill()
    {

    }

    public void Equipment()
    {

    }

    public void UpGrade()
    {

    }
}

[CreateAssetMenu(menuName = "Player/SkillData", order = int.MaxValue)]
public class SkillSo : ScriptableObject
{
    public List<Skill> skillData = new List<Skill>();

    public void Init()
    {

    }
}
