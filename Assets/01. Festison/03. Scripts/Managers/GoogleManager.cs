using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loginText;

    #region ���� �α��� �κ�
    public void GPGSLogIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // �α��� ����
            string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
            string userId = PlayGamesPlatform.Instance.GetUserId();

            loginText.text = "�α��� ���� " + displayName + " / " + userId;

            SceneManager.LoadScene("FestisonUIScene");
            
        }
        else
        {
            // �α��� ����
            loginText.text = "�α��� ����";
            SceneManager.LoadScene("FestisonUIScene");
        }
    }
    #endregion
}

