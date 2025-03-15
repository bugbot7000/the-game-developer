using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Wiki Page", menuName = "Their Game/Wiki Page")]
public class WikiPageSO : ScriptableObject
{
    public WikiPageType PageType = WikiPageType.Devlog;
    public string Date;
    public string Title;
    [TextArea(3, 4)]
    public string Subtitle;
    [TextArea(1, 35)]
    public string Content;
    public bool IsArchivedChat;
    public bool ActivatesInteractveChat;
    [ShowIf("@ActivatesInteractveChat")]
    public string ChatID;
    public bool ContainsBuild;
    [ShowIf("@ContainsBuild")]
    public int BuildIndex;
    [Tooltip("Allows for a page to be searched for by words that are not contained in title, subtitle, or content of the page. Keywords must be in lowercase!")]
    public List<string> AdditionalKeywords;

    public bool PageContainsTerm(string term)
    {
        return Title.Contains(term, System.StringComparison.CurrentCultureIgnoreCase) || 
            Subtitle.Contains(term, System.StringComparison.CurrentCultureIgnoreCase) || 
            Content.Contains(term, System.StringComparison.CurrentCultureIgnoreCase)  || 
            AdditionalKeywords.Contains(term.ToLower());
    }

    public string ContentAsArchivedChat()
    {
        string archivedChat = "";

        foreach (string line in Content.Split('\n'))
        {
            string name = line.Split(":")[0]; 
            string content = line.Split(":")[1];

            archivedChat += $"<b>{name}:</b><indent=3.5em>{content}</indent>\n";
        }

        return archivedChat;
    }
}

public enum WikiPageType {
    Devlog,
    Lore,
    Build,
    Chat
}