using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    bool soundActive = true;
    float musicStartVolume;

    [SerializeField] AudioClip clicButton;

    private void Start()
    {
        musicStartVolume = gameObject.GetComponent<AudioSource>().volume;
    }

    public void MusicButton()
    {
        ClickSound();
        if (gameObject.GetComponent<AudioSource>().volume == musicStartVolume)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
        }
        else { gameObject.GetComponent<AudioSource>().volume = musicStartVolume; }

    }

    public void SoundActive()
    {
        ClickSound();
        if (soundActive)
        {
            soundActive = false;
        }
        else { soundActive = true; }
    }

    public void PlaySound(AudioClip soundfile)
    {
        if (soundActive == true)
        {
            AudioSource.PlayClipAtPoint(soundfile, Camera.main.transform.position);

        }
    }

    public void ClickSound()
    {
        AudioSource.PlayClipAtPoint(clicButton, Camera.main.transform.position);
    }
}
