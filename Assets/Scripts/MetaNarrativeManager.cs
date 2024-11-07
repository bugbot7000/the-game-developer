using System.Collections.Generic;

using UnityEngine;

using Michsky.DreamOS;
using Sirenix.OdinInspector;

[TypeInfoBox("Handles triggering narrative events depending on the player's actions in different parts of the game such as playing builds or finding pages in the wiki.")]
public class MetaNarrativeManager : MonoBehaviour
{
    #region Singleton
    public static MetaNarrativeManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    [SerializeField] MessagingManager messaging;

    List<string> visitedSequences = new List<string>();
    
    [Button, DisableInEditorMode, PropertySpace(8)]
    public void TriggerStorytellerSequence(string id)
    {
        visitedSequences.Add(id);
        messaging.CreateStoryTeller("Phantom", id);
    }

    public bool HasVisitedSequence(string id)
    {
        return visitedSequences.Contains(id);
    }
}