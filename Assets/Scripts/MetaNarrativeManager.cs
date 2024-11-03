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
    
    [Button, DisableInEditorMode, PropertySpace(8)]
    public void TriggerBillStorytellerSequence(string id)
    {
        messaging.CreateStoryTeller("Test", id);
    }
}