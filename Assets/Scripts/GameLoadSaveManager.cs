using UnityEngine;
using UnityEngine.SceneManagement;

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
            }   
            else
            {
                FindFirstObjectByType<OpeningSequenceManager>().StartGame();
            }         
        }
    }
}