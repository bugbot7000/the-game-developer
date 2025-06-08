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

    public bool HaveRequirementsBeenMet()
    {
        foreach (WikiPageSO page in WikiRequirements)
        {
            if (!WikiPageSearchManager.Instance.HasPageBeenVisited(page))
            {
                return false;
            }
        }

        foreach (string build in BuildRequirements)
        {
            if (!GameBuildManager.Instance.HasBuildBeenPlayed(build))
            {
                return false;
            }
        }

        return true;
    }

    // ✅ Write code to detect if build has played (loop through played build, chek title match if played)
    // ✅ Add warning icons to mark the needed ones in index
    // ✅ Add requirements to wikipage search manager
    // ✅ Gamepad emoji to mark if a build has been played
    // ✅ Modify requirements to actually check these new stuff
    // 6. rework index restorartion
    // (hide the little bars...)
    // (rework text...)
    // (make more explicit the remaining corrupt files)
    // 7. fix save and load system (needs to save whether a build has been added and plaid)
}