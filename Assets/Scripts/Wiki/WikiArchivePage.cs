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

    [TitleGroup("Grid Layouts")]
    [SerializeField] GameObject fallIndexContainer;
    [SerializeField] GameObject springIndexContainer;

    [TitleGroup("References")]
    [SerializeField] TextMeshProUGUI springLoadingText;

    [TitleGroup("Label")]
    [SerializeField] RectTransform label;
    [SerializeField] CanvasGroup labelCanvas;
    [SerializeField] TextMeshProUGUI labelText;

    void Start()
    {
        foreach (WikiPageSO page in WikiPageSearchManager.Instance.FallIndex.WikiPages)
        {
            Instantiate(archiveItemPrefab, fallIndexContainer.transform)
                .GetComponent<WikiArchivePageItem>().Initialize(page, this);
        }

        if (WikiPageSearchManager.Instance.SpringUnlocked())
        {
            springLoadingText.gameObject.SetActive(false);
            
            foreach (WikiPageSO page in WikiPageSearchManager.Instance.SpringIndex.WikiPages)
            {
                Instantiate(archiveItemPrefab, springIndexContainer.transform)
                    .GetComponent<WikiArchivePageItem>().Initialize(page, this);
            }  
        }
        else
        {
            
            springLoadingText.SetText($"Rebuilding... {Mathf.Round(WikiPageSearchManager.Instance.GetFallSemesterProgress() * 100)}% complete. Continuing loading pages to complete index rebuild.");
        }

        WikiHistoryManager.Instance.AddPageToHistory(new HistoryItem(PageType.Index));

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
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