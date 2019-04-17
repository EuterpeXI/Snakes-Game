using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This script displays the structure for inputting sentences for dialogue into a format that Unity 
* can store and reconstruct later (serialize). 
* Generally, this script acts as a middleman between the DialogueManager and the DialogueTrigger
* attached to the object. 
*/
[System.Serializable] 
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
