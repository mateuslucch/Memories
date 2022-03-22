using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneAudio : MonoBehaviour
{

    AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip audioClip)
    {
        myAudioSource.clip = audioClip;
        myAudioSource.Play();
    }
}
