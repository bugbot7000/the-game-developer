using UnityEngine;

using DG.Tweening;

public class FinalSecretRoomText : MonoBehaviour
{
    [SerializeField] CanvasGroup finalText;

    void OnTriggerEnter(Collider other)
    {
        finalText.DOFade(1, 1).SetEase(Ease.OutCubic);
    }
}
