using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class WikiHome : MonoBehaviour
{
    [SerializeReference] RectTransform verticalLayoutGroup;
    
    string firstOpenHintSequence = "HINT_0";

    void OnEnable()
    {
        StartCoroutine(checkSequenceDelayed());
    }

    IEnumerator checkSequenceDelayed()
    {
        yield return null;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayoutGroup);

        // Weird little wait since the home page is created when the scene starts.
        yield return new WaitForSeconds(0.5f);

        if (gameObject.activeSelf)
        {
            WikiHistoryManager.Instance.AddPageToHistory(new HistoryItem(PageType.Home));

            if (!MetaNarrativeManager.Instance.HasVisitedSequence(firstOpenHintSequence))
            {
                MetaNarrativeManager.Instance.TriggerStorytellerSequence(firstOpenHintSequence);
            }
        }        
    }
}