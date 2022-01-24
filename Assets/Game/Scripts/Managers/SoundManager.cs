using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    public static AudioSource clickSound;
    //Her ui ekran�n ba��nda veya sounda dinletilebilecek sesler
    public static AudioSource generaUiIntroSound;
    //In game
    public static AudioSource inGameSound;
    //Win ui i�in
    public static AudioSource danceSound;
    //Fail ui i�in
    public static AudioSource failSound;
    //Intro ui i�in
    public static AudioSource introSound;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
       
        clickSound = GetComponent<AudioSource>();

    }

    //Her sesin oynat�lmas� veya durdurulmas� i�in methodlar

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
