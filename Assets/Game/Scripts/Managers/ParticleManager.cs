using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// gerek olmayabilir buna
/// </summary>
public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }
    private ParticleSystem hanabiParticleSystem;
    private ParticleSystem.EmissionModule emissionModule;
  
    private void Awake()
    {
        Instance = this;
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            emissionModule = hanabiParticleSystem.emission;
            emissionModule.enabled = true;
        }
        else
        {
            emissionModule = hanabiParticleSystem.emission;
            emissionModule.enabled = false;
        }
    }
    public void Play()
    {

    }

}
