using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class SwitchInteractable : MonoBehaviour
{
    [SerializeField]
    bool isAlarm = false;
    private bool switchTrigger = false;

    bool OnTriggerStay2D(Collider2D other)
    {
        // If the switch box collider collided with a GameObject that is tagged with "snake_bullet", switch is on/active
        if (other.tag == "snake_bullet" || (other.CompareTag("Mouse") && isAlarm)){
            switchTrigger = true;
        }
        
        return false;
    }
}
