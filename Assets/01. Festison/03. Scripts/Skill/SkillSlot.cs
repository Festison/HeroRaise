using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlot : Slot
{
    public void SetItem(int i)
    {
        switch (SkillManager.Instance.skillSo.skillData[i].skillGrade)
        {
            case SkillGrade.A:
                SkillManager.Instance.outLineImage.color = Color.red;
                SkillManager.Instance.skillExplanation[2].color = Color.red;
                SkillManager.Instance.skillExplanation[4].color = Color.red;
                break;
            case SkillGrade.B:
                SkillManager.Instance.outLineImage.color = new Color(134, 0, 255);
                SkillManager.Instance.skillExplanation[2].color = new Color(134, 0, 255);
                SkillManager.Instance.skillExplanation[4].color = new Color(134, 0, 255);
                break;
            case SkillGrade.C:
                SkillManager.Instance.outLineImage.color = new Color(71, 138, 255);
                SkillManager.Instance.skillExplanation[2].color = new Color(71, 138, 255);
                SkillManager.Instance.skillExplanation[4].color = new Color(71, 138, 255);
                break;
            case SkillGrade.D:
                SkillManager.Instance.outLineImage.color = Color.gray;
                SkillManager.Instance.skillExplanation[2].color = Color.gray;
                SkillManager.Instance.skillExplanation[4].color = Color.gray;
                break;
        }

        SkillManager.Instance.skillImage.sprite = SkillManager.Instance.skillSo.skillData[i].icon;
        SkillManager.Instance.skillExplanation[0].text = SkillManager.Instance.skillSo.skillData[i].skillGrade.ToString();
        SkillManager.Instance.skillExplanation[1].text = "Lv" + SkillManager.Instance.skillSo.skillData[i].level.ToString();
        SkillManager.Instance.skillExplanation[2].text = SkillManager.Instance.skillSo.skillData[i].skillName;
        SkillManager.Instance.skillExplanation[3].text = SkillManager.Instance.skillSo.skillData[i].skillExplanation + ('\n') + "µ¥¹ÌÁö : " + SkillManager.Instance.skillSo.skillData[i].damage.ToString();
        SkillManager.Instance.useSkillText.text = "ÀåÂøµÉ ½½·Ô : " + (SkillManager.Instance.currentIndex + 1).ToString() + "¹ø Â° ";

        if (SkillManager.Instance.skillSo.skillData[i].isGetSkill)
            SkillManager.Instance.skillNumber = i;
    }
}
