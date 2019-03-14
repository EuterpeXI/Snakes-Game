using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class SwitchInteractable : MonoBehaviour
{

    bool OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "snake_bullet"){
            // Have the script for the doors handle the conditions
            return true;
        }
        
        return false;
    }
}
