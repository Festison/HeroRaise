using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Skill
{
    public enum SkillGrade { S, A, B, C, D, E, F }
    public string skillName;
    public int damage;
    public float coolTime;
    public int level;
    public float percent;
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
