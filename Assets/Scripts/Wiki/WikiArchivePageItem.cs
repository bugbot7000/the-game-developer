using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Michsky.DreamOS;
using Sirenix.OdinInspector;
using TMPro;

public class WikiArchivePageItem : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Dictionary<WikiPageType, Sprite> sprites;

    WikiArchivePage wikiArchivePage;
    WikiPageSO contentPage;

    bool visited;
    bool pointerEntered;

    public void Initialize(WikiPageSO target, WikiArchivePage archivePage)
    {
        Image image = GetComponent<Image>();

        image.sprite = sprites[target.PageType];

        contentPage = target;
        wikiArchivePage = archivePage;

        if (WikiPageSearchManager.Instance.HasPageBeenVisited(contentPage))
        {
            image.color = Color.white;
             
            TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.SetText(target.Title);
            textMeshPro.color = Color.black;

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