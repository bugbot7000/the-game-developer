using UnityEngine;

public class GameBuildHelper : MonoBehaviour
{
    public void CallEnableGameBuild()
    {
        GameBuildManager.Instance.EnableGameBuild(transform.GetSiblingIndex());
    }

    public void CallCloseGameBuild()
    {
        GameBuildManager.Instance.DisableGameBuild();
    }
}