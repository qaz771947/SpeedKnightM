using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoginData
{
    public string emailString;//Email inputField ��text
    public string passwordString;//Password inputField ��text

    public LoginData(LoginUI loginMenuUI)
    {
        emailString = loginMenuUI.emailIputfieldL.text;//���󥻦�LoginUI��emailL����
        passwordString = loginMenuUI.passwordIputfieldL.text;//���󥻦�LoginUI��passwordL����
    }
}
