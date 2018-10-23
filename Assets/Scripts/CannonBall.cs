using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
	private Rigidbody2D _rb2D;
	private float groundLevel = -3.5f;
	private Vector2 _gravityVelocity;
	private bool _alreadyTouched;

	public static float gravityModifier = 1f;
	

	// Use this for initialization
	void Awake ()
	{
		_rb2D = GetComponent<Rigidbody2D>();
		_gravityVelocity = new Vector2();
		_alreadyTouched = false;
		//Debug.Log(transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(_rb2D.velocity.x);
		if (TouchedGround()||(_rb2D.velocity.x<=0.01&&_rb2D.velocity.x>=-0.01))
		{
			Destroy(gameObject);
		}
		else if (TouchedMountain())
		{
			/**
			if (_alreadyTouched)
			{
				Debug.Log("What?");
				Destroy(gameObject);
			}
			else
			{
			**/
			Vector2 newVelocity = new Vector2(-_rb2D.velocity.x * 0.7f, -_rb2D.velocity.y * 0.7f);
			AddVelocity(newVelocity);
			_alreadyTouched = true;
			//}

		}
	}

	private void FixedUpdate()
	{
	    _gravityVelocity += gravityModifier*Physics2D.gravity*Time.deltaTime;
		Vector2 deltaPosition = _gravityVelocity * Time.deltaTime;
		_rb2D.velocity = _rb2D.velocity+deltaPosition;
	}

	private bool TouchedGround()
	{
		return (transform.position.y <= groundLevel);
	}

	private bool TouchedMountain()
	{
		GameObject[] pixels = GameObject.FindGameObjectsWithTag("Grass");
		//Debug.Log(pixels.Length);
		foreach (GameObject pixel in pixels)
		{
			//Debug.Log("Was here");
			if (pixel.transform.position.x>=this.transform.position.x-0.1f&&pixel.transform.position.x<=this.transform.position.x+0.1f)
			{
				if(pixel.transform.position.y>=this.transform.position.y-0.1f)
				{
					//Debug.Log("Touch the mountain!");
					return true;
				}
			}
		}
		
		return false;
	}

	public void AddVelocity(Vector2 velocity)
	{
		_rb2D.velocity = velocity;
	}
}
