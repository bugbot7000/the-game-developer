using System.Collections.Generic;

using UnityEngine;

using AptabaseSDK;
using Sirenix.OdinInspector;
using Michsky.DreamOS;

[TypeInfoBox("Handles the search function and keeping track of which pages have been visited.")]
public class WikiPageSearchManager : MonoBehaviour
{
    #region Singleton
    public static WikiPageSearchManager Instance;

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

    [TitleGroup("Wiki Index")]
    [SerializeField] WikiIndexSO fallIndex;
    [SerializeField] WikiIndexSO springIndex;
    [SerializeField] WikiIndexSO summerIndex;
    [SerializeField] bool enableWikiDebug;
    [SerializeField, ShowIf("@enableWikiDebug")] bool debugFall;
    [SerializeField, ShowIf("@enableWikiDebug")] bool debugSpring;
    [SerializeField, ShowIf("@enableWikiDebug")] bool debugSummer;

    [TitleGroup("Requirements")]
    [SerializeField] SemesterUnlockRequirements springUnlockRequirements;
    [SerializeField] SemesterUnlockRequirements summerUnlockRequirements;
    [SerializeField] SemesterUnlockRequirements endingUnlockRequirements;

    [TitleGroup("Parameters")]
    [SerializeField] int maxSearchResults = 3;  
    [SerializeField] float progressToUnlockSpring = 0.8f;
    [SerializeField] float progressToUnlockSummer = 0.7f;

    [TitleGroup("References")]
    [SerializeField] WindowManager webBrowser;
    [SerializeField] NetworkManager networkManager;

    [TitleGroup("Assets")]
    [SerializeField] Sprite notificationSprite;
    
    WikiIndex index;
    List<WikiPageSO> visitedPages = new List<WikiPageSO>();

    public WikiIndex Index => index;
    public WikiIndexSO FallIndex => fallIndex;
    public WikiIndexSO SpringIndex => springIndex;
    public WikiIndexSO SummerIndex => summerIndex;
    public int MaxSearchResults => maxSearchResults;
    public string LastSearchTerm  { get; private set; }
    public WikiPage LastFoundPage { get; private set; }
    public WikiPageSO LastFoundPageSO { get; private set; }
    public float ProgressToUnlockSpring => progressToUnlockSpring;
    public float ProgressToUnlockSummer => progressToUnlockSummer;

    bool hasShownSpringNotification;
    bool hasShowSummerNotification;    

    void Start()
    {
        if (enableWikiDebug)
        {
            Debug.Log("[WikiPageSearchManager] Wiki debug enabled.");

            if (debugFall)
            {
                foreach (WikiPageSO wikiPage in fallIndex.WikiPages)
                {
                    visitedPages.Add(wikiPage);
                }
            }

            if (debugSpring)
            {
                foreach (WikiPageSO wikiPage in springIndex.WikiPages)
                {
                    visitedPages.Add(wikiPage);
                }
            }

            if (debugSummer)
            {
                foreach (WikiPageSO wikiPage in summerIndex.WikiPages)
                {
                    visitedPages.Add(wikiPage);
                }  
            }
          
            webBrowser.OpenWindow();
            webBrowser.GetComponent<WebBrowserManager>().OpenPage("wiki.eren.local/archive");    
            networkManager.networkItems[0].networkSpeed = 100;        
        }
    }

    void Update()
    {
        if (SpringUnlocked() && !hasShownSpringNotification)
        {
            NotificationManager.CreateNotification(notificationSprite, "New Database Available", "Spring", true);
            hasShownSpringNotification = true;
        }

        if (SummerUnlocked() && !hasShowSummerNotification)
        {
            NotificationManager.CreateNotification(notificationSprite, "New Database Available", "Summer", true);
            hasShowSummerNotification = true;
        }        
    }

    public bool IsPageRequirement(WikiPageSO page)
    {
        if (springUnlockRequirements.ContainsPage(page) || summerUnlockRequirements.ContainsPage(page) || endingUnlockRequirements.ContainsPage(page))
            return true;

        return false;
    }

    public float GetTotalProgress()
    {
        float visited = 0;

        foreach (WikiPageSO wikiPage in fallIndex.WikiPages)
            if (HasPageBeenVisited(wikiPage))
                visited++;

        foreach (WikiPageSO wikiPage in springIndex.WikiPages)
            if (HasPageBeenVisited(wikiPage))
                visited++;

        foreach (WikiPageSO wikiPage in summerIndex.WikiPages)
            if (HasPageBeenVisited(wikiPage))
                visited++;

        return visited / (fallIndex.WikiPages.Count + springIndex.WikiPages.Count + summerIndex.WikiPages.Count);
    }

