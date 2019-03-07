﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject snakePrefab;

    private Rigidbody2D body;
    private Vector2 lastMoveDirection;
    public Vector2 initSnakeVelocity;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lastMoveDirection = new Vector2(1.0f, 0.0f);
    }

    void FixedUpdate()
    {
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
    }

    public void FireSnakeAtLaserPointer()
    {
        if (LaserPointController.position != null)
        {
            FireSnake(transform.position - LaserPointController.position);
        }
    }

    void FireSnake(Vector2 direction)
    {
        // TODO: multiply direction normalized to properly exit projectile from character
        GameObject a = Instantiate(snakePrefab, (Vector2) transform.position + (direction.normalized),
            Quaternion.identity);
        // TODO: add force or set velocity of snake to fire
        
        a.GetComponent<Rigidbody2D>().velocity = initSnakeVelocity * direction.normalized;
    }
}