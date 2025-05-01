using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Helper component for playing a random audio from a list without repeating the same audio twice in a row.
/// Assumes that a AudioSourceHander with the audio clips that it will play already exists.
/// </summary>
[RequireComponent(typeof(AudioSourceHandler))]
public class RandomAudioQueue : MonoBehaviour
{
    [SerializeField] private string[] soundsToQueue = default;        
    
    // Event triggered when PlayRandomSound is called. Public so it may be overwriten.
    public UnityEvent OnAudioPlay = default;
    
    private string[] soundQueue;
    private string lastPlayedSound;

    private AudioSourceHandler audioSourceHandler;

    private void Awake()
    {
        if (soundsToQueue.Length > 0)
        {
            lastPlayedSound = soundsToQueue[0];

            soundQueue = new string[soundsToQueue.Length - 1];
            for (int i = 1; i < soundsToQueue.Length; i++)
            {
                soundQueue[i-1] = soundsToQueue[i];
            }

            audioSourceHandler = GetComponent<AudioSourceHandler>();                
        }
    }

    public void PlayRandomSound()
    {
        string targetSound;

        if (soundQueue.Length > 1)
        {
            targetSound = soundQueue[Random.Range(0, soundQueue.Length)];

            // Add last played sound back to queue. 
            for (int i = 0; i < soundQueue.Length; i++)
            {
                if (soundQueue[i] == targetSound)
                {
                    soundQueue[i] = lastPlayedSound;
                    lastPlayedSound = targetSound;
                }
            }
        }
        else
        {
            targetSound = soundsToQueue[0];
        }

        audioSourceHandler.PlayAudioByName(targetSound);
        OnAudioPlay.Invoke();
    }
}