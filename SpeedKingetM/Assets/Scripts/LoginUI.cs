using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SceneManagement;
public class LoginUI : MonoBehaviour
{

    [Header("放置Email InputField(Register)")]
    [SerializeField] InputField emailIputfieldR;

    [Header("放置Password InputField(Register)")]
    [SerializeField] InputField passwordIputfieldR;

    [Header("放置Email InputField(Login)")]
    public InputField emailIputfieldL;

    [Header("放置Password InputField(Login)")]
    public InputField passwordIputfieldL;

    [Header("放置EmailInputField(Reset) ")]
    [SerializeField] InputField emailIputfieldReset;

    [Header("放置Dropdown")]
    [SerializeField] Dropdown dropdown;
    private void Awake()
    {
        LoginData data = SaveLoad.LoadLoginData();
        emailIputfieldL.text = data.emailString;
        passwordIputfieldL.text = data.passwordString;
    }


    public void GuestLogin() //遊客登入按鈕
    {
        StartCoroutine(GuestLoginRoutine());
        
    }

    public void UserLogin()//WhiteLabel登入按鈕 
    {
        SaveLoad.SaveLoginData(this);
        StartCoroutine(WhiteLabelUserLogin());       
    }

    public void UserSignUp() //WhiteLabel註冊按鈕
    {
        //loginMessage.text = language.processingM;
        StartCoroutine(WhiteLabelUserRegister());
    }

    public void UserResetPassword() //WhiteLabel重置密碼按鈕
    {
        //loginMessage.text = language.processingM;
        StartCoroutine(PasswordReset());
    }

    public void Open(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void Return(GameObject ui)
    {
        ui.SetActive(false);
    }

    IEnumerator GuestLoginRoutine()//遊客登入
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("登入成功");
                //loginMessage.text = "登入成功";
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
                SceneManager.LoadScene("Menu");
            }
            else
            {
                Debug.Log("協程失敗");
                //loginMessage.text = language.startSessionFailM;
                //loginMessage.text = language.startSessionFailM;
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator WhiteLabelUserLogin() //WhiteLabel登入
    {
        bool done = false;
        LootLockerSDKManager.WhiteLabelLogin(emailIputfieldL.text, passwordIputfieldL.text, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("登入失敗");
                //loginMessage.text = language.failLoginM;
                //loginMessage.text = language.failLoginM;
                done = true;
            }
            else
            {
                Debug.Log("登入成功,進入中...");
                //loginMessage.text = language.loginingM;
            }
            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                    Debug.Log("啟動LootLocker session 失敗");
                    //loginMessage.text = language.startSessionFailM;
                    //loginMessage.text = language.failLoginM;
                    done = true;
                }
                else
                {
                    Debug.Log("啟動LootLocker session 成功");
                    PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                    //loginMessage.text = language.sessionStartM;
                    done = true;
                    SceneManager.LoadScene("Menu");
                }
            });
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator WhiteLabelUserRegister()//WhiteLabel註冊
    {

        bool done = false;
        LootLockerSDKManager.WhiteLabelSignUp(emailIputfieldR.text, passwordIputfieldR.text, (response) =>
        {
            if (!response.success)
            {
                Debug.Log(response.Error);
                //loginMessage.text = language.registerFailM;
                done = true;
            }
            else
            {
                Debug.Log("註冊成功");
                //loginMessage.text = language.registerSuccessM;
                done = true;
            }

        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator PasswordReset()//重設密碼 
    {
        bool done = false;
        LootLockerSDKManager.WhiteLabelRequestPassword(emailIputfieldReset.text, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("重置密碼失敗");
                //loginMessage.text = language.resetPasswordFailM;
                done = true;
            }
            else
            {
                Debug.Log("已寄信到信箱");
                //loginMessage.text = language.resetPasswordSuccessM;
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
