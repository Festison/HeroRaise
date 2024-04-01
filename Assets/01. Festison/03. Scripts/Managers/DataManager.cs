using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Festioson;
using TMPro;

// ���� ����
// 1. ������ �����͸� �����´�.
// 2. �����͸� ����ȭ�� ���� ���̽����� ��ȯ�Ѵ�.
// 3. ���̽��� �ܺο� �����Ѵ�.

// �ҷ����� ���
// 1. �ܺο� ����� ���̽��� �����´�.
// 2. ���̽��� ������ȭ�� ���� �����ͷ� ��ȯ��Ų��.
// 3. �ҷ��� �����͸� ����Ѵ�.

public class PlayerItem
{
    public int currentEnergy;
    public int maxEnergy;
    public int gold;
    public int gem;
}

public class DataManager : SingleTon<DataManager>
{
    public TextMeshProUGUI[] itemText;
    WaitForSeconds waitForSeconds = new WaitForSeconds(5f);

    public SkillSo skilldata;

    public PlayerItem PlayerItem = new PlayerItem() { currentEnergy = 1, maxEnergy = 2, gold = 0, gem = 0 };
    public PlayerModel playerData = new PlayerModel()
    {
        level = 1,
        hp = 100,
        maxHp = 100,
        damage = 10,
        attackSpeed = 1.0f,
        criticalChance = 5.0f,
        criticalDamage = 1.25f
    };

    private float decreasetime = 100f;

    private string path;

    protected override void Awake()
    {
        base.Awake();
        // ����Ƽ���� �ڵ����� �������ִ� ������ ��η� ���
        path = Application.persistentDataPath + "/Datasave.txt";
    }
    private void Start()
    {
        string json;
        json = Path.Combine(Application.dataPath + "/01. Festison/06. Data/", "playerData.json");
        string playerData = JsonUtility.ToJson(this.playerData, true);
        File.WriteAllText(json, playerData);
        LoadData();
    }

    private void Update()
    {
        StartCoroutine(SaveDataCo());

        if (PlayerItem.currentEnergy < 2)
        {
            ChangeTime();
        }

    }


    #region ������ ����
    public IEnumerator SaveDataCo()
    {
        string playerData = JsonUtility.ToJson(this.playerData, true);
        File.WriteAllText(path, playerData);
        Debug.Log(playerData);
        Debug.Log("������ ����");
        yield return waitForSeconds;
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerModel>(data);
    }
    #endregion

    #region UI ���� �ڵ�
    public void UpdateText()
    {
        itemText[0].text = PlayerItem.currentEnergy + " / " + PlayerItem.maxEnergy;
        itemText[1].text = PlayerItem.gold.ToString();
        itemText[2].text = PlayerItem.gem.ToString(); ;

        int minutes = (int)decreasetime / 60; // ��
        int seconds = (int)decreasetime % 60; // ��

        itemText[3].text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
    }

    public void ChangeTime()
    {
        Debug.Log("�ð� ����");
        decreasetime -= Time.deltaTime; 

        if (0.1f >= decreasetime) // 5�и��� ����
        {
            PlayerItem.currentEnergy++; // �������� 1 ������ŵ�ϴ�.
            decreasetime = 100f; // Ÿ�̸Ӹ� �����մϴ�.
        }
        EventManager.OnPlayerStateChange();
    }


    private void OnEnable()
    {
        EventManager.OnPlayerStateChange += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerStateChange -= UpdateText;
    }
    #endregion
}
