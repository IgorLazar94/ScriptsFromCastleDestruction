using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateFX : MonoBehaviour
{

    private ParticleSystem[] particlesArray;

    private void Start()
    {
        particlesArray = GetComponentsInChildren<ParticleSystem>();
    }

    public void StartPlayParticles()
    {
        StartCoroutine(PlayParticles());
    }

    private IEnumerator PlayParticles()
    {
        yield return new WaitForSeconds(1.8f); 
        for (int i = 0; i < particlesArray.Length; i++)
        {
            particlesArray[i].Play();
        }
    }
}
