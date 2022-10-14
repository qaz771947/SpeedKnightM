using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;//���ĦW��

    public AudioClip clip;//��J����

    [Range(0f, 1f)]
    public float volume;//���q

    [Range(0.1f, 3f)]
    public float pitch;//����

    public bool loop;//�O�_�`������

    [HideInInspector]
    public AudioSource source;
}
