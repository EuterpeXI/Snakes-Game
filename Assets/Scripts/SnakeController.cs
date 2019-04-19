using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TopDownMovementController))]
public class SnakeController : MonoBehaviour
{
    [FormerlySerializedAs("speedToMouse")] public float speedToLaserPointer = 1.0f;
    public float speedToRandom = 0.2f;

    private TopDownMovementController movement;

    // Direction snake will move when cant see laser
    private Vector2 otherDirection = Vector2.zero;

    void Start()
    {
        movement = GetComponent<TopDownMovementController>();
    }

    void FixedUpdate()
    {
        Vector2 laserDirection = LaserPointController.position - transform.position;
        // PERF: laserDirection.magnitude
        float laserMagnitude = laserDirection.magnitude;
        laserDirection = laserDirection.normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection, laserMagnitude);
        if (hit.collider == null)
        {
            movement.Move(laserDirection * speedToLaserPointer);
            otherDirection = Vector2.zero;
        }
        else
        {
            // if this is the first frame snake stops seeing laser
            if (otherDirection == Vector2.zero)
            {
                otherDirection = Random.insideUnitCircle;
            }

            movement.Move(otherDirection * speedToRandom);
        }
    }
}