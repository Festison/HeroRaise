using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : Slot
{
    public override void SetItem(int i)
    {
        switch (ItemManager.Instance.equimentItemList[i].ItemGrade)
        {
            case "A":
                ItemManager.Instance.ItemoutLineImage.color = Color.red;
                ItemManager.Instance.ItemExplanation[0].color = Color.red;
                ItemManager.Instance.ItemExplanation[1].color = Color.red;
                break;
            case "B":
                ItemManager.Instance.ItemoutLineImage.color = new Color(134 / 255f, 0, 255 / 255f);
                ItemManager.Instance.ItemExplanation[0].color = new Color(134 / 255f, 0, 255 / 255f);
                ItemManager.Instance.ItemExplanation[1].color = new Color(134 / 255f, 0, 255 / 255f);
                break;
            case "C":
                ItemManager.Instance.ItemoutLineImage.color = new Color(71 / 255f, 138 / 255f, 255 / 255f);
                ItemManager.Instance.ItemExplanation[0].color = new Color(71 / 255f, 138 / 255f, 255 / 255f);
                ItemManager.Instance.ItemExplanation[1].color = new Color(71 / 255f, 138 / 255f, 255 / 255f);
                break;
            case "D":
                ItemManager.Instance.ItemoutLineImage.color = Color.gray;
                ItemManager.Instance.ItemExplanation[0].color = Color.gray;
                ItemManager.Instance.ItemExplanation[1].color = Color.gray;
                break;
        }

        ItemManager.Instance.ItemImage.sprite = ItemManager.Instance.itemSprites[i];
        ItemManager.Instance.ItemExplanation[0].text = ItemManager.Instance.equimentItemList[i].ItemGrade;
        ItemManager.Instance.ItemExplanation[1].text = ItemManager.Instance.equimentItemList[i].Name;
        ItemManager.Instance.ItemExplanation[2].text = ItemManager.Instance.equimentItemList[i].Explain;
    }

}
