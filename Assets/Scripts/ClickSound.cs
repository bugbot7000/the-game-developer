using UnityEngine;

public class ClickSound : MonoBehaviour
{
    RandomAudioQueue randomAudioQueue;

    void Start()
    {
        randomAudioQueue = GetComponent<RandomAudioQueue>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            randomAudioQueue.PlayRandomSound();
        }
    }
}