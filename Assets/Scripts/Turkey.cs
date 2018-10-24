using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turkey : MonoBehaviour
{

	private GameObject mainPoint;

	private GameObject[] feetPoints;

	private List<GameObject> points;
	
	private float time = 0.0f;

	private float period = 1f;

	private float jumpHeight;

	private bool alreadyTurned = false;

	public float groundLevel;

	public float leftWallPosition;

	public float leftMountainPosition;

	public float rightMountainPosition;


	// Use this for initialization
	void Start ()
	{
		mainPoint = transform.GetChild(0).gameObject;
		feetPoints = new GameObject[2];
		feetPoints[0] = transform.GetChild(1).gameObject;
		feetPoints[1] = transform.GetChild(2).gameObject;
		points = new List<GameObject>();
		GameObject[] allPoints = GameObject.FindGameObjectsWithTag("Point");
		foreach (GameObject point in allPoints)
		{
			if (point.transform.parent.Equals(this.transform))
			{
				points.Add(point);	
			}
		}
		//	Debug.Log(points.Count);

		StartCoroutine(waitAndStartWalk(1f));
	}

	void FixedUpdate()
	{
		checkInput();
		SetGravity();
		turkeyWalk();
		time += Time.deltaTime;
		if (time > period)
		{
			time = 0f;
			turkeyJump();
		}

		if (mainPoint.transform.position.x >= rightMountainPosition)
		{
			GetComponentInParent<TurkeyGenerator>().generateTurkey();
			Destroy(gameObject);
		}
		//touchedMountain();
		jumpHeight = MountainGenerator.mountainHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void SetGravity()
	{
		Vector2 gravity = Physics2D.gravity * Time.deltaTime*3;
		foreach(GameObject foot in feetPoints)
		{
			foot.GetComponent<Point>().Accelerate(gravity);
		}
	}

	private void turkeyWalk()
	{
		if ((mainPoint.transform.position.x<leftWallPosition))
		{
			mainPoint.transform.position = new Vector2(leftWallPosition, mainPoint.transform.position.y);
			foreach(GameObject point in points)
			{
				point.GetComponent<Point>().Accelerate(1,0);
				alreadyTurned = false;
			}	
		}
		
		else if ((mainPoint.transform.position.x>=leftMountainPosition)&& isGrounded()&&!alreadyTurned)
		{
			
			foreach(GameObject point in points)
			{
				point.GetComponent<Point>().Accelerate(-1,0);
				alreadyTurned = true;
			}	
		}
		
	}

	private void checkInput()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			foreach (GameObject point in points)
			{
				point.GetComponent<Point>().Accelerate(new Vector2(-1, 0));
			}
			
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			foreach (GameObject point in points)
			{
				point.GetComponent<Point>().Accelerate(new Vector2(1, 0));
			}
		}
		else if (Input.GetKeyDown(KeyCode.W)&&isGrounded())
		{
			foreach (GameObject point in points)
			{
				point.GetComponent<Point>().Accelerate(new Vector2(0, jumpHeight));
			}
		}
	}
	
	private void turkeyJump()
	{
		float random = UnityEngine.Random.Range(-1f, 10f);
		if (random <= 0f&&isGrounded())
		{
			foreach (GameObject point in points)
			{
				point.GetComponent<Point>().Accelerate(new Vector2(0, jumpHeight));
			}	
		}
		
	}

	private bool isGrounded()
	{
		foreach (GameObject point in points)
		{
			if (point.transform.position.y <= point.GetComponent<Point>().getGroundLevel()+0.2f	)
			{
				return true;
			}
		}

		return false;
	}

	private IEnumerator waitAndStartWalk(float time)
	{
		yield return new WaitForSeconds(time);
		foreach(GameObject point in points)
		{
			point.GetComponent<Point>().Accelerate(-0.5f, 0);
		}
	}

	private IEnumerator waitAndCheckJump(float time)
	{
		yield return new WaitForSeconds(time);
		turkeyJump();
	}

	private void touchedMountain()
	{
		GameObject[] pixels = GameObject.FindGameObjectsWithTag("Grass");
		//Debug.Log(pixels.Length);
		foreach (GameObject pixel in pixels)
		{
			foreach (GameObject point in points)
			{
				if (pixel.transform.position.x>=point.transform.position.x-0.15f&&pixel.transform.position.x<=point.transform.position.x+0.15f)
				{
					if(pixel.transform.position.y>=point.transform.position.y-0.1f)
					{
						Debug.Log("Touch the mountain!");
						//point.GetComponent<Point>().setGroundLevel(pixel.transform.position.y);
					}
				}
			}
		}
		
	}
}
