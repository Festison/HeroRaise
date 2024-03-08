using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{
    [Header("첫 클릭 확인 UI")] public GameObject dotUi;
    [Header("메뉴 UI")] public GameObject menu;
    private bool isSetmenu=false;

    public void OnClickDot()
    {
        dotUi.SetActive(false);

        if (!isSetmenu)
        {
            menu.SetActive(true);
            isSetmenu = true;
        }
        else
        {
            menu.SetActive(false);
            isSetmenu = false;
        }
        
    }

}
