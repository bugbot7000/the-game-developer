using UnityEngine;
using TMPro;

public class WikiSideBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI progress;

    void Start()
    {
        if (!WikiPageSearchManager.Instance.SpringUnlocked())
        {
            progress.SetText($"Rebuilding 'Spring' database... Progress at {Mathf.Round(WikiPageSearchManager.Instance.GetFallSemesterProgress() * 100)}%.");
        }
        else
        {
            progress.SetText("");
        }
    }
}