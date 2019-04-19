using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public float speed = 0.000000000000004f;
    public GameObject snakePrefab;
    public Vector2 initSnakeVelocity;
    public float firingCooldown;
    public float newSpeed = 3.0f;

    private Rigidbody2D rb;
    private float horizonalMovement;
    private float verticalMovement;
    private float timeLastFired;
    private Vector2 lastMoveDirection;
    private Quaternion targetRotation;
    private Vector2 newPosition;
    private Vector2 movement;

    void Start()
    {
        timeLastFired = Time.time;
        rb = GetComponent<Rigidbody2D>();
        lastMoveDirection = new Vector2(1.0f, 0.0f);
    }

    void Update()
    {
        moveCharacter();
    }

    private void moveCharacter()
    {
        horizonalMovement = UltimateJoystick.GetHorizontalAxis("Movement");
        verticalMovement = UltimateJoystick.GetVerticalAxis("Movement");

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);

        // If they are not equal to 0, the character is moving
        if (horizonalMovement != 0.0 && verticalMovement != 0.0){
            movement = new Vector2(horizonalMovement, verticalMovement);
            newPosition = movement * Time.deltaTime * speed;
            //targetRotation = Quaternion.LookRotation(movement);
        }
        else{
            newPosition = Vector2.zero;
        }

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

        rb.MovePosition(rb.position + newPosition);
    }

    public void FireSnake(Vector2 direction)
    {
        if (Time.time - firingCooldown >= timeLastFired)
        {
            // TODO: multiply direction normalized to properly exit projectile from character
            GameObject a = Instantiate(snakePrefab, (Vector2)transform.position + (direction.normalized),
                Quaternion.identity);

            a.GetComponent<Rigidbody2D>().velocity = initSnakeVelocity * direction.normalized;

            timeLastFired = Time.time;
        }
    }

}
