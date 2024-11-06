using UnityEngine;
using TMPro;

public class WikiContentPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleTMP;
    [SerializeField] TextMeshProUGUI contentTMP;

    void Start()
    {
        if (WikiPageSearchManager.Instance.WikiPageExists())
        {
            UpdatePageContent(WikiPageSearchManager.Instance.GetLastSearchedPage());
        }
        else
        {
            titleTMP.SetText("Not Found");
            contentTMP.SetText($"No results were found for your search term '{WikiPageSearchManager.Instance.LastSearchTerm}'.\nPlease try again with another search term.");
        }
    }
    
    public void UpdatePageContent(WikiPage wikiPage)
    {
        titleTMP.SetText(wikiPage.Title);
        contentTMP.SetText(wikiPage.Content);
    }
}