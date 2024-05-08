using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : SingleTon<UIManager>
{
    // [Header("첫 클릭 확인 UI")] public GameObject dotUi;
    [Header("캐릭터 HP 슬라이더")] public Image hpImage;
    [Header("메뉴 UI")] public GameObject menu;
    [Header("메뉴 UI 버튼")] public Button menuBtn;
    [SerializeField] private bool isSetmenu = false;

    [Header("캐릭터 버튼")] public Button infoBtn;

    [Header("스킬 UI")] public GameObject[] skillUI;

    [Header("보스 입장 테스트")] public Button bossBtn;

    public int index;

    public void Start()
    {
        infoBtn.Select();
        SceneManager.activeSceneChanged += activeScene;
    }

    public void Update()
    {
        HpLerpUI();
    }

    public void HpLerpUI()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, DataManager.Instance.playerData.Hp / DataManager.Instance.playerData.MaxHp, Time.deltaTime * 10f);
    }

    public void OnClickMenu()
    {
        //dotUi.SetActive(false);

        if (!isSetmenu)
        {
            menuBtn.enabled = false;
            menu.SetActive(true);
            menu.transform.DOScale(1.0f, 0.5f).OnComplete(SetActiveMenu);
        }
        else if (isSetmenu)
        {
            menuBtn.enabled = false;
            menu.transform.DOScale(0f, 0.5f).OnComplete(SetFalseMenu);
        }
    }
    public void SetActiveMenu()
    {
        isSetmenu = true;
        menuBtn.enabled = true;
    }
    public void SetFalseMenu()
    {
        menu.SetActive(false);
        isSetmenu = false;
        menuBtn.enabled = true;
    }

    public void BossStage(int index)
    {
        this.index = index;
        SceneManager.LoadScene("BossStageScene");
    }

    public void activeScene(Scene s1, Scene s2)
    {
        Debug.Log("씬 이동");
    }
}