using UnityEngine;

public class CheckForMinotaurDeath : MonoBehaviour
{
    [SerializeField] GameObject minotaur;
    [SerializeField] GameObject loadArenaPanel;

    bool openedPanel;

    void Update()
    {
        if (!openedPanel)
        {
            if (minotaur == null)
            {
                loadArenaPanel.SetActive(true);
                openedPanel = true;
            }
        }
    }
}