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

    public WikiIndex Index => index;
    public string LastSearchTerm  { get; private set; }
    public WikiPage LastFoundPage { get; private set; }

    public bool CheckIfWikiPageExists()
    {
        foreach (WikiPage page in index.WikiPages)
        {
            if (page.Keywords.Contains(LastSearchTerm))
            {
                LastFoundPage = page;
                return true;
            }
        }

        return false;
    }

    public void SetSearchTerm(string term)
    {
        LastSearchTerm = term.ToLower().Trim();
    }
}