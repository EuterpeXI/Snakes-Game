using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

/*
Author: Amelia Chin
*/
public class SwitchInteractable : MonoBehaviour
{
    [SerializeField]
    bool isAlarm = false;
    private bool switchTrigger = false;

    /*
    * This trigger function will detect if the switch had collided with an object tagged with "snake_bullet" and sets the value
    * of the switchTrigger boolean based on collision.
    */
    void OnTriggerStay2D(Collider2D other)
    {
        // If the switch box collider collided with a GameObject that is tagged with "snake_bullet", switch is on/active
        if (other.tag == "snake_bullet" || (other.CompareTag("Mouse") && isAlarm)){
            switchTrigger = true;
        }
        
        // Else, switch is still off/inactive
        switchTrigger = false;
    }

    /* 
    * This function returns the value of switchTrigger when called. 
    * switchTrigger returns true if the switch collided with the snake bullet
    * switchTrigger returns false if the switch did not collide with the snake bullet
    */
    public bool get_trigger(){
        return switchTrigger;
    }

}
