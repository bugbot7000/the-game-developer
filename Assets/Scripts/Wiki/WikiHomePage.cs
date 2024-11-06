using UnityEngine;

using Michsky.DreamOS;

public class WikiHomePage : MonoBehaviour
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

        webBrowserManager.OpenPage($"wiki.thelastsorcerer.com/search");
    }
}