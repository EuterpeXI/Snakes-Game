using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 lastMoveDirection;
    private float timeLastFired;

    public float speed = 3.0f;
    public GameObject snakePrefab;

    public Vector2 initSnakeVelocity;
    public float firingCooldown;

    void Start()
    {
        timeLastFired = Time.time;
        body = GetComponent<Rigidbody2D>();
        lastMoveDirection = new Vector2(1.0f, 0.0f);
    }

    void FixedUpdate()
    {
        /*
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        body.MovePosition((Vector2) transform.position + (moveDirection.normalized * speed * Time.deltaTime));
        if (moveDirection != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            FireSnake(lastMoveDirection);
        }
        */
    }

    public void FireSnakeAtLaserPointer()
    {
        if (LaserPointController.position != null)
        {
            FireSnake(transform.position - LaserPointController.position);
        }
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