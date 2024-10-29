using UnityEngine;
using TMPro;

public class UnlockWebsite : MonoBehaviour
{
    [SerializeField] string password;
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] TMP_InputField input;
    [SerializeField] GameObject website;

    public void CheckPassword()
    {
        if (input.text == password)
        {
            website.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            promptText.text = "Incorrect password.";
        }
    }
}