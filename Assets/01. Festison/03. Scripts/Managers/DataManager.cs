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

[System.Serializable]
public class PlayerItem
{
    public int currentEnergy;
    public readonly int maxEnergy = 2;
    public int gold;
    public int gem;
    public int waveData;
}

public class DataManager : SingleTon<DataManager>
{
    [Header("�÷��̾� �ڿ� �ؽ�Ʈ")] public TextMeshProUGUI[] itemText;

    // ������ ������
    public PlayerItem PlayerItem = new PlayerItem();
    public PlayerModel playerData = new PlayerModel();

    // ������ ���
    private string playerpath, itempath;

    protected override void Awake()
    {
        base.Awake();

        // ����Ƽ���� �ڵ����� �������ִ� ������ ��η� ���
        playerpath = Application.persistentDataPath + "/PlayerDatasave.txt";
        itempath = Application.persistentDataPath + "/ItemDatasave.txt";
    }
    private void Start()
    {
        LoadItemData();
        LoadPlayerData();
        WaveManager.Instance.InvokeWaveStart();
    }

    private void Update()
    {
        StartCoroutine(SavePlayerDataCo());
        StartCoroutine(SaveItemDataCo());

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
        playerData.criticalDamage = 125f;
        PlayerItem.currentEnergy = 1;
        PlayerItem.gold = 0;
        PlayerItem.gem = 0;
        PlayerItem.waveData = 0;
    }

    public IEnumerator SavePlayerDataCo()
    {
        string playerData = JsonUtility.ToJson(this.playerData, true);
        File.WriteAllText(playerpath, playerData);
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator SaveItemDataCo()
    {
        string playerItem = JsonUtility.ToJson(this.PlayerItem, true);
        File.WriteAllText(itempath, playerItem);
        yield return new WaitForSeconds(2f);
    }

    public void SavePlayerData()
    {
        string playerDataString = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(playerpath, playerDataString);
    }

    public void SaveItemData()
    {
        string playerItemString = JsonUtility.ToJson(PlayerItem, true);
        File.WriteAllText(itempath, playerItemString);
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(playerpath))
        {
            InitData();
            return;
        }

        string data = File.ReadAllText(playerpath);
        playerData = JsonUtility.FromJson<PlayerModel>(data);
    }

    public void LoadItemData()
    {
        if (!File.Exists(itempath))
        {
            InitData();
            return;
        }

        string Itemdata = File.ReadAllText(itempath);
        PlayerItem = JsonUtility.FromJson<PlayerItem>(Itemdata);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        SavePlayerData();
        SaveItemData();
#else
        Application.Quit();
#endif
    }
    #endregion

    #region UI ���� �ڵ�
    private float decreasetime = 100f;
    public void UpdateText()
    {
        itemText[0].text = PlayerItem.currentEnergy + " / " + PlayerItem.maxEnergy;
        itemText[1].text = PlayerItem.gold.ToString();
        itemText[2].text = PlayerItem.gem.ToString(); ;
        itemText[4].text = "Stage " + (PlayerItem.waveData + 1).ToString();

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
