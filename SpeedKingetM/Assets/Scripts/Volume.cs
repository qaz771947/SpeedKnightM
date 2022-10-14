using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Volume
{
    public float BGMVolume;//BGM slider�����q�ƭ�
    public float soundVolume;//Sound slider�����q�ƭ�

   public Volume(Menu menu) 
    {
        BGMVolume = menu.BGMSlider.value;
        soundVolume = menu.soundSlider.value;
    }
}
