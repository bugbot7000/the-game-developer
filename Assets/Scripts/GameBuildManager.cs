using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using Michsky.DreamOS;

[TypeInfoBox("Handles activating and de-activating the corresponding build game object depending on which games is being run, besides adding builds as they are downloaded.")]
public class GameBuildManager : MonoBehaviour
{
    #region Singleton
    public static GameBuildManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion    
    
    [SerializeField, SceneObjectsOnly] GameObject[] gamePrefabs;
    [SerializeField] GameHubManager.GameItem[] builds;
    [SerializeField] GameHubManager gameHubManager;

    List<GameObject> addedBuilds = new List<GameObject>();

    int enabledBuild = -1;

    public void EnableGameBuild(int build)
    {
        addedBuilds[build].SetActive(true);

        enabledBuild = build;
    }

    public void DisableGameBuild()
    {
        addedBuilds[enabledBuild].SetActive(false);

        enabledBuild = -1;
    }

    [Button]
    public void AddGameBuildToHub(int buildIndex)
    {
        if (!addedBuilds.Contains(gamePrefabs[buildIndex]))
        {
            gameHubManager.AddGameToLibrary(builds[buildIndex]);
            addedBuilds.Add(gamePrefabs[buildIndex]);
        }
    }
}