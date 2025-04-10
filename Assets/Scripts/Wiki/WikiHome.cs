using System.Collections;

using UnityEngine;

public class WikiHome : MonoBehaviour
{
    string firstOpenHintSequence = "HINT_0";

    void OnEnable()
    {
        StartCoroutine(checkSequenceDelayed());
    }

    IEnumerator checkSequenceDelayed()
    {
        yield return new WaitForSeconds(1.0f);

        if (gameObject.activeSelf && !MetaNarrativeManager.Instance.HasVisitedSequence(firstOpenHintSequence))
        {
            MetaNarrativeManager.Instance.TriggerStorytellerSequence(firstOpenHintSequence);
        }        
    }
}