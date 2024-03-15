using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{

    public SkillSo skillSo;

    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI[] skillExplanation = new TextMeshProUGUI[5];

    public void SetCardStat()
    {
        if (skillSo != null)
        {
            skillImage.sprite = skillSo.skillData[1].icon;
            skillExplanation[0].text = skillSo.skillData[1].skillGrade.ToString();
            skillExplanation[1].text = "Lv" + skillSo.skillData[1].level.ToString();
            skillExplanation[2].text = skillSo.skillData[1].skillName;
            skillExplanation[3].text = skillSo.skillData[1].skillExplanation+('\n')+"µ¥¹ÌÁö : " + skillSo.skillData[1].damage.ToString();
        }

    }

    public void Gacha()
    {
        var gachaPer = UnityEngine.Random.Range(1, 101);

        for (int i = 0; i < skillSo.skillData.Count; i++)
        {
            if (skillSo.skillData[i].percent<=gachaPer)
            {
                //Reward(skillSo.skillData[i].skillGrade);
                return;
            }
        }
    }
    /*
    public Skill Reward(SkillGrade skillGrade)
    {
        skillSo.skillData 

        return per[]
    }
    */
}
