using System.Collections.Generic;
using UnityEngine;
using AptabaseSDK;
using TMPro;
using Sirenix.OdinInspector;

public class ThanksCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI emailInput;
    [SerializeField] TextMeshProUGUI buttonText;

    public void SubmitEmail()
    {
        Aptabase.TrackEvent("email", new Dictionary<string, object> {{"email",  emailInput.text}});
        buttonText.SetText("Thank you!");
    }

    [Button]
    public void OpenPanel()
    {
        GetComponentInChildren<Animator>().Play("Thanks Fade In");
        
        CanvasGroup canvasGroup = GetComponentInChildren<CanvasGroup>();

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}