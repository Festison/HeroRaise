using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> target) => this.target = target;
    public List<T> target;
}

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
    public List<Item> allItemList;
    public List<Item> equimentItemList;
    public Sprite[] itemSprites;
    private string itempath;

    [Header("아이템 설명 이미지")]
    public Image[] EquimentImg;
    public Image ItemImage;
    public Sprite defaultItemSprite;
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
        LoadItemData();
        ItemSpriteInit();
    }

    private void Update()
    {
        StartCoroutine((SaveItemDataCo()));     
    }

    #region 데이터 저장 로직
    private void InitItemList()
    {
        equimentItemList.Add(new Item("D", "Weapon", "나무 단검", "공격력이 5상승한다.", "5", true, "5"));
        equimentItemList.Add(new Item("D", "Helmet", "나무 투구", "크리티컬 확률이 5퍼센트 상승한다.", "10", true, "5"));
        equimentItemList.Add(new Item("D", "Armor", "나무 갑옷", "크리티컬 데미지가 10퍼센트 상승한다.", "10", true, "5"));
        equimentItemList.Add(new Item("D", "Shoes", "나무 신발", "공격속도가 10퍼센트 상승한다", "10", true, "5"));
    }

    public IEnumerator SaveItemDataCo()
    {
        string ItemJson = JsonUtility.ToJson(new Serialization<Item>(equimentItemList), true);
        File.WriteAllText(itempath, ItemJson);
        yield return new WaitForSeconds(2f);
    }

    private void LoadItemData()
    {
        if (!File.Exists(itempath))
        {
            InitItemList();
            return;
        }
        string itemdata = File.ReadAllText(itempath);
        equimentItemList = JsonUtility.FromJson<Serialization<Item>>(itemdata).target;
    }
    #endregion

    #region 아이템 뽑기및 장착 로직
    public void ItemSale()
    {
        if (ItemExplanation[2].text != "젬이 부족합니다.")
        {
            DataManager.Instance.PlayerItem.gold += 10000;
        }
    }
    public void DrawItem()
    {
        if (DataManager.instance.PlayerItem.gem < 100)
        {
            ItemoutLineImage.color = Color.gray;
            ItemExplanation[0].color = Color.gray;
            ItemExplanation[1].color = Color.gray;
            ItemImage.sprite = defaultItemSprite;
            ItemExplanation[0].text = "D";
            ItemExplanation[1].text = "젬이 부족합니다.";
            ItemExplanation[2].text = "젬이 부족합니다.";
        }

        if (DataManager.Instance.PlayerItem.gem >= 100)
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
                    DataManager.Instance.PlayerItem.gem -= 100;
                    break;

                    
                }
            }
        }
    }

    public void EquimentItem()
    {
            switch (item.Type)
            {
                case "Weapon":
                    if (equimentItemList[0].isUsing) DataManager.Instance.playerData.damage -= int.Parse(equimentItemList[0].Stat);
                    if (!equimentItemList.Contains(item)) equimentItemList[0] = item;
                    equimentItemList[0].isUsing = true;
                    EquimentImg[0].sprite = ItemImage.sprite;
                    int parseDamage = (int.Parse(item.Stat));
                    DataManager.Instance.playerData.damage += parseDamage;
                    break;
                case "Helmet":
                    if (equimentItemList[1].isUsing) DataManager.Instance.playerData.criticalChance -= float.Parse(equimentItemList[1].Stat);
                    if (!equimentItemList.Contains(item)) equimentItemList[1] = item;
                    equimentItemList[1].isUsing = true;
                    EquimentImg[1].sprite = ItemImage.sprite;
                    int parseCriticalChance = (int.Parse(item.Stat));
                    DataManager.Instance.playerData.criticalChance += parseCriticalChance;
                    break;
                case "Armor":
                    if (equimentItemList[2].isUsing) DataManager.Instance.playerData.criticalDamage -= float.Parse(equimentItemList[2].Stat);
                    if (!equimentItemList.Contains(item)) equimentItemList[2] = item;
                    equimentItemList[2].isUsing = true;
                    EquimentImg[2].sprite = ItemImage.sprite;
                    int parseCriticalDamage = (int.Parse(item.Stat));
                    DataManager.Instance.playerData.criticalDamage += parseCriticalDamage;
                    break;
                case "Shoes":
                    if (equimentItemList[3].isUsing) DataManager.Instance.playerData.attackSpeed -= (float.Parse(equimentItemList[3].Stat) * 0.01f);
                    if (!equimentItemList.Contains(item)) equimentItemList[3] = item;
                    equimentItemList[3].isUsing = true;
                    EquimentImg[3].sprite = ItemImage.sprite;
                    int parseAttackSpeed = (int.Parse(item.Stat));
                    DataManager.Instance.playerData.attackSpeed += (parseAttackSpeed * 0.01f);
                    break;         
        }
    }

    private Dictionary<string, int> itemSpriteIndexMap;
    private void ItemSpriteInit()
    {
        itemSpriteIndexMap = new Dictionary<string, int>
        {
            {"초보자의 장검", 4},
            {"초보자의 투구", 5},
            {"초보자의 갑옷", 6},
            {"초보자의 신발", 7},
            {"성기사의 장검", 8},
            {"성기사의 투구", 9},
            {"성기사의 갑옷", 10},
            {"성기사의 신발", 11},
            {"황제의 장검", 12},
            {"황제의 투구", 13},
            {"황제의 갑옷", 14},
            {"황제의 신발", 15}
        };

        for (int i = 0; i < equimentItemList.Count && i < EquimentImg.Length; i++)
        {
            Item item = equimentItemList[i];
            if (item != null && itemSpriteIndexMap.TryGetValue(item.Name, out int spriteIndex))
            {
                EquimentImg[i].sprite = itemSprites[spriteIndex];
            }
        }
    }

    #endregion

}