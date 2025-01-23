using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Wiki Page", menuName = "Their Game/Wiki Page")]
public class WikiPageSO : ScriptableObject
{
    public string Date;
    public string Title;
    [TextArea(3, 4)]
    public string Subtitle;
    [TextArea(1, 35)]
    public string Content;
    [HorizontalGroup("Chat")]
    public bool ActivatesChat;
    [HorizontalGroup("Chat"), ShowIf("@ActivatesChat")]
    public string ChatID;
    [HorizontalGroup("Build")]
    public bool ContainsBuild;
    [HorizontalGroup("Build"), ShowIf("@ContainsBuild")]
    public int BuildIndex;
    [Tooltip("Allows for a page to be searched for by words that are not contained in title, subtitle, or content of the page.")]
    public List<string> AdditionalKeywords;
}