using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SkillDataContainer
{
    public List<SkillData> skillData;
    public SkillDataContainer(List<SkillData> skillData)
    {
        this.skillData = skillData;
    }
}

public class SkillManager : SingleTon<SkillManager>
{
    #region 스킬 매니저 변수
    [Header("스킬의 기본 데이터")] public SkillSo skillSo;
    public Sprite[] skilldataImage = new Sprite[10];

    [Header("스킬 설명 이미지")]
    public Image skillImage;
    public Image outLineImage;
    public TextMeshProUGUI[] skillExplanation = new TextMeshProUGUI[6];
    public TextMeshProUGUI useSkillText;

    [Header("스킬 저장 이미지")]
    public Image[] skillSlotimages = new Image[10];
    public Image[] useSkillSlotimages = new Image[4];

    [Header("스킬데이터를 저장할 임시 변수")] public int skillNumber = -1;
    #endregion

    private string dataPath;
    public void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "skillData.json");
        LoadSkillData();

        for (int i = 0; i < skillSo.skillData.Count; i++)
        {
            skillSo.skillData[i].isUseSkill = false;
            if (skillSo.skillData[i].isGetSkill)
            {
                skillSlotimages[i].color = Color.white;
            }
        }    
    }

    // 데이터 저장
    public void SaveSkillData()
    {
        string json = JsonUtility.ToJson(new SkillDataContainer(skillSo.skillData), true);
        File.WriteAllText(dataPath, json);
    }

    // 데이터 로드
    public void LoadSkillData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            SkillDataContainer loadedData = JsonUtility.FromJson<SkillDataContainer>(json);
            skillSo.skillData = loadedData.skillData;
        }
        else
        {
            SaveSkillData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSkillData();
    }
    

#region 스킬 이벤트 로직
public void DrawGrade()
    {
        if (DataManager.instance.PlayerItem.gem < 100)
        {
            outLineImage.color = Color.gray;
            skillExplanation[2].color = Color.gray;
            skillExplanation[4].color = Color.gray;
            skillImage.sprite = ItemManager.Instance.defaultItemSprite;
            skillExplanation[0].text = "D";
            skillExplanation[2].text = "젬이 부족합니다.";
            skillExplanation[3].text = "젬이 부족합니다.";
        }

        if (DataManager.Instance.PlayerItem.gem >= 100)
        {
            float totalProbability = 0;

            // 확률의 총 합을 계산합니다.
            foreach (SkillData data in skillSo.skillData)
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
                    skillImage.sprite = skilldataImage[i];
                    skillExplanation[0].text = skillSo.skillData[i].skillGrade.ToString();
                    skillExplanation[1].text = "Lv" + skillSo.skillData[i].level.ToString();
                    skillExplanation[2].text = skillSo.skillData[i].skillName;
                    skillExplanation[3].text = skillSo.skillData[i].skillExplanation + "   " + "데미지 : " + skillSo.skillData[i].damage.ToString();
                    skillExplanation[5].text = skillSo.skillData[i].count.ToString() + " / " + skillSo.skillData[i].LevelUpCount.ToString();
                    useSkillText.text = "장착될 슬롯 : " + (currentIndex + 1).ToString() + "번 째 ";
                    skillSo.skillData[i].isGetSkill = true;

                    if (skillSo.skillData[i].isGetSkill)
                    {
                        skillSlotimages[i].color = Color.white;
                    }

                    skillNumber = i;
                    DataManager.Instance.PlayerItem.gem -= 100;
                    break;
                }
            }       
        }
    }

    [Header("현재 스킬 슬롯 인덱스 위치")] public int currentIndex = 0;

    // 스킬 장착 로직
    public void EquipmentSkill()
    {
        if (skillSo.skillData[skillNumber].isGetSkill && skillExplanation[3].text != "젬이 부족합니다.")
        {
            bool isEquipmentSkill = false;

            for (int i = 0; i < useSkillSlotimages.Length; i++)
            {
                if (useSkillSlotimages[i].sprite == skilldataImage[skillNumber])
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
        useSkillSlotimages[index].sprite = skilldataImage[skillNumber];
    }

    [Header("스킬 이펙트를 저장 할 리스트")] public List<GameObject> SkillEffect = new List<GameObject>();

    public void UseSkill(int i)
    {
        switch (useSkillSlotimages[i].sprite.name)
        {
            case "Lightning":
                SkillEffect[0].SetActive(true);
                break;
            case "FireBall":
                SkillEffect[1].SetActive(true);
                break;
            case "Dendro":
                SkillEffect[2].SetActive(true);
                break;
            case "Hydro":
                SkillEffect[3].SetActive(true);
                break;
            case "IceNova":
                SkillEffect[4].SetActive(true);
                break;
            case "StromPath":
                SkillEffect[5].SetActive(true);
                break;
            case "Healing":
                SkillEffect[6].SetActive(true);
                break;
            case "BlackHole":
                SkillEffect[7].SetActive(true);
                break;
            case "BlackNova":
                SkillEffect[8].SetActive(true);
                break;
        }
    }

    public void SkillLevelUp()
    {
        if (skillSo.skillData[skillNumber].count >= skillSo.skillData[skillNumber].LevelUpCount && DataManager.Instance.PlayerItem.gold >= 10000 && skillExplanation[3].text != "젬이 부족합니다.")
        {
            skillSo.skillData[skillNumber].level += 1;
            skillSo.skillData[skillNumber].damage *= 2;
            skillSo.skillData[skillNumber].LevelUpCount *= 2;
            skillSo.skillData[skillNumber].count = 0;
            DataManager.Instance.PlayerItem.gold -= 10000;

            skillImage.sprite = skilldataImage[skillNumber];
            skillExplanation[0].text = Instance.skillSo.skillData[skillNumber].skillGrade.ToString();
            skillExplanation[1].text = "Lv" + Instance.skillSo.skillData[skillNumber].level.ToString();
            skillExplanation[2].text = skillSo.skillData[skillNumber].skillName;
            skillExplanation[3].text = skillSo.skillData[skillNumber].skillExplanation + "  " + "데미지 : " + skillSo.skillData[skillNumber].damage.ToString();
            skillExplanation[5].text = skillSo.skillData[skillNumber].count.ToString() + " / " + skillSo.skillData[skillNumber].LevelUpCount.ToString();
            useSkillText.text = "장착될 슬롯 : " + (currentIndex + 1).ToString() + "번 째 ";
        }
    }
    #endregion
}
