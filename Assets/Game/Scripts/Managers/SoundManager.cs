using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    public static AudioSource clickSound;
    //Her ui ekranýn baþýnda veya sounda dinletilebilecek sesler
    public static AudioSource generaUiIntroSound;
    //In game
    public static AudioSource inGameSound;
    //Win ui için
    public static AudioSource danceSound;
    //Fail ui için
    public static AudioSource failSound;
    //Intro ui için
    public static AudioSource introSound;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
       
        clickSound = GetComponent<AudioSource>();

    }

    //Her sesin oynatýlmasý veya durdurulmasý için methodlar

    [Button]
    public static void PlayClickSound()
    {
        clickSound.Play();
        
    }

    [Button]
    public static void StopClickSound()
    {
        clickSound.Stop();

    }

   

}
