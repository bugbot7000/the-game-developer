using UnityEngine;

using Michsky.DreamOS;
using TMPro;

public class WikiSideBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI progress;
    [SerializeField] WikiPageSO worldbuildingPage;

    void Start()
    {
        /*
        if (!WikiPageSearchManager.Instance.SpringUnlocked())
        {
            progress.SetText($"Rebuilding 'Spring' database... Progress at {Mathf.Round(WikiPageSearchManager.Instance.GetFallSemesterProgress() * 100)}%.");
        }
        else
        {
            progress.SetText("");
        }
        */
    }

    public void LoadWorldBuilding()
    {
        WikiPageSearchManager.Instance.SetPageToLoad(worldbuildingPage);

        FindAnyObjectByType<WebBrowserManager>().OpenPage($"wiki.eren.local/content");        
    }
}