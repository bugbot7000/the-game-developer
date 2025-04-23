using UnityEngine;

/// <summary>
/// Helper class to clear wiki history on tab being closed.
/// </summary>
public class CallWikiClearHistory : MonoBehaviour
{
    public void ClearTabHistory()
    {
        WikiHistoryManager.Instance.ClearHistory();
    }
}