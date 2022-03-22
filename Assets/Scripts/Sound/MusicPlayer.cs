using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioClip[] musicList;

    [SerializeField] float musicStartVolume = 0.6f;

    AudioSource myAudioSource;
    int musicIndex;
    // gameMuted is changed by toggle
    bool gameMuted = false;

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = musicStartVolume;
        myAudioSource = GetComponent<AudioSource>();
        ChangeSong();
    }

    public void ChangeSong() //altera musica, chamado no start e quando troca de fase
    {
        musicIndex = Random.Range(0, musicList.Length );
        PlayMusic();
    }

    private void PlayMusic()
    {
        myAudioSource.clip = musicList[musicIndex];
        myAudioSource.Play();
        StartCoroutine(WaitAudio());
    }

    public void MusicButton()
    {
        if (gameObject.GetComponent<AudioSource>().volume == musicStartVolume)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
            gameMuted = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = musicStartVolume;
            gameMuted = false;
        }
    }

    private IEnumerator WaitAudio()
    {
        yield return new WaitForSeconds(musicList[musicIndex].length);
        print("end of sound");
        ChangeSong();
    }

    public void EndGameMute(bool mute)
    {
        if (!gameMuted)
        {
            if (mute)
            {
                gameObject.GetComponent<AudioSource>().volume = 0;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().volume = musicStartVolume;
            }
        }
    }
}
