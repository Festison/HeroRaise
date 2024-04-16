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
    public Item(string itemGrade, string Type, string Name, string Explain, string Stat, bool isUsing, string Percent)
    {
        this.ItemGrade = itemGrade;
        this.Type = Type;
        this.Name = Name;
        this.Explain = Explain;
        this.Stat = Stat;
        this.isUsing = isUsing;
        this.Percent = Percent;
    }

    public string ItemGrade, Type, Name, Explain, Stat;
    public string Percent;
    public bool isUsing;

}

public class ItemManager : SingleTon<ItemManager>
{
    [Header("아이템의 기본 데이터")]
    public TextAsset ItemDatabase;
    public List<Item> allItemList, equimentItemList;
    public Sprite[] itemSprites;
    private string itempath;

    [Header("아이템 설명 이미지")]
    public Image[] EquimentImg;
    public Image ItemImage;
    public Image ItemoutLineImage;
    public TextMeshProUGUI[] ItemExplanation = new TextMeshProUGUI[4];
    private Item item; // 뽑은 아이템을 저장할 변수

    private void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split(',');
            allItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE", row[6]));
        }

        itempath = Application.persistentDataPath + "/MyItemText.txt";
        SaveItemData();
        LoadItemData();
    }

    #region 데이터 저장 로직
    private void InitItemList()
    {

    }
    private void SaveItemData()
    {
        File.WriteAllText(itempath, "����");
    }

    private void LoadItemData()
    {
        if (!File.Exists(itempath))
        {
            InitItemList();
        }
        string itemdata = File.ReadAllText(itempath);
    }
    #endregion

    #region 아이템 뽑기및 장착 로직
    public void DrawItem()
    {
        float totalProbability = 0;

        // 확률의 총 합을 계산합니다.
        foreach (Item data in allItemList)
        {
            float parseData = (float.Parse(data.Percent));
            totalProbability += parseData;
        }

        // 랜덤으로 선택된 값에 따라 등급을 선택합니다.
        float randomValue = UnityEngine.Random.Range(0f, totalProbability);
        float cumulativeProb = 0;

        for (int i = 0; i < allItemList.Count; i++)
        {
            float parseData = (float.Parse(allItemList[i].Percent));
            cumulativeProb += parseData;

            if (randomValue <= cumulativeProb)
            {
                switch (allItemList[i].ItemGrade)
                {
                    case "A":
                        ItemoutLineImage.color = Color.red;
                        ItemExplanation[0].color = Color.red;
                        ItemExplanation[1].color = Color.red;
                        break;
                    case "B":
                        ItemoutLineImage.color = new Color(134 / 255f, 0, 255 / 255f);
                        ItemExplanation[0].color = new Color(134 / 255f, 0, 255 / 255f);
                        ItemExplanation[1].color = new Color(134 / 255f, 0, 255 / 255f);
                        break;
                    case "C":
                        ItemoutLineImage.color = new Color(71 / 255f, 138 / 255f, 255 / 255f);
                        ItemExplanation[0].color = new Color(71 / 255f, 138 / 255f, 255 / 255f);
                        ItemExplanation[1].color = new Color(71 / 255f, 138 / 255f, 255 / 255f);
                        break;
                    case "D":
                        ItemoutLineImage.color = Color.gray;
                        ItemExplanation[0].color = Color.gray;
                        ItemExplanation[1].color = Color.gray;
                        break;
                }

                ItemImage.sprite = itemSprites[i];
                ItemExplanation[0].text = allItemList[i].ItemGrade;
                ItemExplanation[1].text = allItemList[i].Name;
                ItemExplanation[2].text = allItemList[i].Explain;
                //ItemExplanation[3].text = allItemList[i].gold;
                item = allItemList[i];
                break;
            }
        }
    }

    public void EquimentItem()
    {      
        switch (item.Type)
        {
            case "Weapon":
                if (!equimentItemList.Contains(item)) equimentItemList[0] = item;
                if (equimentItemList[0]!=null) DataManager.Instance.playerData.damage -= int.Parse(equimentItemList[0].Stat);
                EquimentImg[0].sprite = ItemImage.sprite;
                int parseDamage = (int.Parse(item.Stat));
                DataManager.Instance.playerData.damage += parseDamage;
                break;
            case "Helmet":
                if (!equimentItemList.Contains(item)) equimentItemList[1] = item;
                if (equimentItemList[1] != null) DataManager.Instance.playerData.criticalChance -= float.Parse(equimentItemList[1].Stat);
                EquimentImg[1].sprite = ItemImage.sprite;
                int parseCriticalChance = (int.Parse(item.Stat));
                DataManager.Instance.playerData.criticalChance += parseCriticalChance;                
                break;
            case "Armor":
                if (!equimentItemList.Contains(item)) equimentItemList[2] = item;
                if (equimentItemList[2] != null) DataManager.Instance.playerData.criticalDamage -= float.Parse(equimentItemList[2].Stat);
                EquimentImg[2].sprite = ItemImage.sprite;
                int parseCriticalDamage = (int.Parse(item.Stat));
                DataManager.Instance.playerData.criticalDamage += parseCriticalDamage;             
                break;
            case "Shoes":
                if (!equimentItemList.Contains(item)) equimentItemList[3] = item;
                if (equimentItemList[3] != null) DataManager.Instance.playerData.attackSpeed -= float.Parse(equimentItemList[3].Stat);
                EquimentImg[3].sprite = ItemImage.sprite;
                int parseAttackSpeed = (int.Parse(item.Stat));
                DataManager.Instance.playerData.attackSpeed += parseAttackSpeed;           
                break;
        }
    }
    #endregion

}