using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
	private Rigidbody2D _rb2D;
	private readonly float groundLevel = -3.5f;
	private Vector2 _gravityVelocity;
	private bool _alreadyTouched;
	private float period = 0f;
	private float time = 0f;

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
		//Debug.Log(_rb2D.velocity.x);
		if (TouchedGround())	
		{
			Destroy(gameObject);
		}

		time += Time.deltaTime;
		if (time >= period)
		{
			time = 0f;
			period = 0f;
			TouchedMountain();
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

	private void TouchedMountain()
	{
		GameObject[] pixels = GameObject.FindGameObjectsWithTag("Grass");
		//Debug.Log(pixels.Length);
		foreach (GameObject pixel in pixels)
		{
			//Debug.Log("Was here");
			if (pixel.transform.position.x>=this.transform.position.x-0.15f&&pixel.transform.position.x<=this.transform.position.x+0.15f)
			{
				//Debug.Log(this.transform.position.y + ", " + pixel.transform.position.y);
				if(this.transform.position.y<=pixel.transform.position.y + 0.15f)
				{
					period = 0.03f;
					Vector2 newVelocity = new Vector2(-_rb2D.velocity.x * 0.7f, -_rb2D.velocity.y * 0.7f);
					AddVelocity(newVelocity);
				}
				//if (this.transform.position.y <= pixel.transform.position.y - 0.1f)
				//{
					//Destroy(gameObject);
				//}
			}

			if (this.transform.position.x <= pixel.transform.position.x + 0.08f &&
			    this.transform.position.x >= pixel.transform.position.x - 0.08f)
			{
				if (this.transform.position.y <= pixel.transform.position.y)
				{
					Destroy(gameObject);
				}
			}
		}
	}

	public void AddVelocity(Vector2 velocity)
	{
		_rb2D.velocity = velocity;
	}
}
