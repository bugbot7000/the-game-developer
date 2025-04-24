using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class IndexRestoration : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI fallText;
    [SerializeField] Image fallProgressBar;
    [SerializeField] Image fallUnlockMarker;
    [SerializeField] TextMeshProUGUI springText;
    [SerializeField] Image springProgressBar;
    [SerializeField] Image springUnlockMarker;
    [SerializeField] TextMeshProUGUI summerText;
    [SerializeField] Image summerProgressBar;
    [SerializeField] Image summerUnlockMarker;

    void Start()
    {
        fallUnlockMarker.rectTransform.anchoredPosition = new Vector2(500 * WikiPageSearchManager.Instance.ProgressToUnlockSpring, 0);
        springUnlockMarker.rectTransform.anchoredPosition = new Vector2(500 * WikiPageSearchManager.Instance.ProgressToUnlockSummer, 0);
    }
    
    void Update()
    {
        titleText.SetText($"Index Restoration - {(int) (WikiPageSearchManager.Instance.GetTotalProgress() * 100)}%");
        
        float fallProgress = WikiPageSearchManager.Instance.GetFallSemesterProgress();
        fallText.SetText($"Fall Restoration Progress - {(int) (fallProgress * 100)}%");
        fallProgressBar.rectTransform.sizeDelta = new Vector2(500 * fallProgress, 16);
        
        if (WikiPageSearchManager.Instance.SpringUnlocked())
        {
            float springProgress = WikiPageSearchManager.Instance.GetSpringSemesterProgress();
            springText.SetText($"Spring Restoration Progress - {(int) (springProgress * 100)}%");
            springProgressBar.rectTransform.sizeDelta = new Vector2(500 * springProgress, 16);
        }
        else
        {
            springText.SetText("??? - 0%");
            springProgressBar.rectTransform.sizeDelta = Vector2.zero;
        }

        if (WikiPageSearchManager.Instance.SummerUnlocked())
        {
            float summerProgress = WikiPageSearchManager.Instance.GetSummerProgress();
            summerText.SetText($"Summer Restoration Progress - {(int) (summerProgress * 100)}%");
            summerProgressBar.rectTransform.sizeDelta = new Vector2(500 * summerProgress, 16);
        }
        else
        {
            summerText.SetText("??? - 0%");
            summerProgressBar.rectTransform.sizeDelta = Vector2.zero;
        }
    }
}