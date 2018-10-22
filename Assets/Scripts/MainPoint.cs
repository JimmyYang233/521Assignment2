using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MainPoint : MonoBehaviour {
	
	private Vector2 current;
	private Vector2 previous;
	private float groundLevel = -3.5f;
	private Vector2[] pointDistances;

	public float bounce;
	public Vector2 acceleration;
	public GameObject[] neighborPoints;
	public bool isMainPoint;
	public float leftWallPosition;
	
	
	// Use this for initialization
	void Start ()
	{
		current = transform.position;
		previous = transform.position;
		pointDistances = new Vector2[neighborPoints.Length];
		for(int i = 0; i<neighborPoints.Length; i++)
		{
			pointDistances[i] = neighborPoints[i].transform.position - transform.position;
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
		Stick();

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
		for (int i = 0; i < neighborPoints.Length; i++)
		{
			float dx = neighborPoints[i].transform.position.x - transform.position.x;
			float dy = neighborPoints[i].transform.position.y - transform.position.y;
			float distance = (float)Math.Sqrt(dx * dx + dy * dy);
			//float differenceX = pointDistance.x - dx;
			//float differenceY = pointDistance.y - dy;
			float pointLength = (float) Math.Sqrt(pointDistances[i].x * pointDistances[i].x + pointDistances[i].y * pointDistances[i].y);
			float difference = pointLength - distance;
			float percent = difference / distance / 2;
			//float offsetX = differenceX;
			//float offsetY = differenceY;
			float offsetX = dx * percent;
			float offsetY = dy * percent;
		
			transform.position = current = current - new Vector2(offsetX/2.0f, offsetY/2.0f);
			neighborPoints[i].GetComponent<Point>().addPosition(new Vector2(offsetX/2.0f, offsetY/2.0f));
		}
		

	}

	public void addPosition(Vector2 position)
	{
		transform.position = current = current + position;
	}
}
