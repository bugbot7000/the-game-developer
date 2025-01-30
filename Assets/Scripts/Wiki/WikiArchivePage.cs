using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;
using TMPro;

/// <summary>
/// Code for the page that shows which pages you've visted and which pages you haven't. 
/// </summary>
public class WikiArchivePage : MonoBehaviour
{
    [TitleGroup("Prefabs"), AssetsOnly]
    [SerializeField] GameObject archiveItemPrefab;

    [TitleGroup("References")]
    [SerializeField] GameObject archiveContainer;
    [SerializeField] RectTransform label;
    [SerializeField] CanvasGroup labelCanvas;
    [SerializeField] TextMeshProUGUI labelText;

    void Start()
    {
        foreach (WikiPageSO page in WikiPageSearchManager.Instance.NewIndex.WikiPages)
        {
            Instantiate(archiveItemPrefab, archiveContainer.transform)
                .GetComponent<WikiArchivePageItem>().Initialize(page, this);
        }
    }
    
    void Update()
    {
        if (labelCanvas.alpha == 1)
        {
            label.position = Input.mousePosition;
        }
    }

    public void ShowArchiveLabel(string text)
    {
        labelText.SetText(text);
        LayoutRebuilder.ForceRebuildLayoutImmediate(label); // Force ContentSizeFitter to update before showing.
        labelCanvas.alpha = 1;
    }

    public void HideArchiveLabel()
    {
        labelCanvas.alpha = 0;
    }
}