using System.Collections.Generic;

using UnityEngine;

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
        //TODO: I might not have to sort by date if there are already correctly sorted?
        return searchForTerm(LastSearchTerm);
    }    
}