using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
	private Vector2 current;
	private Vector2 previous;
	private float groundLevel = -3.5f;
	private Vector2 pointDistance;

	public float bounce;
	public Vector2 acceleration;
	public GameObject neighborPoint;
	public bool isMainPoint;
	public float leftWallPosition;
	
	
	// Use this for initialization
	void Start ()
	{
		current = transform.position;
		previous = transform.position;
		if (neighborPoint != null)
		{
			pointDistance = neighborPoint.transform.position - transform.position;
		}
		else
		{
			pointDistance = new Vector2(0,0);
		}
		Accelerate();
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector2 velocity = current - previous;
		Vector2 gravity = Physics2D.gravity * Time.deltaTime * Time.deltaTime;
		previous = current;
		transform.position = current = current + velocity + gravity;
		Constraints();
		if (neighborPoint != null)
		{
			Stick();
		}

		if (isMainPoint && transform.position.x<=leftWallPosition)
		{
			Debug.Log("Was here");
			acceleration = new Vector3(1,0);
			Accelerate();
		}
		
		
	}

	private void Constraints()
	{
		Vector2 velocity = current - previous;
		if (transform.position.y < groundLevel)
		{
			current = new Vector2(transform.position.x, groundLevel);
			previous = new Vector2(previous.x, velocity.y*bounce+current.y);
		}
	}

	private void Accelerate()
	{
		Vector2 velocity = current - previous;
		previous = current;
		current = current + velocity + acceleration*Time.deltaTime;
	}

	private void Stick()
	{
		float dx = neighborPoint.transform.position.x - transform.position.x;
		float dy = neighborPoint.transform.position.y - transform.position.y;
		float differenceX = pointDistance.x - dx;
		float differenceY = pointDistance.y - dy;

		float offsetX = differenceX;
		float offsetY = differenceY;

		transform.position = current = current - new Vector2(offsetX, offsetY);

	}
}
