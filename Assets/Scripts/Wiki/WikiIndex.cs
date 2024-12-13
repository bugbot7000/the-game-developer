using System;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Wiki Index", menuName = "Their Game/Wiki Index")]
public class WikiIndex : ScriptableObject
{
    public List<WikiPage> WikiPages;
}

[Serializable]
public class WikiPage
{
    [FoldoutGroup("$ID")]
    public string Date;
    [FoldoutGroup("$ID")]
    public string Title;
    [FoldoutGroup("$ID")]
    public string Subtitle;
    [FoldoutGroup("$ID"), TextArea(1, 35)]
    public string Content;
    [FoldoutGroup("$ID"), HorizontalGroup("$ID/Chat")]
    public bool ActivatesChat;
    [FoldoutGroup("$ID"), HorizontalGroup("$ID/Chat"), ShowIf("@ActivatesChat")]
    public string ChatID;
    [FoldoutGroup("$ID"), HorizontalGroup("$ID/Build")]
    public bool ContainsBuild;
    [FoldoutGroup("$ID"), HorizontalGroup("$ID/Build"), ShowIf("@ContainsBuild")]
    public int BuildIndex;
    [FoldoutGroup("$ID")]
    public List<string> Keywords;

    public string ID => $"{Date} • {Title} • {Subtitle}";
}