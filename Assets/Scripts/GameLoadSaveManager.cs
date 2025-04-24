using UnityEngine;
using UnityEngine.SceneManagement;

using Michsky.DreamOS;

public class GameLoadSaveManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnSceneChange;
    }

    void OnSceneChange(Scene current, Scene next)
    {
        if (next.name == "Desktop")
        {
            if (PlayerPrefs.HasKey("CompletedIntro") && PlayerPrefs.GetInt("CompletedIntro") == 1)
            {
                FindFirstObjectByType<OpeningSequenceManager>().SkipOpening();
                FindFirstObjectByType<WikiPageSearchManager>().Load();
                FindFirstObjectByType<NotepadManager>(FindObjectsInactive.Include)
                    .UpdateFirstNoteContent(PlayerPrefs.GetString("notepad"));
            }   
            else
            {
                FindFirstObjectByType<OpeningSequenceManager>().StartGame();
            }         
        }
    }
}