    public float GetFallSemesterProgress()
    {
        float visited = 0;

        foreach (WikiPageSO wikiPage in fallIndex.WikiPages)
            if (HasPageBeenVisited(wikiPage))
                visited++;

        return visited / fallIndex.WikiPages.Count;
    }

    public bool SpringUnlocked()
    {
        return springUnlockRequirements.HaveRequirementsBeenMet();
    }

    public float GetSpringSemesterProgress()
    {
        float visited = 0;

        foreach (WikiPageSO wikiPage in springIndex.WikiPages)
            if (HasPageBeenVisited(wikiPage))
                visited++;        

        return visited / springIndex.WikiPages.Count;
    }

    public bool SummerUnlocked()
    {
        return summerUnlockRequirements.HaveRequirementsBeenMet();
    }

    public float GetSummerProgress()
    {
        float visited = 0;

        foreach (WikiPageSO wikiPage in summerIndex.WikiPages)
            if (HasPageBeenVisited(wikiPage))
                visited++;        

        return visited / summerIndex.WikiPages.Count;
    }    

    public void SetSearchTerm(string term)
    {
        LastSearchTerm = term.ToLower().Trim();
    }

    public void SetPageToLoad(WikiPageSO page)
    {
        Aptabase.TrackEvent("page_visit", new Dictionary<string, object> {{"first_time", !visitedPages.Contains(page)}, {"page", page.Title} });
        
        if (!visitedPages.Contains(page))
        {
            visitedPages.Add(page);
            Save();
        }

        LastFoundPageSO = page;
    }

    public bool HasPageBeenVisited(WikiPageSO page)
    {
        return visitedPages.Contains(page);
    }

    [TitleGroup("Debug")]
    [Button]
    void testSearch(string term)
    {
        List<WikiPageSO> results = searchForTerm(term);

        Debug.Log($"[WikiIndexSO] Found {results.Count} for search term '{term}':"); 

        foreach (WikiPageSO result in results)
            Debug.Log($"    {result.Title}");
    }

    List<WikiPageSO> searchForTerm(string term)
    {
        List<WikiPageSO> results = new List<WikiPageSO>();
        
        foreach (WikiPageSO page in fallIndex.WikiPages)
            if (page.PageContainsTerm(term))
                results.Add(page);
        
        if (SpringUnlocked())
            foreach (WikiPageSO page in springIndex.WikiPages)
                if (page.PageContainsTerm(term))
                    results.Add(page);

        if (SummerUnlocked())
            foreach (WikiPageSO page in summerIndex.WikiPages)
                if (page.PageContainsTerm(term))
                    results.Add(page);                                      

        return results;
    }

    public List<WikiPageSO> GetSearchResultsForLastTerm()
    {
        List<WikiPageSO> results = searchForTerm(LastSearchTerm);

        if (results.Count > 0)
        {
            string names = "";

            for (int i = 0; i < MaxSearchResults && i < results.Count; i++)
            {
                string visitedCharacter = HasPageBeenVisited(results[i]) ? "✔" : "";

                names += $"({visitedCharacter}) {results[i].Title} - ";
            }

            names = names.Substring(0, names.Length - 3);
            
            Aptabase.TrackEvent("search_results", new Dictionary<string, object> {{"term", LastSearchTerm}, {"count", results.Count}, {"shown_pages", names} });
        }       
        else
        {
            Aptabase.TrackEvent("no_results_found", new Dictionary<string, object> {{"term", LastSearchTerm} });
        }
        
        return results;
    }

    [Button]
    public void Save()
    {
        foreach (WikiPageSO visited in visitedPages)
        {
            PlayerPrefs.SetInt(visited.name, 1);
        }
    }   

    [Button]
    public void Load()
    {
        foreach (WikiPageSO page in fallIndex.WikiPages)
        {
            if (PlayerPrefs.HasKey(page.name) && PlayerPrefs.GetInt(page.name) == 1)
            {
                visitedPages.Add(page);
            }
        }
        
        foreach (WikiPageSO page in springIndex.WikiPages)
        {
            if (PlayerPrefs.HasKey(page.name) && PlayerPrefs.GetInt(page.name) == 1)
            {
                visitedPages.Add(page);
            }
        }

        foreach (WikiPageSO page in summerIndex.WikiPages)
        {
            if (PlayerPrefs.HasKey(page.name) && PlayerPrefs.GetInt(page.name) == 1)
            {
                visitedPages.Add(page);
            }            
        }        
    } 
}