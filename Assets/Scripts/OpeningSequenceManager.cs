using UnityEngine;

using System.Collections;

using DG.Tweening;
using Michsky.DreamOS;

public class OpeningSequenceManager : MonoBehaviour
{
    //ralia's lament should have some dialogue...
    
    [SerializeField] bool skipOpening;
    [SerializeField] CanvasGroup fade;
    [SerializeField] Canvas dreamOS;
    [SerializeField] Canvas titleScreen;
    [SerializeField] GameObject raliasLamentBuild;
    [SerializeField] RectTransform notifcation;
    [SerializeField] RectTransform fullScreenRawImage;
    [SerializeField] RectTransform mouseCursor;
    [SerializeField] RectTransform topBar;
    [SerializeField] WindowManager messaging;
    [SerializeField] WindowManager webBrowser;

    IEnumerator Start()
    {
        yield return null;

        if (skipOpening)
        {
            Debug.Log("[OpeningSequenceManager] Skipping opening sequence.");

            dreamOS.gameObject.SetActive(true);
            titleScreen.gameObject.SetActive(false);
            fade.gameObject.SetActive(false);
            fullScreenRawImage.parent.gameObject.SetActive(false);
        }
        else
        {
            fade.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic).OnComplete(() => fade.blocksRaycasts = false);
        }
    }

    public void StartGame()
    {
        fade.blocksRaycasts = true;
        DOTween.Sequence()
            .Append(fade.DOFade(1.0f, 2.0f).SetEase(Ease.OutCubic))
            .AppendCallback(() => {
                dreamOS.gameObject.SetActive(true);
                raliasLamentBuild.SetActive(true);
                titleScreen.gameObject.SetActive(false);
                messaging.OpenWindow();
                webBrowser.OpenWindow();
                webBrowser.GetComponent<WebBrowserManager>().OpenPage("www.vapor.com/ralias-lament");
            })
            .AppendInterval(2.0f)
            .Append(fade.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic))
            .AppendCallback(() => {
                fade.blocksRaycasts = false;
                notificationAndCloseGameSequence();
            });
    }

    void notificationAndCloseGameSequence()
    {
        DOTween.Sequence()
            .AppendInterval(5.0f)
            .AppendCallback(() => MetaNarrativeManager.Instance.TriggerStorytellerSequence("SEQ_0"))
            .Append(notifcation.DOAnchorPosX(-64, 0.5f).SetEase(Ease.OutCubic))
            .AppendInterval(4.5f)
            .Append(notifcation.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic))
            .Join(mouseCursor.DOAnchorPosY(1025, 3.0f))
            .Join(topBar.DOAnchorPosY(0, 0.5f))
            .AppendInterval(1.5f)
            .Append(fullScreenRawImage.DOScale(Vector3.zero, 1.0f).SetEase(Ease.InOutQuint))
            .AppendCallback(() => topBar.gameObject.SetActive(false))
            .AppendCallback(() => mouseCursor.gameObject.SetActive(false))
            .AppendCallback(() => raliasLamentBuild.SetActive(false));
    }
}