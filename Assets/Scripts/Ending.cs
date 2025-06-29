using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;
using TMPro;
using Michsky.DreamOS;
using Sirenix.OdinInspector;

public class Ending : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI gameBy;
    [SerializeField] TextMeshProUGUI thanksTo;

    void Start()
    {
        DynamicMessageHandler.EndingCall = ShowEnding;
    }

    [Button]
    public void ShowEnding()
    {
        StartCoroutine(_ShowEnding());

        IEnumerator _ShowEnding()
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

            FindFirstObjectByType<MusicPlayerManager>()?.Stop();

            canvasGroup.blocksRaycasts = true;

            audioSource.Play();
            audioSource.DOFade(1.0f, 5.0f).SetEase(Ease.OutQuint);

            yield return canvasGroup.DOFade(1.0f, 5.0f).SetEase(Ease.OutQuint).WaitForCompletion();

            yield return new WaitForSeconds(1.5f);

            yield return title.DOFade(1, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
            yield return new WaitForSeconds(5f);
            yield return title.DOFade(0, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();

            yield return new WaitForSeconds(2.5f);

            yield return gameBy.DOFade(1, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
            yield return new WaitForSeconds(7f);
            yield return gameBy.DOFade(0, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();

            yield return new WaitForSeconds(1.5f);

            yield return thanksTo.DOFade(1, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
            yield return new WaitForSeconds(5f);
            yield return thanksTo.DOFade(0, 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();

            yield return audioSource.DOFade(0.0f, 10.0f).SetEase(Ease.OutCubic).WaitForCompletion();

            SceneManager.LoadSceneAsync(0);
        }
    }
}
