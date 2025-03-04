using UnityEngine;

using Michsky.DreamOS;

public class WikiSearchButton : MonoBehaviour
{
    WebBrowserManager webBrowserManager;
    
    void Start()
    {
        webBrowserManager = FindAnyObjectByType<WebBrowserManager>();
    }
    
    public void Search(string content)
    {
        if (!Input.GetButtonDown("Submit"))
            return;

        WikiPageSearchManager.Instance.SetSearchTerm(content);

        webBrowserManager.OpenPage($"wiki.eren.local/search");
    }

    public void ReturnToPreviousSearch()
    {
        webBrowserManager.OpenPage($"wiki.eren.local/search");
    }

    public void GoToArchive()
    {
        webBrowserManager.OpenPage("wiki.eren.local/archive");
    }
}