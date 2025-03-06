using UnityEngine;

using Michsky.DreamOS;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SearchableTextMeshPro : MonoBehaviour
{
    WebBrowserManager webBrowserManager;
    TextMeshProUGUI text;
    bool blocked;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        webBrowserManager = FindAnyObjectByType<WebBrowserManager>();
    }

    void Update()
    {
        if (blocked)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            int wordIndex = TMP_TextUtilities.FindIntersectingWord(text, Input.mousePosition, null);

            if (wordIndex != -1)
            {
                string textString = text.text;
                string wordString = text.textInfo.wordInfo[wordIndex].GetWord();

                textString = textString.Remove(
                    text.textInfo.wordInfo[wordIndex].firstCharacterIndex,  
                    text.textInfo.wordInfo[wordIndex].characterCount
                );
                
                textString = textString.Insert(
                    text.textInfo.wordInfo[wordIndex].firstCharacterIndex,
                    $"<b><u>{wordString}</b></u>"
                );
                
                WikiPageSearchManager.Instance.SetSearchTerm(wordString);

                webBrowserManager.OpenPage($"wiki.eren.local/search");

                text.SetText(textString);

                blocked = true;
            }
        }
    }
}