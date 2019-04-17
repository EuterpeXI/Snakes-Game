using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Author: Amelia Chin
*/

public class doorEvent : MonoBehaviour
{
    public GameObject[] offSwitchList;
    public GameObject[] onSwitchList;
    public GameObject[] closedDoors;
    public GameObject[] openedDoors;

    private bool allTrue = false;
    private int count = 0;

    // Update is called once per frame
    void Update()
    {
        // For every off switch that gets triggered by the snake, the script SwitchInteractable will return true
        // If the switch is not activated, the script will return false
        for (int i = 0; i < offSwitchList.Length; i++){
            bool check = offSwitchList[i].GetComponent<SwitchInteractable>().get_trigger();

            // If the SwitchInteractable returns false at any point, the condition to open the door have not been met.
            // The particular offSwitch is still active, and the corresponding onSwitch is not active.
            if (check == false){
                offSwitchList[i].SetActive(true);
                onSwitchList[i].SetActive(false);
            }
            // Else, the SwitchInteractable returned true, which means that the condition to open the door have been met.
            // When true is returned for that particular switch, it is now "on." So offSwitch is not active and onSwitch is now active.
            else if (check == true) {
                offSwitchList[i].SetActive(false);
                onSwitchList[i].SetActive(true);
            }
        }

        allTrue = allActive();
        
        // If all the switches in the off switch list are true (meaning they are all activated), then we can go ahead and 
        // open the door.
        if (allTrue){
            for (int j = 0; j < closedDoors.Length; j++){
                openedDoors[j].SetActive(true);
                closedDoors[j].SetActive(false);
            }
        }
        // Else, close the door
        else{
            for (int j = 0; j < closedDoors.Length; j++){
                openedDoors[j].SetActive(false);
                closedDoors[j].SetActive(true);
            }
        }

    }

    private bool allActive(){
        bool check = true;

        for (int i = 0; i < onSwitchList.Length; i++){
            if (!onSwitchList[i].activeInHierarchy){
                check = false;
                break;
            }
        }

        return check;
    }
}
