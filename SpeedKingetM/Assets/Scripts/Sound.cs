using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;//音效名稱

    public AudioClip clip;//放入音效

    [Range(0f, 1f)]
    public float volume;//音量

    [Range(0.1f, 3f)]
    public float pitch;//音調

    public bool loop;//是否循環撥放

    [HideInInspector]
    public AudioSource source;
}
