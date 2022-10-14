using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoginData
{
    public string emailString;//Email inputField 的text
    public string passwordString;//Password inputField 的text

    public LoginData(LoginUI loginMenuUI)
    {
        emailString = loginMenuUI.emailIputfieldL.text;//等於本次LoginUI的emailL的值
        passwordString = loginMenuUI.passwordIputfieldL.text;//等於本次LoginUI的passwordL的值
    }
}
