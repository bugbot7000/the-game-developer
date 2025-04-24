using System.Collections;

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
    [SerializeField] GameObject summerIndexContainer;

    [TitleGroup("References")]
    [SerializeField] RectTransform layoutGroup;
    [SerializeField] TextMeshProUGUI springErrorText;
    [SerializeField] TextMeshProUGUI summerErrorText;

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
            springErrorText.gameObject.SetActive(false);
            
            foreach (WikiPageSO page in WikiPageSearchManager.Instance.SpringIndex.WikiPages)
            {
                Instantiate(archiveItemPrefab, springIndexContainer.transform)
                    .GetComponent<WikiArchivePageItem>().Initialize(page, this);
            }  
        }

        if (WikiPageSearchManager.Instance.SummerUnlocked())
        {
            summerErrorText.gameObject.SetActive(false);

            foreach (WikiPageSO page in WikiPageSearchManager.Instance.SummerIndex.WikiPages)
            {
                Instantiate(archiveItemPrefab, summerIndexContainer.transform)
                    .GetComponent<WikiArchivePageItem>().Initialize(page, this);
            }              
        }

        WikiHistoryManager.Instance.AddPageToHistory(new HistoryItem(PageType.Index));

        StartCoroutine(rebuild());
    }

    IEnumerator rebuild()
    {
        yield return new WaitForEndOfFrame();

        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
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