using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] backgroundMusic;
    public AudioSource[] soundEffects;

    void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            soundEffects[index].Play();
        }
    }

    public void PlayBackgroundMusic(int index)
    {
        if (backgroundMusic[index].isPlaying)
        {
            return;
        }
        
        StopMusic();
        if (index >= 0 && index < backgroundMusic.Length)
        {
            backgroundMusic[index].Play();
        }
    }

    public void StopMusic()
    {
        for (int index = 0; index < backgroundMusic.Length; index++)
        {
            backgroundMusic[index].Stop();
        }
    }
}
