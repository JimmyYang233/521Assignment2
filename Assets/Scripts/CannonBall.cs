using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private float groundLevel = -3.5f;
	private Vector2 velocity;

	public static float weight = 1f;
	

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		//Debug.Log(transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y<=groundLevel)
		{
			Destroy(gameObject);
		}
	}

	private void FixedUpdate()
	{
	    velocity += weight*Physics2D.gravity*Time.deltaTime;
		Vector2 deltaPosition = velocity * Time.deltaTime;
		rb2d.velocity = rb2d.velocity+deltaPosition;
	}
}
