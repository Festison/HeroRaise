using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Player/PlayerData", order = int.MaxValue)]
public class PlayerStatSo : ScriptableObject
{
    public int hp;
    public int damage;
    public float range;
    public int lv;
    public float atkSpeed;
}
