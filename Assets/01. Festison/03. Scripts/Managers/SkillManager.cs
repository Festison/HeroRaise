using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : SingleTon<SkillManager>
{
    public SkillSo skillSo;

    [SerializeField] private Image skillImage;
    [SerializeField] private Image outLineImage;
    [SerializeField] private TextMeshProUGUI[] skillExplanation = new TextMeshProUGUI[5];

    public void DrawGrade()
    {
        float totalProbability = 0;

        // 확률의 총 합을 계산합니다.
        foreach (Skill data in skillSo.skillData)
        {
            totalProbability += data.percent;
        }

        // 랜덤으로 선택된 값에 따라 등급을 선택합니다.
        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProb = 0;

        for (int i = 0; i < skillSo.skillData.Count; i++)
        {
            cumulativeProb += skillSo.skillData[i].percent;

            if (randomValue <= cumulativeProb)
            {
                Debug.Log(cumulativeProb);
                switch (skillSo.skillData[i].skillGrade)
                {
                    case SkillGrade.A:
                        outLineImage.color = Color.red;
                        skillExplanation[2].color = Color.red;
                        skillExplanation[4].color = Color.red;
                        break;
                    case SkillGrade.B:
                        outLineImage.color = Color.magenta;
                        skillExplanation[2].color = Color.magenta;
                        skillExplanation[4].color = Color.magenta;
                        break;
                    case SkillGrade.C:
                        outLineImage.color = Color.blue;
                        skillExplanation[2].color = Color.blue;
                        skillExplanation[4].color = Color.blue;
                        break;
                    case SkillGrade.D:
                        outLineImage.color = Color.gray;
                        skillExplanation[2].color = Color.gray;
                        skillExplanation[4].color = Color.gray;
                        break;
                }

                skillImage.sprite = skillSo.skillData[i].icon;
                skillExplanation[0].text = skillSo.skillData[i].skillGrade.ToString();
                skillExplanation[1].text = "Lv" + skillSo.skillData[i].level.ToString();
                skillExplanation[2].text = skillSo.skillData[i].skillName;
                skillExplanation[3].text = skillSo.skillData[i].skillExplanation + ('\n') + "데미지 : " + skillSo.skillData[i].damage.ToString();
                break;
            }
        }
    }

}
