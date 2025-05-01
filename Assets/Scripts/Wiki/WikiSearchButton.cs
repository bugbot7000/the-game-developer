using UnityEngine;

using Michsky.DreamOS;

public class WikiSearchButton : MonoBehaviour
{
    WebBrowserManager webBrowserManager;
    RandomAudioQueue randomAudioQueue;
    AudioSourceHandler audioSourceHandler;
    
    void Start()
    {
        webBrowserManager = FindAnyObjectByType<WebBrowserManager>();
        audioSourceHandler = GetComponent<AudioSourceHandler>();
        randomAudioQueue = GetComponent<RandomAudioQueue>();
    }
    
    public void Search(string content)
    {
        if (!Input.GetButtonDown("Submit"))
            return;

        audioSourceHandler.PlayAudioByName("enter_key");

        WikiPageSearchManager.Instance.SetSearchTerm(content);

        webBrowserManager.OpenPage($"wiki.eren.local/search");
    }

    public void HandleTypingSound()
    {
        if (!Input.anyKeyDown)
            return;

        randomAudioQueue.PlayRandomSound();
    }

    public void ReturnToPreviousSearch()
    {
        webBrowserManager.OpenPage($"wiki.eren.local/search");
    }

    public void GoToArchive()
    {
        webBrowserManager.OpenPage("wiki.eren.local/archive");
    }
}