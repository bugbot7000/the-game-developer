using UnityEngine;

using Michsky.DreamOS;

//TODO: Rename this to search bar....
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

        webBrowserManager.OpenPage($"wiki.eren.local/search");
    }
}