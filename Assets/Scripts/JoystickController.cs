using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public float speed = 0.000000000000004f;

    private Rigidbody2D rb;
    private float horizonalMovement;
    private float verticalMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveCharacter();
    }

    private void moveCharacter()
    {
        horizonalMovement = UltimateJoystick.GetHorizontalAxis("Movement");
        verticalMovement = UltimateJoystick.GetVerticalAxis("Movement");

        Vector2 movement = new Vector2(horizonalMovement, verticalMovement);

        //Debug.Log("Position: " + rb.position);
        Vector2 newPosition = movement * Time.deltaTime * speed;

        //Debug.Log("NewPosition: " + newPosition);
        //Debug.Log("Movement: " + movement);
        rb.MovePosition(rb.position + newPosition);
    }
}
