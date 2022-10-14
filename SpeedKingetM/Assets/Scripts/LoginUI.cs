using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SceneManagement;
public class LoginUI : MonoBehaviour
{

    [Header("��mEmail InputField(Register)")]
    [SerializeField] InputField emailIputfieldR;

    [Header("��mPassword InputField(Register)")]
    [SerializeField] InputField passwordIputfieldR;

    [Header("��mEmail InputField(Login)")]
    public InputField emailIputfieldL;

    [Header("��mPassword InputField(Login)")]
    public InputField passwordIputfieldL;

    [Header("��mEmailInputField(Reset) ")]
    [SerializeField] InputField emailIputfieldReset;

    [Header("��mDropdown")]
    [SerializeField] Dropdown dropdown;
    private void Awake()
    {
        LoginData data = SaveLoad.LoadLoginData();
        emailIputfieldL.text = data.emailString;
        passwordIputfieldL.text = data.passwordString;
    }


    public void GuestLogin() //�C�ȵn�J���s
    {
        StartCoroutine(GuestLoginRoutine());
        
    }

    public void UserLogin()//WhiteLabel�n�J���s 
    {
        SaveLoad.SaveLoginData(this);
        StartCoroutine(WhiteLabelUserLogin());       
    }

    public void UserSignUp() //WhiteLabel���U���s
    {
        //loginMessage.text = language.processingM;
        StartCoroutine(WhiteLabelUserRegister());
    }

    public void UserResetPassword() //WhiteLabel���m�K�X���s
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

    IEnumerator GuestLoginRoutine()//�C�ȵn�J
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("�n�J���\");
                //loginMessage.text = "�n�J���\";
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
                SceneManager.LoadScene("Menu");
            }
            else
            {
                Debug.Log("��{����");
                //loginMessage.text = language.startSessionFailM;
                //loginMessage.text = language.startSessionFailM;
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator WhiteLabelUserLogin() //WhiteLabel�n�J
    {
        bool done = false;
        LootLockerSDKManager.WhiteLabelLogin(emailIputfieldL.text, passwordIputfieldL.text, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("�n�J����");
                //loginMessage.text = language.failLoginM;
                //loginMessage.text = language.failLoginM;
                done = true;
            }
            else
            {
                Debug.Log("�n�J���\,�i�J��...");
                //loginMessage.text = language.loginingM;
            }
            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                    Debug.Log("�Ұ�LootLocker session ����");
                    //loginMessage.text = language.startSessionFailM;
                    //loginMessage.text = language.failLoginM;
                    done = true;
                }
                else
                {
                    Debug.Log("�Ұ�LootLocker session ���\");
                    PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                    //loginMessage.text = language.sessionStartM;
                    done = true;
                    SceneManager.LoadScene("Menu");
                }
            });
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator WhiteLabelUserRegister()//WhiteLabel���U
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
                Debug.Log("���U���\");
                //loginMessage.text = language.registerSuccessM;
                done = true;
            }

        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator PasswordReset()//���]�K�X 
    {
        bool done = false;
        LootLockerSDKManager.WhiteLabelRequestPassword(emailIputfieldReset.text, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("���m�K�X����");
                //loginMessage.text = language.resetPasswordFailM;
                done = true;
            }
            else
            {
                Debug.Log("�w�H�H��H�c");
                //loginMessage.text = language.resetPasswordSuccessM;
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
