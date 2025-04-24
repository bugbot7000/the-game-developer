using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveAndQuitHelper : MonoBehaviour
{
    public void SaveAndQuit()
    {
        PlayerPrefs.Save();

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;         
        #else
            Application.Quit();   
        #endif        
    }
}