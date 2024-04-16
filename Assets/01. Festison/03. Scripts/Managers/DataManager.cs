using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Festioson;
using TMPro;
using Veco;

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
    public int waveData;
}

public class DataManager : SingleTon<DataManager>
{
    public TextMeshProUGUI[] itemText;

    public SkillSo skilldata;

    public PlayerItem PlayerItem = new PlayerItem() { currentEnergy = 1, maxEnergy = 2, gold = 0, gem = 0, waveData = 0 };
    public PlayerModel playerData = new PlayerModel();

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

        string testJson = Path.Combine(Application.dataPath + "/01. Festison/06. Data/", "playerItemData.json");
        string playerItemData = JsonUtility.ToJson(this.PlayerItem, true);
        File.WriteAllText(testJson, playerItemData);

        LoadData();
        WaveManager.Instance.InvokeWaveStart();
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
    public void InitData()
    {
        playerData.level = 1;
        playerData.hp = 100;
        playerData.maxHp = 100;
        playerData.damage = 10;
        playerData.attackSpeed = 1.0f;
        playerData.criticalChance = 5.0f;
        playerData.criticalDamage = 1.25f;
        SaveData();
        LoadData();
    }

    public IEnumerator SaveDataCo()
    {
        string playerData = JsonUtility.ToJson(this.playerData, true);
        string playerItem = JsonUtility.ToJson(this.PlayerItem, true);
        File.WriteAllText(path, playerData);
        File.WriteAllText(path, playerItem);
        Debug.Log("������ ����");
        yield return new WaitForSeconds(2f);
    }

    public void SaveData()
    {      
        string playerDataString = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path, playerDataString);
        string playerItemString = JsonUtility.ToJson(PlayerItem, true);
        File.WriteAllText(path, playerItemString);
    }

    public void LoadData()
    {
        if (!File.Exists(path))
        {
            InitData();
            return;
        }

        string data = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerModel>(data);
        string Itemdata = File.ReadAllText(path);
        PlayerItem = JsonUtility.FromJson<PlayerItem>(Itemdata);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        SaveData();
#else
        Application.Quit();
        SaveData();
#endif
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
