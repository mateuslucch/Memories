using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dodger
{
    public class AudioController : MonoBehaviour
    {
        AudioSource myAudioSource;
        bool isMusicPlaying = true;
        [SerializeField] [Range(0f, 1f)] float maxVolume;

        private void Start()
        {            
            myAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown("m"))
            {
                isMusicPlaying = !isMusicPlaying;
                if (isMusicPlaying)
                {
                    myAudioSource.volume = maxVolume;
                }
                else
                {
                    myAudioSource.volume = 0f;
                }
            }
        }
    }
}