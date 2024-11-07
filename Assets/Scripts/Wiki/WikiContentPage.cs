using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WikiContentPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI subtitle;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] Button buildButton;

    TextMeshProUGUI buttonText;
    WikiPage currentPage;

    void Start()
    {
        buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

        if (WikiPageSearchManager.Instance.CheckIfWikiPageExists())
        {
            UpdatePageContent(WikiPageSearchManager.Instance.LastFoundPage);
        }
        else
        {
            date.SetText("");
            title.SetText("Not Found");
            subtitle.SetText("");
            content.SetText($"No results were found for your search term '{WikiPageSearchManager.Instance.LastSearchTerm}'.\nPlease try again with another search term.");
            buildButton.gameObject.SetActive(false);
        }
    }
    
    public void UpdatePageContent(WikiPage wikiPage)
    {
        currentPage = wikiPage;

        date.SetText(wikiPage.Date);
        title.SetText(wikiPage.Title);
        subtitle.SetText(wikiPage.Subtitle);
        content.SetText(wikiPage.Content);

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

        if (wikiPage.ActivatesChat && !MetaNarrativeManager.Instance.HasVisitedSequence(wikiPage.ChatID))
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
}