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

    // this is slightly annoying due to my inconsistency in real index vs index in which they were added to the game hub...
    // easiet way to handle is probably just by creating a helper method in the game build manager
    // use string, and loop through everything...

    // i also need to figure out how to visualize this? i honestly don't think that replicating the 
    // index view in the index restoarion is the best, I like the bards...
    // maybe a visual indicator in the index page (what icon would make sense? a star, oh maybe the warning emoji),
    // and a gamepad emoji to indicate you;ve played it? would maybe want to separte that out in the index restorationi too
    //  and the index restoration is still a bar, but also has a text saying "corrupted files left".
    // only kind of weird think is i also need to visualize the builds being played
    // (and not only the index page being loaded)
}