using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 lastMoveDirection;
    private float timeLastFired;

    public float speed = 3.0f;
    public GameObject snakePrefab;
    public ProjectileSpawner snakeCannon;
    private Rigidbody2D body;
    private Vector2 lastMoveDirection;
    public Vector2 initSnakeVelocity;
    public float firingCooldown;

    public MobileJoystickPlayerController playerController;

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
        snakeCannon.Spawn();
    }
}