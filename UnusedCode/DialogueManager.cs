using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI Elements")]
    public Canvas dialogueCanvas;           // The canvas that contains the dialogue UI
    public TextMeshProUGUI dialogueText;    // The TextMeshPro text component

    [Header("Settings")]
    public float letterDelay = 0.05f;       // Delay between each letter

    private Queue<string> dialogues;        // Queue to hold dialogue lines
    private bool isTyping = false;          // Is a dialogue currently being typed out?
    private bool canProceed = false;        // Can the player proceed to the next dialogue?

    void Start()
    {
        dialogues = new Queue<string>();
        dialogueCanvas.enabled = false;     // Hide dialogue canvas at the start
    }

    void Update()
    {
        // Listen for left-click input to proceed
        if (canProceed && Input.GetMouseButtonDown(0))
        {
            DisplayNextDialogue();
        }
    }

    /// <summary>
    /// Starts the dialogue sequence.
    /// </summary>
    /// <param name="dialogueLines">A list of dialogue strings to display.</param>
    public void StartDialogue(List<string> dialogueLines)
    {
        dialogueCanvas.enabled = true;      // Show the dialogue canvas
        dialogues.Clear();

        foreach (string line in dialogueLines)
        {
            dialogues.Enqueue(line);        // Enqueue all dialogue lines
        }

        DisplayNextDialogue();
    }

    /// <summary>
    /// Displays the next dialogue in the queue.
    /// </summary>
    void DisplayNextDialogue()
    {
        if (dialogues.Count == 0)
        {
            dialogueCanvas.enabled = false; // Hide canvas if no dialogues are left
            return;
        }

        string sentence = dialogues.Dequeue();
        StopAllCoroutines();                // Stop any ongoing typing coroutine
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Types out the sentence letter by letter.
    /// </summary>
    /// <param name="sentence">The sentence to display.</param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        canProceed = false;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        isTyping = false;
        canProceed = true;
    }
}