using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public class Item
{
    public Item(string itemGrade, string Type, string Name, string Explain, string Stat, bool isUsing)
    { this.ItemGrade = itemGrade; this.Type = Type; this.Name = Name; this.Explain = Explain; this.Stat = Stat; this.isUsing = isUsing; }



    public string ItemGrade, Type, Name, Explain, Stat;
    public bool isUsing;

}
public class ItemManager : SingleTon<ItemManager>
{
    public TextAsset ItemDatabase;
    public List<Item> allItemList, myItemList;
    private string itempath;

    private void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split(',');
            allItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));
        }
       
        itempath = Application.persistentDataPath + "/MyItemText.txt";
        SaveItemData();
        LoadItemData();
    }

    private void InitItemList()
    {
        myItemList = new List<Item>();
    }
    private void SaveItemData()
    {
        File.WriteAllText(itempath, "гоюл");
    }


    private void LoadItemData()
    {
        if (!File.Exists(itempath))
        {
            InitItemList();
        }
        string itemdata = File.ReadAllText(itempath);
    }
}
