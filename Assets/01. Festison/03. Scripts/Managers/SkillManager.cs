using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : SingleTon<SkillManager>
{
    [Header("스킬의 기본 데이터")] public SkillSo skillSo;

    [Header("스킬 설명 이미지")]
    public Image skillImage;
    public Image outLineImage;
    public TextMeshProUGUI[] skillExplanation = new TextMeshProUGUI[6];
    public TextMeshProUGUI useSkillText;

    [Header("스킬 저장 이미지")]
    public Image[] skillSlotimages = new Image[10];
    public Image[] useSkillSlotimages = new Image[4];


    [Header("스킬데이터를 저장할 임시 변수")] public int skillNumber = -1;

    public void Start()
    {
        for (int i = 0; i < skillSo.skillData.Count; i++)
        {
            skillSo.skillData[i].isUseSkill = false;
            if (skillSo.skillData[i].isGetSkill)
            {
                skillSlotimages[i].color = Color.white;
            }
        }

    }

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
                switch (skillSo.skillData[i].skillGrade)
                {
                    case SkillGrade.A:
                        outLineImage.color = Color.red;
                        skillExplanation[2].color = Color.red;
                        skillExplanation[4].color = Color.red;
                        break;
                    case SkillGrade.B:
                        outLineImage.color = new Color(134 / 255f, 0, 255 / 255f);
                        skillExplanation[2].color = new Color(134, 0, 255);
                        skillExplanation[4].color = new Color(134, 0, 255);
                        break;
                    case SkillGrade.C:
                        outLineImage.color = new Color(71, 138, 255);
                        skillExplanation[2].color = new Color(71, 138, 255);
                        skillExplanation[4].color = new Color(71, 138, 255);
                        break;
                    case SkillGrade.D:
                        outLineImage.color = Color.gray;
                        skillExplanation[2].color = Color.gray;
                        skillExplanation[4].color = Color.gray;
                        break;
                }

                skillSo.skillData[i].count++;
                skillImage.sprite = skillSo.skillData[i].icon;
                skillExplanation[0].text = skillSo.skillData[i].skillGrade.ToString();
                skillExplanation[1].text = "Lv" + skillSo.skillData[i].level.ToString();
                skillExplanation[2].text = skillSo.skillData[i].skillName;
                skillExplanation[3].text = skillSo.skillData[i].skillExplanation + "   " + "데미지 : " + skillSo.skillData[i].damage.ToString();
                skillExplanation[5].text = skillSo.skillData[i].count.ToString() + " / " + "2";
                useSkillText.text = "장착될 슬롯 : " + (currentIndex + 1).ToString() + "번 째 ";
                skillSo.skillData[i].isGetSkill = true;

                if (skillSo.skillData[i].isGetSkill)
                {
                    skillSlotimages[i].color = Color.white;
                }

                skillNumber = i;
                break;
            }
        }
    }

    [Header("현재 스킬 슬롯 인덱스 위치")] public int currentIndex = 0;

    // 스킬 장착 로직
    public void EquipmentSkill()
    {
        if (skillSo.skillData[skillNumber].isGetSkill)
        {
            bool isEquipmentSkill = false;

            for (int i = 0; i < useSkillSlotimages.Length; i++)
            {
                if (useSkillSlotimages[i].sprite == skillSo.skillData[skillNumber].icon)
                {
                    isEquipmentSkill = true;
                    break;
                }
            }

            if (!isEquipmentSkill)
            {
                // 다른 스킬을 장착하기 전에 모든 스킬의 isUseSkill을 false로 설정
                foreach (var skill in skillSo.skillData)
                {
                    skill.isUseSkill = false;
                }

                if (currentIndex < useSkillSlotimages.Length)
                {
                    SkillIndex(currentIndex);
                    currentIndex++;
                }
            }

            if (currentIndex == 4)
            {
                currentIndex = 0;
            }
        }
    }

    private void SkillIndex(int index)
    {
        skillSo.skillData[skillNumber].isUseSkill = true;
        useSkillSlotimages[index].enabled = true;
        useSkillSlotimages[index].sprite = skillSo.skillData[skillNumber].icon;
    }

    [Header("스킬 이펙트를 저장 할 리스트")] public List<GameObject> SkillEffect = new List<GameObject>();

    public void UseSkill(int i)
    {
        switch (useSkillSlotimages[i].sprite.name)
        {
            case "Lightning":
                SkillEffect[0].SetActive(true);
                break;
            case "Fireball":
                SkillEffect[1].SetActive(true);
                break;
            case "Dendro":
                SkillEffect[2].SetActive(true);
                break;
            case "Hydro":
                SkillEffect[3].SetActive(true);
                break;
        }
    }
}
