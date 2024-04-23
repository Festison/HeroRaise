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
    [Header("플레이어 자원 텍스트")] public TextMeshProUGUI[] itemText;

    // 데이터 생성자
    public PlayerItem PlayerItem = new PlayerItem();
    public PlayerModel playerData = new PlayerModel();

    // 데이터 경로
    private string playerpath, itempath;

    protected override void Awake()
    {
        base.Awake();

        // 유니티에서 자동으로 생성해주는 폴더를 경로로 사용
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


    #region 데이터 관리
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

    #region UI 관련 코드
    private float decreasetime = 100f;
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
