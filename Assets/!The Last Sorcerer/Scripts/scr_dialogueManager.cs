using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_dialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText; 
    public TextMesh exampleText;
    public CanvasGroup canvasGroup;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        canvasGroup.alpha = 0;
    }

    public void StartDialogue(scr_dialogueScript dialogue)
    {
        canvasGroup.alpha = 1;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        canvasGroup.alpha = 0;
    }
}
