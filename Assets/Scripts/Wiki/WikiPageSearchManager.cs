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

    [SerializeField] WikiIndex index;

    public string LastSearchTerm { get; private set; }

    public bool WikiPageExists()
    {
        return index.WikiPages.ContainsKey(LastSearchTerm);
    }

    public void SetSearchTerm(string term)
    {
        LastSearchTerm = term.ToLower().Trim().Replace(" ", "-");
    }

    public WikiPage GetLastSearchedPage()
    {
        return index.WikiPages[LastSearchTerm];
    }
}