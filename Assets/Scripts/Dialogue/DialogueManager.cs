using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    /*
    * This function handles the start of a conversation. Once the trigger event (e.g. a start button) 
    * is activated, the predefined sentences will queue up using the foreach function and
    * DisplayNextSentence() function will be called and will wait for the next event trigger
    * to display the next sentences in queue.
    */
    public void StartDialogue(Dialogue dialogue){
        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /*
    * This function takes the next sentence in the queue and displays it.
    * E.g. If the function is hooked up to a button onClick() event, it will display the next 
    * sentence in queue once that button is clicked.
    */
    public void DisplayNextSentence(){
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }

        string currentSentence = sentences.Dequeue();
        dialogueText.text = currentSentence;
        Debug.Log(currentSentence);
    }

    /*
    * This function is called when the sentence queue's end is reached.
    */
    void EndDialogue(){
        Debug.Log("End of Conversation");
    }

}
