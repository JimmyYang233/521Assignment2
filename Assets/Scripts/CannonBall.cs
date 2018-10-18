using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private float groundLevel = -1.2f;

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
	}
}
