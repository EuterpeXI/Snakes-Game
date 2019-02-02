using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeController : MonoBehaviour 
{

	public float speedToMouse = 1.0f;
	public float speedToRandom = 0.2f;
	public Transform laserPoint;
	
	Rigidbody2D body;
	
	// Direction snake will move when cant see laser
	Vector2 otherDirection = Vector2.zero;
	
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate()
	{
		Vector2 laserDirection = laserPoint.position - transform.position;
		
		// PERF: laserDirection.magnitude 
		RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection.normalized, laserDirection.magnitude);
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
	
	void Move(Vector2 direction, float speed)
	{
		// TODO: Interpolate current velocity with new velocity or use forces 
		body.MovePosition((Vector2)transform.position + ((direction).normalized * speed * Time.deltaTime));	
	}
	
}
