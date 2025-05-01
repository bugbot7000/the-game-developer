using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Michsky.DreamOS;

public class WikiContentPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] Button buildButton;
    [SerializeField] Image downloadIcon;
    [SerializeField] Color downloadedColor;
    [SerializeField] RectTransform contentRect;

    TextMeshProUGUI buttonText;
    WikiPageSO currentPage;

    void Start()
    {
        buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

        UpdatePageContent(WikiPageSearchManager.Instance.LastFoundPageSO);

        WikiHistoryManager.Instance.AddPageToHistory(new HistoryItem(PageType.Content, content:currentPage));
    }
    
    public void UpdatePageContent(WikiPageSO wikiPage)
    {
        currentPage = wikiPage;

        title.SetText(wikiPage.Title);

        if (!string.IsNullOrEmpty(wikiPage.Subtitle))
        {
            date.SetText($"{coloredName(wikiPage.Subtitle.Trim())} - {wikiPage.Date}");
        }
        else
        {
            date.SetText(wikiPage.Date);
        }

        if (wikiPage.IsArchivedChat)
        {
            content.SetText(wikiPage.ContentAsArchivedChat());
            content.lineSpacing = 0;
            content.paragraphSpacing = 64;
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

        FindFirstObjectByType<WebBrowserManager>().UpdateCurrentTabText(wikiPage.Title);

        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, content.rectTransform.sizeDelta.y + 300);

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect.parent.transform.GetComponent<RectTransform>());

        if (wikiPage.ActivatesInteractveChat && !MetaNarrativeManager.Instance.HasVisitedSequence(wikiPage.ChatID))
        {
            MetaNarrativeManager.Instance.TriggerStorytellerSequence(wikiPage.ChatID);
        }
    }

    string coloredName(string name)
    {
        if      (name == "Eren")  return "<b><color=#0bd400>Eren</color></b>";
        else if (name == "Rose")  return "<b><color=#c002d1>Rose</color></b>";
        else if (name == "Kento") return "<b><color=#f5260f>Kento</color></b>";
        else                      return name;
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