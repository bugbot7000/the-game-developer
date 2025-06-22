using UnityEngine;

using DG.Tweening;
using TMPro;

public class SecretRoomHandler : MonoBehaviour
{
    [SerializeField] string passphrase;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] CanvasGroup passphraseGroup;
    [SerializeField] TextMeshProUGUI feedbackText;
    [SerializeField] GameObject fakeWall;
    [SerializeField] GameObject hiddenRoom;

    bool submittingPassphrase;
    bool passphraseSubmittedCorrectly;

    void Update()
    {
        if (submittingPassphrase)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                hidePassphraseUI();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (inputField.text == passphrase)
                {
                    hidePassphraseUI();

                    fakeWall.SetActive(false);
                    hiddenRoom.SetActive(true);

                    passphraseSubmittedCorrectly = true;
                }
                else
                {
                    inputField.text = "";
                    inputField.GetComponent<RectTransform>().DOShakeAnchorPos(0.2f, 50, 100);
                    inputField.ActivateInputField();
                    feedbackText.SetText("Nothing happened...");
                }
            }
            else if (Input.anyKeyDown)
            {
                inputField.ActivateInputField();
            }
        }
    }

    void hidePassphraseUI()
    {
        inputField.text = "";
        inputField.DeactivateInputField();
        passphraseGroup.DOFade(0.0f, 1.0f).SetEase(Ease.OutCubic);
        submittingPassphrase = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!submittingPassphrase && !passphraseSubmittedCorrectly)
        {
            passphraseGroup.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic);
            inputField.ActivateInputField();
            submittingPassphrase = true;
        }
    }
}