using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.ProBuilder.MeshOperations;

public class LevelManager : MonoBehaviour
{
    public enum LevelType
    {
        Maze_Gray,
        Maze_Art,
        Arena_Gray,
        Arena_Art
    }
    public PlayableDirector Timeline;
    bool shouldPixelateView;
    scr_playerController playerController;
    scr_EnemySpawner spawner;
    public LevelType typeOfLevel;
    private void Awake()
    {
    }

    private void Start()
    {
        switch (typeOfLevel)
        {
            case LevelType.Maze_Gray:
                break;
            case LevelType.Maze_Art:
                break;
            case LevelType.Arena_Gray:
                break;
            case LevelType.Arena_Art:
                Timeline.Play();
                playerController = FindObjectOfType<scr_playerController>();
                break;
        }
     
    }

    public void OnSequenceComplete()
    {
        playerController.enabled = true;
        // spawner.enabled = true;
    }
}
