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

    #region 구글 로그인 부분
    public void GPGSLogIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // 로그인 성공
            string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
            string userId = PlayGamesPlatform.Instance.GetUserId();

            loginText.text = "로그인 성공 " + displayName + " / " + userId;

            SceneManager.LoadScene("FestisonUIScene");
            
        }
        else
        {
            // 로그인 실패
            loginText.text = "로그인 실패";
            SceneManager.LoadScene("FestisonUIScene");
        }
    }
    #endregion
}

