using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TurkeyGenerator : MonoBehaviour
{

	public GameObject turkey;

	public float leftWallPosition;

	public float leftMountainPosition;

	public float turkeyNumber;
	// Use this for initialization
	void Start () {/**
		for (int i = 0; i < turkeyNumber; i++)
		{
			generateTurkey();
		}
		**/
		//generateTurkey();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generateTurkey()
	{
		float random = UnityEngine.Random.Range(leftWallPosition, leftMountainPosition);
		GameObject newTurkey = Instantiate(turkey,transform);
		newTurkey.transform.position = new Vector3(random, -2.82f, 0);
		//Debug.Log(newTurkey.transform.position);
		//Debug.Log(newTurkey.transform.GetChild(0).position);
		//newTurkey.transform.position = new Vector3(random, -2.82f, 0);
	}
}
