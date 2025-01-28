using UnityEngine;
using UnityEngine.UI;
using Michsky.DreamOS;
using TMPro;

/// <summary>
/// Button that appears on the search result page. Shows the name of the page, and whether it has been visited or not.
/// </summary>
public class WikiSearchResultButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Image visitedImage;

    WikiPageSO targetPage;

    public void InitializeButton(WikiPageSO page)
    {
        targetPage = page;
        
        buttonText.SetText($"({page.Date}) {page.Title}");

        //TODO: if page visited, show image
    }

    // Called on button OnClick event.
    public void LoadPage()
    {
        WikiPageSearchManager.Instance.SetPageToLoad(targetPage);

        FindAnyObjectByType<WebBrowserManager>().OpenPage($"wiki.eren.local/content");
    }
}
