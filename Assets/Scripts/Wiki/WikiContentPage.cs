using UnityEngine;
using TMPro;

public class WikiContentPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI subtitle;
    [SerializeField] TextMeshProUGUI content;

    void Start()
    {
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
        }
    }
    
    public void UpdatePageContent(WikiPage wikiPage)
    {
        date.SetText(wikiPage.Date);
        title.SetText(wikiPage.Title);
        subtitle.SetText(wikiPage.Subtitle);
        content.SetText(wikiPage.Content);
    }
}