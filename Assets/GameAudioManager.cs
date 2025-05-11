using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using Random = System.Random;

public class GameAudioManager : MonoBehaviour
{
    private static GameAudioManager _instance;
    private bool canPlayMusic,canPlaySFX;
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

    public AudioSource SFXSource;
    public List<AudioFile> audioFiles = new ();
    public enum SFX
    {
        gen_slash,
        blunt,
        footstep,
        cheer,
        weapon_swing,
        zombie_moan,
        arrow_draw,
        arrow_loose,
        ghostly_fire_summon,
        ice_crack,
        push_perc,
        pull_sfx,
        knife_sharpen,
        charm_choir,
        rattlesnake_move,
        rock_crumble,
        rock_crash,
        mino_stomp,
        bull_snarl,
        minotaur_death,
        necro_laugh,
        necro_die,
        necro_attack
    }
    [SerializeField]

    private void Awake()
    {
        DontDestroyOnLoad(this);
      _instance = this;
    }

    public void playSFX(SFX sfxType)
    {
        for (int i = 0; i < audioFiles.Count; i++)
        {
            if (audioFiles[i].EffectType == sfxType)
            {
                SFXSource.clip = audioFiles[i].audioClip;
                SFXSource.pitch = UnityEngine.Random.Range(audioFiles[i].minPitchShift,audioFiles[i].maxPitchShift);
                SFXSource.Play();
            }
        }
    }
}

[Serializable]
public class AudioFile
{
    public GameAudioManager.SFX EffectType;
    public AudioClip audioClip;
    public float minPitchShift, maxPitchShift;
}
