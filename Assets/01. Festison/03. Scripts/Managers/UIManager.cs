using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{
    [Header("ù Ŭ�� Ȯ�� UI")] public GameObject dotUi;
    [Header("�޴� UI")] public GameObject menu;
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
