using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    const string MUSIC_VOLUME_KEY = "master volume";
    const string SFX_VOLUME_KEY = "sfx volume";

    const string musicOn = "Music On";
    const string musicOff = "Music Off";
    const string sfxOn = "Sfx On";
    const string sfxOff = "Sfx Off";

    bool soundActive = true;
    [SerializeField] float musicStartVolume = 0.6f;

    [SerializeField] AudioClip clickButtonSound;
    [SerializeField] MusicPlayer musicPlayer = null;
    AudioSource myAudioSource;

    // solve the problem of loading playerprefs next scene first
    // private void Awake()
    // {
    //     int soundControl = FindObjectsOfType<SoundControl>().Length;
    //     if (soundControl > 1) { Destroy(gameObject); }
    //     else { DontDestroyOnLoad(gameObject); }
    // }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        if (musicPlayer != null)
        {
            myAudioSource.Stop();
        }

        if (!PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
        {
            PlayerPrefs.SetString(MUSIC_VOLUME_KEY, musicOn);
            PlayerPrefs.SetString(SFX_VOLUME_KEY, sfxOn);
        }

        gameObject.GetComponent<AudioSource>().volume = musicStartVolume;

    }

    public void MusicButton()
    {
        ClickSound();
        if (gameObject.GetComponent<AudioSource>().volume == musicStartVolume)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
            PlayerPrefs.SetString(MUSIC_VOLUME_KEY, musicOff);
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = musicStartVolume;
            PlayerPrefs.SetString(MUSIC_VOLUME_KEY, musicOn);
        }
    }

    public void SfxSound()
    {
        print("teste");
        ClickSound();
        if (soundActive)
        {
            soundActive = false;
            PlayerPrefs.SetString(SFX_VOLUME_KEY, sfxOff);
        }
        else
        {
            soundActive = true;
            PlayerPrefs.SetString(SFX_VOLUME_KEY, sfxOn);
        }
    }

    public void PlayPieceSound(AudioClip soundfile)
    {
        if (soundActive == true)
        {
            AudioSource.PlayClipAtPoint(soundfile, Camera.main.transform.position);
        }
    }

    public void ClickSound()
    {
        if (clickButtonSound != null) { AudioSource.PlayClipAtPoint(clickButtonSound, Camera.main.transform.position); }
        else { print("Forgot to put audio clicks"); }
    }
}
