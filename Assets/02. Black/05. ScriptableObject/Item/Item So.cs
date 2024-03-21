using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/ItemData", order = int.MaxValue)]
public class ItemSo : ScriptableObject
{
    public enum ItemType
    {
        Sword,
        Armor,
        Shoes,
        Gloves
    }
    public enum ItemRating
    {
        A,
        B,
        C,
        D
    }
    public ItemType itemType;
    public ItemRating itemRating;
    public int lv;
    public string itemName;
    public float value;
    public int price;
    public Sprite sprite;
}
