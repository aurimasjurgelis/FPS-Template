using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource backgroundMusic;

    public List<AudioSource> sounds = new List<AudioSource>();
    private Dictionary<string, AudioSource> soundEffects;

    private void Awake()
    {
        instance = this;
        soundEffects = new Dictionary<string, AudioSource>();
        foreach(var sound in sounds)
        {
            soundEffects.Add(sound.name, sound);
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlaySFX(string soundEffectName)
    {
        soundEffects[soundEffectName].Stop();
        soundEffects[soundEffectName].Play();
    }

    public void StopSFX(string soundEffectName)
    {
        soundEffects[soundEffectName].Stop();
    }
}
