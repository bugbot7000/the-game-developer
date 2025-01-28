using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using TMPro;

public class WikiSearchResultsPage : MonoBehaviour
{
    [TitleGroup("Prefabs"), AssetsOnly]
    [SerializeField] GameObject searchResultButtonPrefab;

    [TitleGroup("References")]
    [SerializeField] GameObject searchResultsContainer;
    [SerializeField] TextMeshProUGUI searchResultsText;

    WikiPageSearchManager searchManager => WikiPageSearchManager.Instance;

    void Start()
    {
        List<WikiPageSO> searchResults = searchManager.GetSearchResultsForLastTerm();

        for (int i = 0; i < searchManager.MaxSearchResults && i < searchResults.Count; i++)
        {
            Instantiate(searchResultButtonPrefab, searchResultsContainer.transform)
                .GetComponent<WikiSearchResultButton>().InitializeButton(searchResults[i]);
        }

        if (searchResults.Count == 0)
        {
            searchResultsText.SetText($"No results were found for the given search term '{searchManager.LastSearchTerm}'.");
        }
        else if (searchResults.Count > searchManager.MaxSearchResults)
        {
            searchResultsText.SetText($"{searchResults.Count} results were found for the given search term '{searchManager.LastSearchTerm}'. The first {searchManager.MaxSearchResults} are shown.");
        }
        else
        {
            searchResultsText.SetText($"{searchResults.Count} results were found for the given search term '{searchManager.LastSearchTerm}'.");
        }
    }
}