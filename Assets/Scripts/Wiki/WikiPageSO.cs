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
    [ShowIf("@ContainsBuild")]
    public string BuildTitle;
    [Tooltip("Allows for a page to be searched for by words that are not contained in title, subtitle, or content of the page. Keywords must be in lowercase!")]
    public List<string> AdditionalKeywords;

    List<string> KentoAliasess = new List<string>{"KentoWrites (narrative/puzzles)", "Narrative Chef", "Burning Down Your Kitchen", "Sisyphus Moment", "Kento's Still Writing", "Kento “Mister Hustle” Cox", "Kento’s Still Writing", "Kentusle", "Kento (Working)"};
    List<string> RoseAliasess = new List<string>{"rosygoldart (art)", "in the (art) trenches", "eepy artiste", "panera rose refresher", "toonity update anti", "punished rose", "wilting rose", "rose", "rosygoldart"};
    List<string> ErenAliasess = new List<string>{"erenlaiii (programming)", "Mooncake Dealer", "beep boop coder [ERROR]", "Mooncake's Indentured Servant", "Mooncake’s Indentured Servant", "Flashing Lights Warning", "Lai-ing on the floor", "Not Lai-king This Job Hunt", "Neverending Coding Problems"};

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
            if (line.StartsWith("-") || line.StartsWith("["))
            {
                archivedChat += $"{line}\n";
                continue;
            }

            string name = line.Split(":")[0]; 
            string content = line.Split(":")[1];

            if (KentoAliasess.Contains(name))
            {
                name = $"<color=#f5260f>{name}</color>";
            }
            else if (RoseAliasess.Contains(name))
            {
                name = $"<color=#c002d1>{name}</color>";
            }
            else if (ErenAliasess.Contains(name))
            {
                name = $"<color=#0bd400>{name}</color>";
            }

            archivedChat += $"<b>{name}:</b>{content}\n";
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