using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LaserPointController laserPoint;
    public JoystickController player;
    public float TapThreshold = 0.3f;

    private int joystickFingerId = -1;
    private Transform playerTransform;
    private float timeDown = 0f;

    private void Start()
    {
        playerTransform = this.transform;
    }

    private void Update()
    {
        //var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID
        bool buttDown = Input.GetButton("Fire1");
        bool buttUp = Input.GetButtonUp("Fire1");

        if (buttDown && !buttUp)
        {
            laserPoint.MoveLaser(Input.mousePosition);
        }
        else
        {
            laserPoint.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            laserPoint.GetComponent<SpriteRenderer>().enabled = false;
        }
        #endif
    }

    void NotUpdate()
    {
//        #undef UNITY_EDITOR
        #if UNITY_EDITOR || UNITY_STANDALONE
        bool isMouseButtonDown = Input.GetButton("Fire1");
        bool didUserReleaseClick = Input.GetButtonUp("Fire1");
        Vector3 pos = Vector3.zero;
        
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // update laser point for holds and mouse moves
        laserPoint.MoveLaser(pos);

        if (isMouseButtonDown)
            timeDown += Time.deltaTime;

        bool isTouchATap = didUserReleaseClick && timeDown < TapThreshold;
        
        //fire snake if click or tap 
        if (isTouchATap)
        {
            player.FireSnake(Vector3.Normalize(pos - playerTransform.position));
            Debug.Log("touchy");
        }
        
        //must happen AFTER the tap threshold check
        if (!isMouseButtonDown)
            timeDown = 0f;
        
        #elif UNITY_ANDROID
        //use latest touch point, don't use current touch points
        int touchId = Input.touches.Length - 1;

        if (touchId >= 0)
        {
            Touch touch = Input.touches[touchId];
            Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            
            // update laser point for where finger is
            laserPoint.MoveLaser(pos);

            if (touch.phase == TouchPhase.Began)
                timeDown = 0f;

            if (touch.phase != TouchPhase.Ended)
                timeDown += Time.deltaTime;
            else if (timeDown < TapThreshold)
            {
                //fire snake
                player.FireSnake(Vector3.Normalize(pos - playerTransform.position));
            }
        }

        #endif
    }
}
