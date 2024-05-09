using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

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
    #region ��ų �Ŵ��� ����
    [Header("��ų�� �⺻ ������")] public SkillSo skillSo;
    public Sprite[] skilldataImage = new Sprite[10];

    [Header("��ų ���� �̹���")]
    public Image skillImage;
    public Image outLineImage;
    public TextMeshProUGUI[] skillExplanation = new TextMeshProUGUI[6];
    public TextMeshProUGUI useSkillText;

    [Header("��ų ���� �̹���")]
    public Image[] skillSlotimages = new Image[10];
    public Image[] useSkillSlotimages = new Image[4];

    [Header("��ų�����͸� ������ �ӽ� ����")] public int skillNumber = -1;
    #endregion

    public void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "skillData.json");
        LoadSkillData();
        InitializeSkillSlots();
    }

    #region ��ų ������ ����
    private string dataPath;

    private void InitializeSkillSlots()
    {
        for (int i = 0; i<skillSo.skillData.Count; i++)
        {
            skillSo.skillData[i].isUseSkill = false;
            if (skillSo.skillData[i].isGetSkill)
            {
                skillSlotimages[i].color = Color.white;
            }
        }
    }

    // ������ ����
    public void SaveSkillData()
    {
        string json = JsonUtility.ToJson(new SkillDataContainer(skillSo.skillData), true);
        File.WriteAllText(dataPath, json);
    }

    // ������ �ε�
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
    #endregion

    #region ��ų �̺�Ʈ ����
    public void DrawGrade()
    {
        if (DataManager.instance.PlayerItem.gem < 100)
        {
            outLineImage.color = Color.gray;
            skillExplanation[2].color = Color.gray;
            skillExplanation[4].color = Color.gray;
            skillImage.sprite = ItemManager.Instance.defaultItemSprite;
            skillExplanation[0].text = "D";
            skillExplanation[2].text = "���� �����մϴ�.";
            skillExplanation[3].text = "���� �����մϴ�.";
        }

        if (DataManager.Instance.PlayerItem.gem >= 100)
        {
            float totalProbability = 0;

            // Ȯ���� �� ���� ����մϴ�.
            foreach (SkillData data in skillSo.skillData)
            {
                totalProbability += data.percent;
            }

            // �������� ���õ� ���� ���� ����� �����մϴ�.
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
                    skillExplanation[3].text = skillSo.skillData[i].skillExplanation + "   " + "������ : " + skillSo.skillData[i].damage.ToString();
                    skillExplanation[5].text = skillSo.skillData[i].count.ToString() + " / " + skillSo.skillData[i].LevelUpCount.ToString();
                    useSkillText.text = "������ ���� : " + (currentIndex + 1).ToString() + "�� ° ";
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

    [Header("���� ��ų ���� �ε��� ��ġ")] public int currentIndex = 0;

    // ��ų ���� ����
    public void EquipmentSkill()
    {
        if (skillSo.skillData[skillNumber].isGetSkill && skillExplanation[3].text != "���� �����մϴ�.")
        {
            bool isEquipmentSkill = false;

            for (int i = 0; i < useSkillSlotimages.Length; i++)
            {
                if (useSkillSlotimages[i].sprite == skilldataImage[skillNumber])
                {
                    skillSo.skillData[skillNumber].isUseSkill = true;
                    isEquipmentSkill = true;
                    break;
                }
            }

            if (!isEquipmentSkill)
            {
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
    public void SkillLevelUp()
    {
        if (skillSo.skillData[skillNumber].count >= skillSo.skillData[skillNumber].LevelUpCount && DataManager.Instance.PlayerItem.gold >= 10000 && skillExplanation[3].text != "���� �����մϴ�.")
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
            skillExplanation[3].text = skillSo.skillData[skillNumber].skillExplanation + "  " + "������ : " + skillSo.skillData[skillNumber].damage.ToString();
            skillExplanation[5].text = skillSo.skillData[skillNumber].count.ToString() + " / " + skillSo.skillData[skillNumber].LevelUpCount.ToString();
            useSkillText.text = "������ ���� : " + (currentIndex + 1).ToString() + "�� ° ";
        }
    }

    [Header("��ų ����Ʈ�� ���� �� ����Ʈ")] public List<GameObject> SkillEffect = new List<GameObject>();
    [Header("��ų ��ũ��Ʈ ����Ʈ")] public List<Skill> skills = new List<Skill>();
    [Header("��ų��Ÿ�� �̹���")] public Image[] skillCoolTimeImg;
    [Header("��ų��Ÿ�� �ؽ�Ʈ")] public TextMeshProUGUI[] skillcoolTimeText;
    public void UseSkill(int i)
    {
        switch (useSkillSlotimages[i].sprite.name)
        {
            case "Lightning":
                if (skillSo.skillData[0].isUseSkill)
                {
                    SkillEffect[0].SetActive(true);
                    StartCoroutine(SkillCoolTimeCo(skillSo.skillData[0].coolTime, i));
                }
                break;
            case "FireBall":
                if (skillSo.skillData[1].isUseSkill)
                {
                    SkillEffect[1].SetActive(true);
                    StartCoroutine(SkillCoolTimeCo(skillSo.skillData[1].coolTime, i));
                }
                break;
            case "Dendro":
                if (skillSo.skillData[2].isUseSkill)
                {
                    SkillEffect[2].SetActive(true);
                    StartCoroutine(SkillCoolTimeCo(skillSo.skillData[2].coolTime, i));
                }
                break;
            case "Hydro":
                if (skillSo.skillData[3].isUseSkill)
                {
                    SkillEffect[3].SetActive(true);
                    StartCoroutine(SkillCoolTimeCo(skillSo.skillData[3].coolTime, i));
                }
                break;  
        }   
    }

    //��Ÿ�� �ڷ�ƾ
    public IEnumerator SkillCoolTimeCo(float time, int skillIndex)
    {
        float coolTime = time;
        skillSo.skillData[skillIndex].isUseSkill = false;
        skillCoolTimeImg[skillIndex].enabled = true;
        skillCoolTimeImg[skillIndex].fillAmount = 1;
        while (coolTime > 0)
        {
            yield return new WaitForSeconds(1f);
            coolTime -= 1.0f;
            skillcoolTimeText[skillIndex].text = $"{coolTime:F0}";
            skillCoolTimeImg[skillIndex].fillAmount = coolTime / time;
        }

        skillcoolTimeText[skillIndex].text = " ";
        skillCoolTimeImg[skillIndex].enabled = false;
        skillSo.skillData[skillIndex].isUseSkill = true;
    }

    #endregion
}
