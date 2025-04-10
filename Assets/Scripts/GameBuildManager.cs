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
    
    [SerializeField] bool enableBuildDebug;
    [SerializeField] List<string> chatOnBuild = new List<string>();
    [SerializeField, SceneObjectsOnly] GameObject[] gamePrefabs;
    [SerializeField] GameHubManager.GameItem[] builds;
    [SerializeField] GameHubManager gameHubManager;
    [SerializeField] GameObject cloneParent;

    List<int> addedBuildIndices = new List<int>();
    List<GameObject> addedBuilds = new List<GameObject>();
    List<bool> hasBuildBeenPlayed = new List<bool>();

    int enabledBuild = -1;

    void Start()
    {
        for (int i = 0; i < gamePrefabs.Length; i++)
        {
            Instantiate(gamePrefabs[i], cloneParent.transform);
        }

        if (enableBuildDebug)
        {
            for (int i = 0; i < builds.Length; i++)
            {
                AddGameBuildToHub(i);
            }
        }
    }

    public void EnableGameBuild(int build)
    {
        addedBuilds[build].SetActive(true);
        hasBuildBeenPlayed[build] = true;

        enabledBuild = build;

        if (!string.IsNullOrWhiteSpace(chatOnBuild[build]))
        {
            if (!MetaNarrativeManager.Instance.HasVisitedSequence(chatOnBuild[build]))
            {
                MetaNarrativeManager.Instance.TriggerStorytellerSequence(chatOnBuild[build]);
            }

            chatOnBuild[build] = "";
        }
    }

    public void RestartGameBuild(int build)
    {
        int sibilingIndex = addedBuilds[build].transform.GetSiblingIndex();
        Destroy(addedBuilds[build]);
        addedBuilds[build] = cloneParent.transform.GetChild(sibilingIndex).gameObject;
        addedBuilds[build].transform.parent = transform;
        addedBuilds[build].transform.SetSiblingIndex(sibilingIndex);
        GameObject replacementClone = Instantiate(addedBuilds[build], cloneParent.transform);
        replacementClone.transform.SetSiblingIndex(sibilingIndex);
        EnableGameBuild(build);
    }

    public void DisableGameBuild()
    {
        addedBuilds[enabledBuild].SetActive(false);

        enabledBuild = -1;
    }

    public bool HasGameBeenAdded(int index)
    {
        return addedBuildIndices.Contains(index);
    }

    public bool HasGameBeenPlayed(int index)
    {
        if (hasBuildBeenPlayed.Count == 0)
        {
            return false;
        }

        return hasBuildBeenPlayed[index];
    }

    [Button]
    public void AddGameBuildToHub(int buildIndex)
    {
        if (!addedBuilds.Contains(gamePrefabs[buildIndex]))
        {
            gameHubManager.AddGameToLibrary(builds[buildIndex]);
            addedBuilds.Add(gamePrefabs[buildIndex]);
            addedBuildIndices.Add(buildIndex);
            hasBuildBeenPlayed.Add(false);
        }
    }
}