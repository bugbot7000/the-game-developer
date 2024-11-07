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
    [FoldoutGroup("$ID"), TextArea(1, 5)]
    public string Content;
    [FoldoutGroup("$ID")]
    public List<string> Keywords;

    public string ID => $"{Date} • {Title} • {Subtitle}";
}