using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/ItemData", order = int.MaxValue)]
public class ItemSo : ScriptableObject
{
    public int grade;
    public string itemName;
    public int atkvalue;
    public float atkRange;
    public float atkSpeedValue;
    public int price;
    public Sprite sprite;
    public int lv;
}
