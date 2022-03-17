using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChange : MonoBehaviour
{
    public void ChangeMusic()
    {
        FindObjectOfType<SoundControl>().MusicButton();

        if (FindObjectOfType<MusicPlayer>())
        {
            FindObjectOfType<MusicPlayer>().MusicButton();
        }
    }

    public void ChangeSfx()
    {
        FindObjectOfType<SoundControl>().SfxSound();
    }
}
