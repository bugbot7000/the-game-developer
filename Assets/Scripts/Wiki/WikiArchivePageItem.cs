using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Michsky.DreamOS;

public class WikiArchivePageItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    WikiArchivePage wikiArchivePage;
    WikiPageSO contentPage;

    bool visited;
    bool pointerEntered;

    public void Initialize(WikiPageSO target, WikiArchivePage archivePage)
    {
        contentPage = target;
        wikiArchivePage = archivePage;

        if (WikiPageSearchManager.Instance.HasPageBeenVisited(contentPage))
        {
            GetComponent<Image>().color = Color.green;
            visited = true;
        }
        else
        {
            visited = false;
        }
    }

    void Update()
    {
        if (!pointerEntered)
            return;

        if (Input.GetMouseButtonDown(0) && visited)
        {
            WikiPageSearchManager.Instance.SetPageToLoad(contentPage);
            
            FindAnyObjectByType<WebBrowserManager>().OpenPage($"wiki.eren.local/content");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEntered = true;
        wikiArchivePage.ShowArchiveLabel(visited ? $"{contentPage.Title} ({contentPage.Date})" : "???");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEntered = false;
        wikiArchivePage.HideArchiveLabel();
    }
}