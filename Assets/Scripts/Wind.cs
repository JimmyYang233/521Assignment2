using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

	public float minW;
	public float maxW;
	private Vector2 velocity;
	private float windForce;
	private float time;
	private float period;
	
	
	// Use this for initialization
	void Start ()
	{
		velocity = new Vector2();
		windForce = 0;
		time = 0f;
		period = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		time += Time.deltaTime;
		if (time > period)
		{
			time = 0f;
			newWind();
		}
		velocity += new Vector2(windForce, 0)*Time.deltaTime;
		Vector2 deltaPosition = velocity * Time.deltaTime;
		Vector3 move = Vector3.right * deltaPosition.x;
		transform.position = transform.position + move;
		GameObject[] cannonBalls = GameObject.FindGameObjectsWithTag("CannonBall");
		
		foreach (GameObject cannonBall in cannonBalls)
		{
			if (cannonBall.transform.position.y >= MountainGenerator.mountainHeight)
			{
				cannonBall.GetComponent<Rigidbody2D>().velocity += deltaPosition;
			}
			
		}

		GameObject[] turkeyPoints = GameObject.FindGameObjectsWithTag("Point");

		foreach (GameObject turkeyPoint in turkeyPoints)
		{
			if (turkeyPoint.transform.position.y >= MountainGenerator.mountainHeight)
			{
				turkeyPoint.GetComponent<Point>().Accelerate(windForce*Time.deltaTime,0);
			}
		}
		
	}

	private void newWind()
	{
		windForce = Random.Range(minW, maxW);
		//Debug.Log(windForce);
	}
}
