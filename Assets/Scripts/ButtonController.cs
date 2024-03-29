﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public RectTransform joystick;
    public RectTransform button;
    public Vector2 spacing;
    public JoystickController playerController;
    private RectTransform prevJoystick;
    public bool secondButton;

    private void Start()
    {
        //Vector2 fixedSpacing = spacing / 100;

        //float spacerX = Screen.width * fixedSpacing.x;
        //float spacerY = Screen.height * fixedSpacing.y;

        prevJoystick = joystick;
        //if (secondButton)
        //    button.sizeDelta = new Vector2(prevJoystick.sizeDelta.x + 50, prevJoystick.sizeDelta.y + 50);
        //else
        //    button.sizeDelta = prevJoystick.sizeDelta;
        //button.position = new Vector3(spacerX,spacerY);

        playerController = GameObject.FindWithTag("Player").GetComponent<JoystickController>();
    }

    private void Update()
    {
        if (!prevJoystick.Equals(joystick.sizeDelta))
        {
            Vector2 fixedSpacing = spacing / 100;

            float spacerX = Screen.width * fixedSpacing.x;
            float spacerY = Screen.height * fixedSpacing.y;

            prevJoystick = joystick;
            if (secondButton)
            {
                button.sizeDelta = new Vector2(
                    prevJoystick.sizeDelta.x + prevJoystick.sizeDelta.x ,
                    prevJoystick.sizeDelta.y + prevJoystick.sizeDelta.y );
                button.position = new Vector3(
                    spacerX - prevJoystick.sizeDelta.y * 1 / 2, 
                    spacerY - prevJoystick.sizeDelta.y * 1 / 2);
            }
            else
            {
                button.sizeDelta = prevJoystick.sizeDelta;
                button.position = new Vector3(spacerX, spacerY);
            }
        }
    }

    public void FireSnakeThroughPlayerController()
    {
        playerController.FireSnake(
            Vector3.Normalize( 
                LaserPointController.position - playerController.transform.position
                )
            );
    }
}
