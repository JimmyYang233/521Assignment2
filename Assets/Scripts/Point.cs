using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Point : MonoBehaviour
{
	private Vector2 current;
	private Vector2 previous;
	private float groundLevel = -3.5f;
	private Vector2[] pointDistances;
	
	
	public GameObject[] neighborPoints;
	public float bounce;
	public float leftMountainPosition;
	
	// Use this for initialization
	void Start ()
	{
		current = transform.position;
		previous = transform.position;
		//neighborPoints = GameObject.FindGameObjectsWithTag("Point");
		pointDistances = new Vector2[neighborPoints.Length];
		for(int i = 0; i<neighborPoints.Length; i++)
		{
			pointDistances[i] = neighborPoints[i].transform.position - transform.position;
		}	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector2 velocity = current - previous;
		previous = current;
		transform.position = current = current + velocity;
		Constraints();
		Stick();
		checkCannonBall();
	}

	private void Constraints()
	{
		Vector2 velocity = current - previous;
		if (transform.position.y <= getGroundLevel()&&transform.position.y >= getGroundLevel()-0.3f)
		{
			current = new Vector2(transform.position.x, getGroundLevel());
			previous = new Vector2(previous.x, velocity.y*bounce+current.y);
		}

		if (transform.position.y <= getGroundLevel() && transform.position.x > leftMountainPosition)
		{
			current = new Vector2(transform.position.x, transform.position.y);
			previous = new Vector2(velocity.x*bounce+current.x, previous.y);
		}
	}

	public void Accelerate(Vector2 acceleration)
	{
		Vector2 velocity = current - previous;
		previous = current;
		current = current + velocity + acceleration*Time.deltaTime;
	}
	
	public void Accelerate(float X, float Y)
	{
		Vector2 velocity = current - previous;
		previous = current;
		Vector2 acceleration = new Vector2(X, Y);
		current = current + velocity + acceleration*Time.deltaTime;
	}

	private void Stick()
	{
		//Debug.Log(neighborPoints.Length + "");
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

	private void checkCannonBall()
	{
		GameObject[] cannonBalls = GameObject.FindGameObjectsWithTag("CannonBall");
		foreach (GameObject cannonBall in cannonBalls)
		{
			if (cannonBall.transform.position.x>=this.transform.position.x-0.1f&&cannonBall.transform.position.x<=this.transform.position.x+0.1f)
			{
				if(cannonBall.transform.position.y>=this.transform.position.y-0.1f&&cannonBall.transform.position.y<=this.transform.position.y+0.1f)
				{
					Debug.Log("Touch the Turkey!");
					Vector2 acceleration = cannonBall.GetComponent<Rigidbody2D>().velocity;
					Destroy(cannonBall);
					Accelerate(acceleration*(float)0.7);
				}
			}
		}
	}


	public float getGroundLevel()
	{
		GameObject[] pixels = GameObject.FindGameObjectsWithTag("Grass");
		//Debug.Log(pixels.Length);
		foreach (GameObject pixel in pixels)
		{
			if (pixel.transform.position.x>=this.transform.position.x-0.15f&&pixel.transform.position.x<=this.transform.position.x+0.15f)
			{
				return pixel.transform.position.y;
			}
		}

		return groundLevel;
	}
}
