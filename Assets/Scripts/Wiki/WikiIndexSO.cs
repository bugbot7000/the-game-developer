using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Wiki Index", menuName = "Their Game/Wiki Index SO")]
public class WikiIndexSO : ScriptableObject
{
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
        
        foreach (WikiPageSO page in WikiPages)
            if (page.PageContainsTerm(term))
                results.Add(page);

        return results;
    }
    
    [TitleGroup("Pages")]
    public List<WikiPageSO> WikiPages;
}