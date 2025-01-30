using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class WikiContentPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI subtitle;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] Button buildButton;

    TextMeshProUGUI buttonText;
    WikiPageSO currentPage;

    void Start()
    {
        buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

        UpdatePageContent(WikiPageSearchManager.Instance.LastFoundPageSO);
    }
    
    public void UpdatePageContent(WikiPageSO wikiPage)
    {
        currentPage = wikiPage;

        date.SetText(wikiPage.Date);
        title.SetText(wikiPage.Title);
        subtitle.SetText(wikiPage.Subtitle);

        if (wikiPage.IsArchivedChat)
        {
            content.SetText(wikiPage.ContentAsArchivedChat());
        }
        else
        {
            content.SetText(wikiPage.Content);
        }

        buildButton.gameObject.SetActive(wikiPage.ContainsBuild);
        if (wikiPage.ContainsBuild)
        {
            if (GameBuildManager.Instance.HasGameBeenAdded(wikiPage.BuildIndex))
            {
                buildButton.enabled = false;
                buttonText.SetText("Build already downloaded");
            }
            else
            {
                buildButton.enabled = true;
                buttonText.SetText("Download Build");
            }
        }

        if (wikiPage.ActivatesInteractveChat && !MetaNarrativeManager.Instance.HasVisitedSequence(wikiPage.ChatID))
        {
            MetaNarrativeManager.Instance.TriggerStorytellerSequence(wikiPage.ChatID);
        }
    }

    public void DownloadBuild()
    {
        buildButton.enabled = false;
        buttonText.SetText("Build added to Game Hub");

        GameBuildManager.Instance.AddGameBuildToHub(currentPage.BuildIndex);
    }

    [Button]
    public void LoadWikiPageByName(string name)
    {
        foreach (WikiPage page in WikiPageSearchManager.Instance.Index.WikiPages)
        {
            if (page.Title == name)
            {
                // UpdatePageContent(page);
                return;
            }
        }

        Debug.LogWarning($"[WikiContentPage] Could not find page with title '{name}'");   
    }
}