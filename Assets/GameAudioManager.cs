using System;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    private static GameAudioManager _instance;
    public bool canPlayMusic,canPlaySFX;
    public static GameAudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameAudioManager is null!");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
      _instance = this;
    }
}
