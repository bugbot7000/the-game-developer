using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class IndexRestoration : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI fallText;
    [SerializeField] Image fallProgressBar;
    [SerializeField] TextMeshProUGUI springText;
    [SerializeField] Image springProgressBar;
    [SerializeField] TextMeshProUGUI summerText;
    [SerializeField] Image summerProgressBar;

    float barLength = 600f;

    void Update()
    {
        titleText.SetText($"Index Restoration - {(int) (WikiPageSearchManager.Instance.GetTotalProgress() * 100)}%");
        mainText.SetText($"Corrupted index cache entries have been identified.\nManually load corrupted pages and run builds to restore index.\n{WikiPageSearchManager.Instance.GetMissingRequirementsForCurrentChunk()}");
        
        float fallProgress = WikiPageSearchManager.Instance.GetFallSemesterProgress();
        fallText.SetText($"Fall Restoration Progress - {(int) (fallProgress * 100)}%");
        fallProgressBar.rectTransform.sizeDelta = new Vector2(barLength * fallProgress, 16);
        
        if (WikiPageSearchManager.Instance.SpringUnlocked())
        {
            float springProgress = WikiPageSearchManager.Instance.GetSpringSemesterProgress();
            springText.SetText($"Spring Restoration Progress - {(int) (springProgress * 100)}%");
            springProgressBar.rectTransform.sizeDelta = new Vector2(barLength * springProgress, 16);
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
            summerProgressBar.rectTransform.sizeDelta = new Vector2(barLength * summerProgress, 16);
        }
        else
        {
            summerText.SetText("??? - 0%");
            summerProgressBar.rectTransform.sizeDelta = Vector2.zero;
        }
    }
}