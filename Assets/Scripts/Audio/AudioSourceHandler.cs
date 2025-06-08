using UnityEngine;
using System;
using DG.Tweening;

/// <summary>
/// Keeps a list of audio clips and generates at runtime the corresponding audio sources.
/// Inspired by this Brackey video: https://youtu.be/6OT43pvUyfY
/// </summary>
public class AudioSourceHandler : MonoBehaviour
{
    [SerializeField] private float masterVolume = 1.0f; // The default volume of every audio source.
    [SerializeField] private SoundEffect[] soundEffects = default;
    
    private void Start()
    {
        foreach(SoundEffect sound in soundEffects)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.AudioClip;
            sound.AudioSource.volume = masterVolume * sound.Volume;
            sound.AudioSource.playOnAwake = false;
        }
    }

    void UpdateVolume()
    {
        foreach(SoundEffect sound in soundEffects)
        {
            sound.AudioSource.volume = masterVolume * sound.Volume;
        }
    }

    public void PlayAudioByName(string name)
    {
        SoundEffect sound = Array.Find(soundEffects, targetSound => targetSound.Name == name);

        if (sound != null)
        {
            // This check is only really for when someone decides to call this method on Awake, before the AudioSources have been created...
            if (sound.AudioSource != null)
            {
                sound.AudioSource.volume = masterVolume * sound.Volume;

                sound.AudioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"[AudioSourceHandler] Could not find audio clip with name '{name}'.");
        }
    }

    public void PlayAudioByNameLoop(string name)
    {
        SoundEffect sound = Array.Find(soundEffects, targetSound => targetSound.Name == name);

        if (sound != null)
        {
            sound.AudioSource.volume = masterVolume * sound.Volume;
            
            sound.AudioSource.loop = true;

            sound.AudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"[AudioSourceHandler] Could not find audio clip with name '{name}'.");
        }
    }

    public void PlayAudioByNameOneShot(string name)
    {
        SoundEffect sound = Array.Find(soundEffects, targetSound => targetSound.Name == name);

        if (sound != null)
        {
            // Create a temporary AudioSource
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            tempSource.clip = sound.AudioClip;
            tempSource.volume = masterVolume * sound.Volume;
            
            // Play the sound
            tempSource.Play();
            
            // Destroy the temporary AudioSource after the clip finishes playing
            Destroy(tempSource, sound.AudioClip.length);
        }
        else
        {
            Debug.LogWarning($"[AudioSourceHandler] Could not find audio clip with name '{name}'.");
        }
    }

    public void FadeAudioOut(string name, float fadeDuration)
    {
        SoundEffect sound = Array.Find(soundEffects, targetSound => targetSound.Name == name);

        if (sound != null)
        {
            // sound.AudioSource.DOFade(0f, fadeDuration);
            Debug.LogWarning($"[AudioSourceHandler] Cannot find DOFade method.");
        }
        else
        {
            Debug.LogWarning($"[AudioSourceHandler] Could not find audio clip with name '{name}'.");
        }
    }

    public void StopAudio(string name)
    {
        SoundEffect sound = Array.Find(soundEffects, targetSound => targetSound.Name == name);

        if (sound != null)
        {
            sound.AudioSource.Stop();
        }
        else
        {
            Debug.LogWarning($"[AudioSourceHandler] Could not find audio clip with name '{name}'.");
        }
    }
}

[System.Serializable]
public class SoundEffect
{
    public string Name;
    public AudioClip AudioClip;
    public float Volume = 1.0f;

    [HideInInspector]
    public AudioSource AudioSource;
}
