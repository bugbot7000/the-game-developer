using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Wiki Index", menuName = "Their Game/Wiki Index SO")]
public class WikiIndexSO : ScriptableObject
{    
    [TitleGroup("Pages")]
    public List<WikiPageSO> WikiPages;
}