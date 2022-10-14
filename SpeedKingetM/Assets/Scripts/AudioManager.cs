using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("放入音效")]
    public Sound[] sounds;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)//確認只存在一個AudioManager
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) //將Sound屬性質給予AudioSource物件
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()//遊戲開始時,撥放背景音樂
    {
        Play("BGM");
    }

    public void Play(string name) //播放指定音效
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("音效" + name + "不存在");
            return;
        }
        s.source.Play();
    }

    public void Volume(string name, float value) //調整指定音效之音量
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = value;
    }
}
