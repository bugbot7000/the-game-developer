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
    
    [SerializeField] GameObject[] gamePrefabs;
    [SerializeField] GameHubManager.GameItem[] builds;

    GameHubManager gameHubManager;

    int enabledBuild = -1;

    void Start()
    {
        gameHubManager = FindAnyObjectByType<GameHubManager>();
    }

    public void EnableGameBuild(int build)
    {
        gamePrefabs[build].SetActive(true);

        enabledBuild = build;
    }

    public void DisableGameBuild()
    {
        gamePrefabs[enabledBuild].SetActive(false);

        enabledBuild = -1;
    }

    [Button]
    public void AddGameBuildToHub(int buildIndex)
    {
        gameHubManager.AddGameToLibrary(builds[buildIndex]);
    }
}