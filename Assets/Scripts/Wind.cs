using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

	public float minW;
	public float maxW;
	private Vector2 velocity;
	private float windForce;
	
	
	// Use this for initialization
	void Start ()
	{
		velocity = new Vector2();
		windForce = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		StartCoroutine(waitAndChange(0.5f));
		velocity += new Vector2(windForce, 0)*Time.deltaTime;
		Vector2 deltaPosition = velocity * Time.deltaTime;
		Vector3 move = Vector3.right * deltaPosition.x;
		transform.position = transform.position + move;
		GameObject[] cannonBalls = GameObject.FindGameObjectsWithTag("CannonBall");

		foreach (GameObject cannonBall in cannonBalls)
		{
			cannonBall.GetComponent<Rigidbody2D>().velocity += deltaPosition;
		}
	}

	private IEnumerator waitAndChange(float time)
	{
		yield return new WaitForSeconds(time);
		newWind();
	}

	private void newWind()
	{
		windForce = Random.Range(minW, maxW);
		//Debug.Log(windForce);
	}
}
