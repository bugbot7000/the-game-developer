using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Michsky.DreamOS;
using TMPro;

/// <summary>
/// Loads a content page when you click on it.
/// </summary>
public class WikiContentPageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] WikiPageSO targetPage;

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
