using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    UnitDamage,
    UnitDestroy,
    CastleDmageByUnit,
    CastleDamageByProjectile,
    CastleDestroy,
    Win,
    Lose

}
[System.Serializable]
public class SoundData
{
    public AudioSource soundSource;
    public SoundType soundType;
}
public class SFXSourcesController : MonoBehaviour
{
    [SerializeField] AudioController audioController;
    public static SFXSourcesController instance;

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SoundPlay(SoundType soundType)
    {
        audioController.PlaySound(soundType);
        Debug.Log("DamageSoundPlay + " + soundType);

    }

    public void MusicOnOff()
    {
        audioController.MusicOff();
    }
}
