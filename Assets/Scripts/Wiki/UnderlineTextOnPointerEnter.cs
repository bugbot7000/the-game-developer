using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

/// <summary>
/// Button that appears on the search result page. Shows the name of the page, and whether it has been visited or not.
/// </summary>
public class UnderlineTextOnPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI buttonText;

    public void Start()
    {
        buttonText = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontStyle |= FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontStyle ^= FontStyles.Underline;
    }
}
