using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("��J����")]
    public Sound[] sounds;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)//�T�{�u�s�b�@��AudioManager
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) //�NSound�ݩʽ赹��AudioSource����
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()//�C���}�l��,����I������
    {
        Play("BGM");
    }

    public void Play(string name) //������w����
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("����" + name + "���s�b");
            return;
        }
        s.source.Play();
    }

    public void Volume(string name, float value) //�վ���w���Ĥ����q
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = value;
    }
}
