using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Michsky.DreamOS;
using TMPro;

/// <summary>
/// Button that appears on the search result page. Shows the name of the page, and whether it has been visited or not.
/// </summary>
public class WikiSearchResultButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Image visitedImage;
    [SerializeField] Image bullet;

    WikiPageSO targetPage;

    public void InitializeButton(WikiPageSO page)
    {
        targetPage = page;
        
        buttonText.SetText($"({page.Date}) {page.Title}");

        visitedImage.enabled = WikiPageSearchManager.Instance.HasPageBeenVisited(targetPage);
        bullet.enabled = !visitedImage.enabled;
    }

    // Called on button OnClick event.
    public void LoadPage()
    {
        WikiPageSearchManager.Instance.SetPageToLoad(targetPage);

        FindAnyObjectByType<WebBrowserManager>().OpenPage($"wiki.eren.local/content");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontStyle = FontStyles.Normal;
    }
}
