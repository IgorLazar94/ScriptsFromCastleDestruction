using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class AudioController : MonoBehaviour
{
    [Header ("BackgroundMusic")]
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] Slider musicSlider;
    [SerializeField] float backgroundMusicVolume;
    private float musicVolume = 1f;


    [Header("SFX")]
    [SerializeField] List<SoundData> soundEffects;
    public Dictionary<SoundType, SoundData> soundsDataDict;
    [SerializeField] Slider soundSlider;
    [SerializeField] float sfxVolume;
    private float sfxSoundVolume = 1f;
    

    void Start()
    {
        soundsDataDict = new Dictionary<SoundType, SoundData>();

        backgroundMusic.volume = backgroundMusicVolume;
        musicSlider.value = backgroundMusic.volume / 0.05f;
  
        soundSlider.value = sfxVolume / 0.05f;
       
        musicSlider.onValueChanged.AddListener(OnSliderMusicValueChanged);
        soundSlider.onValueChanged.AddListener(OnSFXMusicValueChanged);

        foreach (SoundData soundsData in soundEffects)
        {
            soundsDataDict.Add(soundsData.soundType, soundsData);
        }

        for (int i = 0; i < soundEffects.Count; i++)
        {
            soundEffects[i].soundSource.volume = sfxVolume;
        }

        PlayerPrefs.SetString("MusicState", "Enable");
        PlayerPrefs.SetString("SoundState", "Enable");
        PlayerPrefs.SetString("VibrationState", "Enable");
        CheckSoundState("MusicState");

    }

    void OnSliderMusicValueChanged(float value)
    {
        musicVolume = value;

        backgroundMusic.volume = musicVolume * 0.05f;
    }

    void OnSFXMusicValueChanged(float value)
    {
        sfxSoundVolume = value;

        for (int i = 0; i < soundEffects.Count; i++)
        {
            soundEffects[i].soundSource.volume = sfxSoundVolume * 0.05f;
        }
    }

    public void SetOnOffMusic() //BackGround music
    {
        string state = PlayerPrefs.GetString("MusicState");
        if (state == "Enable")
        {
            PlayerPrefs.SetString("MusicState", "Disable");
            backgroundMusic.Stop();
        }
        else if (state == "Disable")
        {
            PlayerPrefs.SetString("MusicState", "Enable");
            backgroundMusic.Play();
        }

        CheckSoundState("MusicState");
    }

    public void SetOnOffSounds() // Sounds
    {
        string state = PlayerPrefs.GetString("SoundState");
        if (state == "Enable")
        {
            PlayerPrefs.SetString("SoundState", "Disable");

            for (int i = 0; i < soundEffects.Count; i++)
            {
                soundEffects[i].soundSource.enabled = false;
            }
            
        }
        else if (state == "Disable")
        {
            PlayerPrefs.SetString("SoundState", "Enable");
            for (int i = 0; i < soundEffects.Count; i++)
            {
                soundEffects[i].soundSource.enabled = true;
            }
        }

        CheckSoundState("SoundState");

    }

    public void PlaySound(SoundType soundType)
    {

        if (soundsDataDict.ContainsKey(soundType))
        {
            soundsDataDict[soundType].soundSource.Play();
        }
    }

   
    public string CheckSoundState(string key)
    {
   
        return PlayerPrefs.GetString(key);
    }

    public void MusicOff()
    {
        backgroundMusic.Stop();
    }
}
