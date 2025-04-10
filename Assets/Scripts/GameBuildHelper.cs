using UnityEngine;
using Michsky.DreamOS;

public class GameBuildHelper : MonoBehaviour
{
    [SerializeField] public ButtonManager continueButton;
    [SerializeField] public ButtonManager playRestartButton;
    [SerializeField] public Sprite restartSprite;
    
    void Start()
    {
        RefreshButtons();
    }

    public void RefreshButtons()
    {
        if (continueButton != null && playRestartButton != null)
        {
            playRestartButton.onClick.AddListener(CallEnableGameBuild);
            playRestartButton.onClick.AddListener(RestartGame);

            if (GameBuildManager.Instance.HasGameBeenPlayed(transform.GetSiblingIndex()))
            {
                playRestartButton.gameObject.SetActive(true);
                playRestartButton.SetText("Restart");
                playRestartButton.SetIcon(restartSprite);
                playRestartButton.onClick.RemoveListener(CallEnableGameBuild);
                
                continueButton.gameObject.SetActive(true);
            }
            else
            {
                playRestartButton.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(false);

                playRestartButton.onClick.RemoveListener(RestartGame);
            }
        }        
    }
    
    public void CallEnableGameBuild()
    {
        GameBuildManager.Instance.EnableGameBuild(transform.GetSiblingIndex());
        RefreshButtons();
    }

    public void RestartGame()
    {
        GameBuildManager.Instance.RestartGameBuild(transform.GetSiblingIndex());
    }

    public void CallCloseGameBuild()
    {
        GameBuildManager.Instance.DisableGameBuild();
    }
}