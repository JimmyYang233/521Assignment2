using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Cannon : MonoBehaviour
{

	public GameObject cannonBall;

	public float speed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		facingMove();
		shoot();
	}

	void facingMove()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		
		Vector2 direction = new Vector2(mousePosition.x-transform.position.x, mousePosition.y - transform.position.y);
		//Debug.Log(direction.x + ", " + direction.y);

		if (direction.x <= 0f && direction.y >= 0f)
		{
			transform.right = -direction;
		}
	}

	void shoot()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject newCannonBall = GameObject.Instantiate(cannonBall);
			newCannonBall.GetComponent<CannonBall>().AddVelocity(-transform.right * speed);
		}
	}
}
