using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Festioson;

// ���� ����
// 1. ������ �����͸� �����´�.
// 2. �����͸� ����ȭ�� ���� ���̽����� ��ȯ�Ѵ�.
// 3. ���̽��� �ܺο� �����Ѵ�.

// �ҷ����� ���
// 1. �ܺο� ����� ���̽��� �����´�.
// 2. ���̽��� ������ȭ�� ���� �����ͷ� ��ȯ��Ų��.
// 3. �ҷ��� �����͸� ����Ѵ�.

public class DataManager : SingleTon<DataManager>
{
    WaitForSeconds waitForSeconds = new WaitForSeconds(5f);

    public SkillSo skilldata;

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
    }


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
}
