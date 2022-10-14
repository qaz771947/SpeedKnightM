using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Volume
{
    public float BGMVolume;//BGM slider的音量數值
    public float soundVolume;//Sound slider的音量數值

   public Volume(Menu menu) 
    {
        BGMVolume = menu.BGMSlider.value;
        soundVolume = menu.soundSlider.value;
    }
}
