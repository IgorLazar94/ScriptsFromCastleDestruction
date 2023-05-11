using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSources : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource [] soundEffects;

    [SerializeField] AudioController audioController;

    public void SwitchBackgroundMusic()
    {
       // backgroundMusic.Play()
    }
}
