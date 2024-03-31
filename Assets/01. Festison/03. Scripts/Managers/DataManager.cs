using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Festioson;

// 저장 순서
// 1. 저장할 데이터를 가져온다.
// 2. 데이터를 직렬화를 통해 제이슨으로 변환한다.
// 3. 제이슨을 외부에 저장한다.

// 불러오는 방법
// 1. 외부에 저장된 제이슨을 가져온다.
// 2. 제이슨을 역직렬화를 통해 데이터로 변환시킨다.
// 3. 불러온 데이터를 사용한다.

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
        // 유니티에서 자동으로 생성해주는 폴더를 경로로 사용
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
        Debug.Log("데이터 저장");
        yield return waitForSeconds;
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerModel>(data);
    }
}
