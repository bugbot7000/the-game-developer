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

        yield return new WaitForSeconds(1.0f);

        if (gameObject.activeSelf && !MetaNarrativeManager.Instance.HasVisitedSequence(firstOpenHintSequence))
        {
            MetaNarrativeManager.Instance.TriggerStorytellerSequence(firstOpenHintSequence);
        }        
    }
}