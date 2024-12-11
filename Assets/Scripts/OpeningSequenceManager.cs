using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class OpeningSequenceManager : MonoBehaviour
{
    [SerializeField] CanvasGroup fade;
    [SerializeField] Canvas dreamOS;
    [SerializeField] Canvas titleScreen;

    void Start()
    {
        fade.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic).OnComplete(() => fade.blocksRaycasts = false);
    }

    public void StartGame()
    {
        fade.blocksRaycasts = true;
        DOTween.Sequence()
            .Append(fade.DOFade(1.0f, 2.0f).SetEase(Ease.OutCubic))
            .AppendCallback(() => {
                dreamOS.gameObject.SetActive(true);
                titleScreen.gameObject.SetActive(false);
            })
            .AppendInterval(1.0f)
            .Append(fade.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic))
            .AppendCallback(() => fade.blocksRaycasts = false);
    }
}