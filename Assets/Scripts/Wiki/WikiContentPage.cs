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
    [SerializeField] Image downloadIcon;
    [SerializeField] Color downloadedColor;

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
            content.lineSpacing = 0;
            content.paragraphSpacing = 48;
        }
        else
        {
            content.SetText(wikiPage.Content);
            content.lineSpacing = 12;
            content.paragraphSpacing = 0;
        }

        buildButton.gameObject.SetActive(wikiPage.ContainsBuild);
        if (wikiPage.ContainsBuild)
        {
            if (GameBuildManager.Instance.HasGameBeenAdded(wikiPage.BuildIndex))
            {
                buildButton.enabled = false;
                buttonText.SetText("Build already downloaded");
                setButtonDownloaded();
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

        setButtonDownloaded();

        GameBuildManager.Instance.AddGameBuildToHub(currentPage.BuildIndex);
    }

    void setButtonDownloaded()
    {
        downloadIcon.enabled = false;
        buttonText.fontStyle = FontStyles.Normal;
        buttonText.color = downloadedColor;
        buttonText.GetComponent<UnderlineTextOnPointerEnter>().enabled = false;
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