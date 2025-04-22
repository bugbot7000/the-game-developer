using UnityEngine;

using System.Collections;

using DG.Tweening;
using TMPro;
using Michsky.DreamOS;

public class OpeningSequenceManager : MonoBehaviour
{
    [SerializeField] bool skipOpening;
    [SerializeField] CanvasGroup fade;
    [SerializeField] Canvas dreamOS;
    [SerializeField] GameObject raliasLamentBuild;
    [SerializeField] RectTransform notifcation;
    [SerializeField] RectTransform fullScreenRawImage;
    [SerializeField] RectTransform mouseCursor;
    [SerializeField] RectTransform topBar;
    [SerializeField] TextMeshProUGUI chapterTitleText;
    [SerializeField] CanvasGroup theEndCanvasGroup;
    [SerializeField] WindowManager messaging;
    [SerializeField] WindowManager webBrowser;

    IEnumerator Start()
    {
        yield return null;

        if (skipOpening)
        {
            Debug.Log("[OpeningSequenceManager] Skipping opening sequence.");

            dreamOS.gameObject.SetActive(true);
            fade.gameObject.SetActive(false);
            fullScreenRawImage.parent.gameObject.SetActive(false);
        }
        else
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        fade.blocksRaycasts = true;
        DOTween.Sequence()
            .AppendCallback(() => {
                dreamOS.gameObject.SetActive(true);
                raliasLamentBuild.SetActive(true);
                messaging.OpenWindow();
                // webBrowser.OpenWindow();
                // webBrowser.GetComponent<WebBrowserManager>().OpenPage("www.vapor.com/ralias-lament");
            })
            .AppendInterval(3.0f)
            .Append(fade.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic))
            .AppendCallback(() => {
                fade.blocksRaycasts = false;
            })
            .AppendInterval(3.0f)
            .Append(chapterTitleText.DOFade(0, 1.0f).SetEase(Ease.OutCubic));
    }

    public void notificationAndCloseGameSequence()
    {
        DOTween.Sequence()
            .AppendInterval(2.0f)
            .Append(theEndCanvasGroup.DOFade(1, 2.0f).SetEase(Ease.OutSine))
            .AppendInterval(2.0f)
            .AppendCallback(() => MetaNarrativeManager.Instance.TriggerStorytellerSequence("SEQ_0"))
            .Append(notifcation.DOAnchorPosX(-64, 0.5f).SetEase(Ease.OutCubic))
            .AppendInterval(3.0f)
            .Append(notifcation.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic))
            .Join(topBar.DOAnchorPosY(0, 0.5f));
    }

    public void CloseGame()
    {
        fullScreenRawImage.DOScale(Vector3.zero, 1.0f).SetEase(Ease.InOutQuint).OnComplete( () => {
            topBar.gameObject.SetActive(false);
            mouseCursor.gameObject.SetActive(false);
            raliasLamentBuild.SetActive(false);
        });
    }
}