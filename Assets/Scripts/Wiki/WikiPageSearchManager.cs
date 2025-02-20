using System.Collections.Generic;

using UnityEngine;

using AptabaseSDK;
using Sirenix.OdinInspector;

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
    [SerializeField] WikiIndexSO newIndex;

    [TitleGroup("Parameters")]
    [SerializeField] int maxSearchResults = 3;  
    
    WikiIndex index;
    List<WikiPageSO> visitedPages = new List<WikiPageSO>();

    public WikiIndex Index => index;
    public WikiIndexSO NewIndex => newIndex;
    public int MaxSearchResults => maxSearchResults;
    public string LastSearchTerm  { get; private set; }
    public WikiPage LastFoundPage { get; private set; }
    public WikiPageSO LastFoundPageSO { get; private set; }

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
        
        foreach (WikiPageSO page in newIndex.WikiPages)
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
}