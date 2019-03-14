using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeController : MonoBehaviour
{
    public float speedToMouse = 1.0f;
    public float speedToRandom = 0.2f;
    public float AirTime; // the time spent in the 'air' when spawned 
    public bool inAir; // status of in air changes how the snake moves

    private Rigidbody2D body;

    // Direction snake will move when cant see laser
    private Vector2 otherDirection = Vector2.zero;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        inAir = true;
        StartCoroutine(TimeUtilites.WaitToDoAction(() =>
        {
            inAir = false;
            Debug.Log("No longer in air", this);
        }, AirTime));
    }

    void FixedUpdate()
    {
        Vector2 laserDirection = LaserPointController.position - transform.position;
        if (inAir)
        {
        }
        else
        {
            // PERF: laserDirection.magnitude
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, LaserPointController.position, laserDirection.magnitude);
            if (hit.collider == null)
            {
                Move(laserDirection, speedToMouse);
                otherDirection = Vector2.zero;
            }
            else
            {
                // if this is the first frame snake stops seeing laser
                if (otherDirection == Vector2.zero)
                {
                    otherDirection = Random.insideUnitCircle;
                }

                Move(otherDirection, speedToRandom);
            }
        }
    }

    void Move(Vector2 direction, float speed)
    {
        // TODO: Interpolate current velocity with new velocity or use forces 
        body.MovePosition((Vector2) transform.position + ((direction).normalized * speed * Time.deltaTime));
        body.MoveRotation(Vector2.Angle(transform.position, direction));
    }
}