using UnityEngine;

public class CheckForMinotaurDeath : MonoBehaviour
{
    [SerializeField] GameObject minotaur;

    bool openedPanel;

    void Update()
    {
        if (!openedPanel)
        {
            if (minotaur == null)
            {
                FindFirstObjectByType<ThanksCanvas>()?.OpenPanel();
                openedPanel = true;
            }
        }
    }
}