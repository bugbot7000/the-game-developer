using System;
using System.Collections.Generic;

using UnityEngine;

using Michsky.DreamOS;
using Sirenix.OdinInspector;

public class WikiHistoryManager : SerializedMonoBehaviour
{
    #region Singleton
    public static WikiHistoryManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion    
    
    // We initalize arrays with length 4 since our maximum is 4 tabs.
    [SerializeField, ReadOnly] int[] tabHistoryIndex = {0, 0, 0, 0};
    [SerializeField, ReadOnly] List<HistoryItem>[] history = 
        {new List<HistoryItem>(), new List<HistoryItem>(), new List<HistoryItem>(), new List<HistoryItem>(),};

    WebBrowserManager webBrowserManager;

    bool block;
    
    void Start()
    {
        webBrowserManager = FindFirstObjectByType<WebBrowserManager>();
    }

    public void GoBack()
    {
        int openTab = webBrowserManager.GetOpenTabIndex();
        
        // Cannot go back if you are on the first page.
        if (tabHistoryIndex[openTab] == 1)
            return;

        block = true;

        tabHistoryIndex[openTab]--;

        HistoryItem itemToOpen = history[openTab][tabHistoryIndex[openTab] - 1];

        LoadHistory(itemToOpen);
    }

    public void GoForward()
    {
        int openTab = webBrowserManager.GetOpenTabIndex();
        
        // Cannot go forward if you are on the last page.
        if (tabHistoryIndex[openTab] == history[openTab].Count)
            return;

        block = true;

        tabHistoryIndex[openTab]++;

        HistoryItem itemToOpen = history[openTab][tabHistoryIndex[openTab] - 1];

        LoadHistory(itemToOpen);
    }

    public void LoadHistory(HistoryItem item)
    {
        switch (item.Page)
        {
            case PageType.Home:
                webBrowserManager.OpenHomePage();
                break;

            case PageType.Search:
                WikiPageSearchManager.Instance.SetSearchTerm(item.SearchTerm);
                webBrowserManager.OpenPage("wiki.eren.local/search");
                break;

            case PageType.Content:
                WikiPageSearchManager.Instance.SetPageToLoad(item.Content);
                webBrowserManager.OpenPage("wiki.eren.local/content");
                break;
            
            case PageType.Index:
                webBrowserManager.OpenPage("wiki.eren.local/archive");
                break;
        }
    }

    // A history for a tab is cleared if it's closed.
    public void ClearHistory()
    {
        int openTab = webBrowserManager.GetOpenTabIndex();

        history[openTab] = new List<HistoryItem>();
        tabHistoryIndex[openTab] = 0;
    }

    public void AddPageToHistory(HistoryItem item)
    {
        // We use the block to avoid pages from being added to the history when the player is using the back or forward button.
        if (block)
        {
            block = false;

            return;
        }

        int openTab = webBrowserManager.GetOpenTabIndex();

        // If we are at the latest point in the history of this tab, we add the history item to the end of the list.
        if (tabHistoryIndex[openTab] == history[openTab].Count)
        {
            history[openTab].Add(item);
        }
        // If not, it means we've gone back, and thus will erase the history from this point onwards.
        else
        {
            history[openTab] = history[openTab].GetRange(0, tabHistoryIndex[openTab]);
            history[openTab].Add(item);
        }

        tabHistoryIndex[openTab]++;
    }
}

public enum PageType {Home, Search, Content, Index}

[Serializable]
public class HistoryItem
{
    public PageType Page;
    [ShowIf("@Page==PageType.Search")]
    public string SearchTerm;
    [ShowIf("@Page==PageType.Content")]
    public WikiPageSO Content; 

    public HistoryItem(PageType type, string term="", WikiPageSO content=null)
    {
        Page = type;
        SearchTerm = term;
        Content = content;
    }
}