using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlot : MonoBehaviour
{
    
 
    public void SetItem(int i)
    {
        SkillManager.Instance.outLineImage.color = Color.gray;
        SkillManager.Instance.skillExplanation[2].color = Color.gray;
        SkillManager.Instance.skillExplanation[4].color = Color.gray;
        SkillManager.Instance.skillImage.sprite = SkillManager.Instance.skillSo.skillData[i].icon;
        SkillManager.Instance.skillExplanation[0].text = SkillManager.Instance.skillSo.skillData[i].skillGrade.ToString();
        SkillManager.Instance.skillExplanation[1].text = "Lv" + SkillManager.Instance.skillSo.skillData[i].level.ToString();
        SkillManager.Instance.skillExplanation[2].text = SkillManager.Instance.skillSo.skillData[i].skillName;
        SkillManager.Instance.skillExplanation[3].text = SkillManager.Instance.skillSo.skillData[i].skillExplanation + ('\n') + "µ¥¹ÌÁö : " + SkillManager.Instance.skillSo.skillData[i].damage.ToString();

        if (SkillManager.Instance.skillSo.skillData[i].isGetSkill)
            SkillManager.Instance.skillNumber = i;
    }
}
