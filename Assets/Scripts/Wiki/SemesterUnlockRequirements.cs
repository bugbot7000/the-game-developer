using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SemesterUnlockRequirements", menuName = "Their Game/Requirements")]
public class SemesterUnlockRequirements : ScriptableObject
{
    [TitleGroup("Required Pages")]
    public List<WikiPageSO> WikiRequirements;

    [TitleGroup("Required Builds")]
    public List<string> BuildRequirements;

    public bool ContainsPage(WikiPageSO page)
    {
        return WikiRequirements.Contains(page) || BuildRequirements.Contains(page.BuildTitle);
    }

    // ✅ Write code to detect if build has played (loop through played build, chek title match if played)
    // ✅ Add warning icons to mark the needed ones in index
    // ✅ Add requirements to wikipage search manager
    // ✅ Gamepad emoji to mark if a build has been played
    // 5. Modify requirements to actually check these new stuff
    // (where is the index restoration actually running anyways?)
    // (aha... it works in the page search manager, the index restoration just does visualizatoin)
    // 6. rework index restorartion
    // ....does my save and load system save and load build?
}