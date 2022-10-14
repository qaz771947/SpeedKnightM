using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour
{
    public static void SaveLoginData(LoginUI loginUI)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/login.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        LoginData data = new LoginData(loginUI);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LoginData LoadLoginData()
    {
        string path = Application.persistentDataPath + "/login.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LoginData data = formatter.Deserialize(stream) as LoginData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("未找到在" + path + "此路徑的儲存檔案");
            return null;
        }
    }

    public static void SaveVolumeValue(Menu menu)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path2 = Application.persistentDataPath + "/volume.data";
        FileStream stream = new FileStream(path2, FileMode.Create);

        Volume volume = new Volume(menu);
        formatter.Serialize(stream, volume);
        stream.Close();
    }

    public static Volume LoadVolumeValue()
    {
        string path2 = Application.persistentDataPath + "/volume.data";
        if (File.Exists(path2))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path2, FileMode.Open);
            Volume volume = formatter.Deserialize(stream) as Volume;
            stream.Close();
            return volume;
        }
        else
        {
            Debug.LogError("未找到在" + path2 + "此路徑的儲存檔案");
            return null;
        }
    }



}
