using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TopDownMovementController))]
public class SnakeController : MonoBehaviour
{
    [FormerlySerializedAs("speedToMouse")] public float speedToLaserPointer = 1.0f;
    public float speedToRandom = 0.2f;
    
    public Sprite SnakeSprite;
    public Sprite BoxSnakeSprite;

    public Collider2D SnakeCollider;
    public Collider2D BoxSnakeCollider;
    
    public bool IsBoxSnake;

    private TopDownMovementController movement;
    private SpriteRenderer spriteRenderer;

    // Direction snake will move when cant see laser
    private Vector2 otherDirection = Vector2.zero;

    void Start()
    {
        movement = GetComponent<TopDownMovementController>();
        spriteRenderer =  this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //update layer (9 is snkae, 10 is boxsnake)
        this.gameObject.layer = IsBoxSnake ? 9 : 10;
        
        // update sprite
        spriteRenderer.sprite = IsBoxSnake ? BoxSnakeSprite : SnakeSprite;
        
        //update colliders
        SnakeCollider.enabled = !IsBoxSnake;
        BoxSnakeCollider.enabled = IsBoxSnake;
    }

    void FixedUpdate()
    {
        Vector2 laserDirection = LaserPointController.position - transform.position;
        // PERF: laserDirection.magnitude
        float laserMagnitude = laserDirection.magnitude;
        laserDirection = laserDirection.normalized;
        
        //ignroe layers: snake = 9, player = 8, box = 11
        //ignore box unitl it becomes BOX SNAKE!! ( so we don't ignore layer 10)
        var raycastMask = ~((1 << 9) | (1 << 8) | (1 << 11) | (1 << 2));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection, laserMagnitude, raycastMask);
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

    private void OnTriggerEnter(Collider other)
    {
        // 11 is Box layer
        if (other.gameObject.layer == 11)
        {
            this.IsBoxSnake = true;
            Destroy(other.gameObject);
        }
    }
}