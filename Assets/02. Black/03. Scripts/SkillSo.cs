using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Skill
{
    public string skillName;
    public int damage;
    public float coolTime;
    public int level;
    public float weight;
    public Image icon;
}

[CreateAssetMenu(menuName = "Player/SkillData", order = int.MaxValue)]
public class SkillSo : ScriptableObject
{
    public List<Skill> skillData = new List<Skill>();
}
