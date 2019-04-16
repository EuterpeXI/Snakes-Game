using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MobileJoystickPlayerController joystick;
    public LaserPointController laserPoint;
    public TopDownMovementController playerMovement;
    public PlayerController player;

    private int joystickFingerId = -1;

    void Update()
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        laserPoint.MoveLaser(Input.mousePosition);
        playerMovement.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized);
        if (Input.GetButtonDown("Fire1"))
        {
            player.FireSnake((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
#else
        bool joystickReleased = true;
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(i);
            Vector3 touchLocation = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                if ((touch.position - (Vector2)joystick.transform.position).magnitude < joystick.joystickRadius)
                {
                    joystickFingerId = touch.fingerId;
                }
            }
            
            if (touch.fingerId == joystickFingerId)
            {
                playerMovement.Move(joystick.OnJoystickDrag(touch.position));
                joystickReleased = false;
            }
            else
            {
                laserPoint.MoveLaser(touch.position);

                if (touch.tapCount >= 2)
                {
                    player.FireSnake(touchLocation);
                    touch.tapCount = 1;
                }
            }
        }
        if (joystickReleased)
        {
            playerMovement.Move(joystick.ResetJoystick());
            joystickFingerId = -1;
        }
#endif
    }
}
