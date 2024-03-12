using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/SkillData", order = int.MaxValue)]
public class Skill : ScriptableObject
{
    [SerializeField]
    private float skillCoolTime;
    public float SkillCoolTime { get => skillCoolTime;}
    [SerializeField]
    private int damage;
    public int Damage { get => damage;}
}
