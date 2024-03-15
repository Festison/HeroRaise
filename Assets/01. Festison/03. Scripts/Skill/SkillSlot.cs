using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Image image;

      
    public void SetItem()
    {
        image.sprite = SkillManager.Instance.skillSo.skillData[0].icon;
    }
}
