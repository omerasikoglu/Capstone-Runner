using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        Instance ??= this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip, float volume = .3f)
    {
        audioSource.PlayOneShot(clip, volume);
    }


}
