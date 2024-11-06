using System;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Wiki Index", menuName = "Their Game/Wiki Index")]
public class WikiIndex : SerializedScriptableObject
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
    public Dictionary<string, WikiPage> WikiPages;
}

[Serializable]
public class WikiPage
{
    public string Title;
    [TextArea]
    public string Content;
}