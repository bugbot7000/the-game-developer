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

    public int MissingRequirementPages()
    {
        int missing = 0;

        foreach (WikiPageSO page in WikiRequirements)
        {
            if (!WikiPageSearchManager.Instance.HasPageBeenVisited(page))
            {
                missing++;
            }
        }

        return missing;
    }

    public int MissingRequirementBuilds()
    {
        int missing = 0;

        foreach (string build in BuildRequirements)
        {
            if (!GameBuildManager.Instance.HasBuildBeenPlayed(build))
            {
                missing++;
            }
        }        

        return missing;
    }
}