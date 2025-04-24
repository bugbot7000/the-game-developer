using UnityEngine;
using Michsky.DreamOS;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveAndQuitHelper : MonoBehaviour
{
    public void SaveAndQuit()
    {
        PlayerPrefs.SetString("notepad", FindFirstObjectByType<NotepadManager>(FindObjectsInactive.Include).GetFirstNoteContent());

        PlayerPrefs.Save();

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;         
        #else
            Application.Quit();   
        #endif        
    }
}