using UnityEngine;

using Sirenix.OdinInspector;

[TypeInfoBox("Handles activating and de-activating the corresponding build game object depending on which games is being run.")]
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

    int enabledBuild = -1;
    
    [SerializeField] GameObject[] gamePrefabs;

    public void EnableGameBuild(int build)
    {
        Debug.Log(build);

        gamePrefabs[build].SetActive(true);

        enabledBuild = build;
    }

    public void DisableGameBuild()
    {
        gamePrefabs[enabledBuild].SetActive(false);

        enabledBuild = -1;
    }
}