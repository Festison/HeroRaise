using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Festioson;
using TMPro;
using Veco;

// 저장 순서
// 1. 저장할 데이터를 가져온다.
// 2. 데이터를 직렬화를 통해 제이슨으로 변환한다.
// 3. 제이슨을 외부에 저장한다.

// 불러오는 방법
// 1. 외부에 저장된 제이슨을 가져온다.
// 2. 제이슨을 역직렬화를 통해 데이터로 변환시킨다.
// 3. 불러온 데이터를 사용한다.

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
    public TextMeshProUGUI[] itemText;

    public SkillSo skilldata;

    public PlayerItem PlayerItem = new PlayerItem() { currentEnergy = 1, gold = 0, gem = 0, waveData = 0 };
    public PlayerModel playerData = new PlayerModel();

    private float decreasetime = 100f;

    private string path;
    string json;
    string testJson;

    protected override void Awake()
    {
        base.Awake();

        // 유니티에서 자동으로 생성해주는 폴더를 경로로 사용
        path = Application.persistentDataPath + "/Datasave.txt";
    }
    private void Start()
    {
        
        json = Path.Combine(Application.dataPath + "/01. Festison/06. Data/", "playerData.json");
        string playerData = JsonUtility.ToJson(this.playerData, true);
        File.WriteAllText(json, playerData);

        testJson = Path.Combine(Application.dataPath + "/01. Festison/06. Data/", "playerItemData.json");
        string playerItemData = JsonUtility.ToJson(this.PlayerItem, true);
        File.WriteAllText(testJson, playerItemData);


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


    #region 데이터 관리
    public void InitData()
    {
        playerData.level = 1;
        playerData.hp = 100;
        playerData.maxHp = 100;
        playerData.damage = 10;
        playerData.attackSpeed = 1.0f;
        playerData.criticalChance = 5.0f;
        playerData.criticalDamage = 1.25f;
        SavePlayerData();
        LoadPlayerData();
    }

    public IEnumerator SavePlayerDataCo()
    {
        string playerData = JsonUtility.ToJson(this.playerData, true);
        File.WriteAllText(json, playerData);
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator SaveItemDataCo()
    {
        string playerItem = JsonUtility.ToJson(this.PlayerItem, true);
        File.WriteAllText(testJson, playerItem);
        yield return new WaitForSeconds(2f);
    }

    public void SavePlayerData()
    {
        string playerDataString = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(json, playerDataString);
    }

    public void SaveItemData()
    {
        string playerItemString = JsonUtility.ToJson(PlayerItem, true);
        File.WriteAllText(testJson, playerItemString);
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(json))
        {
            InitData();
            return;
        }

        string data = File.ReadAllText(json);
        playerData = JsonUtility.FromJson<PlayerModel>(data);       
    }

    public void LoadItemData()
    {
        string Itemdata = File.ReadAllText(testJson);
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
        SaveData();
#endif
    }
    #endregion

    #region UI 관련 코드
    public void UpdateText()
    {
        itemText[0].text = PlayerItem.currentEnergy + " / " + PlayerItem.maxEnergy;
        itemText[1].text = PlayerItem.gold.ToString();
        itemText[2].text = PlayerItem.gem.ToString(); ;
        itemText[4].text = "Stage " + (PlayerItem.waveData + 1).ToString();

        int minutes = (int)decreasetime / 60; // 분
        int seconds = (int)decreasetime % 60; // 초

        itemText[3].text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
    }

    public void ChangeTime()
    {
        decreasetime -= Time.deltaTime;

        if (0.1f >= decreasetime) // 5분마다 실행
        {
            PlayerItem.currentEnergy++; // 에너지를 1 증가시킵니다.
            decreasetime = 100f; // 타이머를 리셋합니다.
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
