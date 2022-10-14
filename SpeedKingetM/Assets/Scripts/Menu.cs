using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] List<GameObject> characters = new List<GameObject>();
    public Slider BGMSlider, soundSlider;
    public int selectedCharacter = 0;
    private void Awake()
    {
        StartCoroutine(initialization());
        
       
    }

    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void Open(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void LoadScence(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Count;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Count;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void Slected()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    }

    public void SetSoundVolum(float volume)
    {
        audioManager.Volume("Attack", volume);
        audioManager.Volume("Defeat", volume);
        audioManager.Volume("Grounded", volume);
        audioManager.Volume("Destroy", volume);

    }
    public void SetBGMVolume(float volume)//背景音樂音量
    {
        audioManager.Volume("BGM", volume);
    }

    public void SaveVolumeValue() 
    {
        SaveLoad.SaveVolumeValue(this);
    }

    IEnumerator initialization()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null) 
        {
            yield return LoadVolumeValue();
        }
        else 
        {
            while (audioManager == null) 
            {
                audioManager = FindObjectOfType<AudioManager>();
            }
        }
        
    }
    IEnumerator LoadVolumeValue() 
    {
        bool done = false;
        Volume volume = SaveLoad.LoadVolumeValue();
        BGMSlider.value = volume.BGMVolume;
        soundSlider.value = volume.soundVolume;
        done = true;
        yield return new WaitWhile(() => done == false);
    }
}
