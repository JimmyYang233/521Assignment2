using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = System.Random;

public class MountainGenerator : MonoBehaviour
{
	private float minX = -3f;
	private float maxX = 3f;
	private float minY = 0f;
	private float maxY = 20f;
	
	public static float mountainHeight;
	

	public GameObject dirt;
	public GameObject grass;

	public float dirtSize;

	private PerlinNoise noise;

	// Use this for initialization
	void Start ()
	{
		regenerate();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.R))
		{
			regenerate();
		}
	}

	private void regenerate()
	{
		mountainHeight = 0;
		int childNum = transform.childCount;
		for (int i = 0; i < childNum; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		noise = new PerlinNoise(UnityEngine.Random.Range(50,3000), dirtSize);
		float currentX = minX;
		while (currentX < maxX)
		{
			
			int columHeight = noise.getNoise((int) ((currentX - minX) / dirtSize), (int) ((maxY - minY) / dirtSize));
			if ((float)(columHeight) * dirtSize>=mountainHeight)
			{
				mountainHeight = (float) (columHeight) * dirtSize;
			}
			//Debug.Log(columHeight);
			float currentY = minY;
			while (currentY < minY+(float)(columHeight)*dirtSize)
			{
				GameObject currentDirt = Instantiate(dirt, this.transform);
				
				currentDirt.transform.localPosition = new Vector3(currentX, currentY, 1);
				currentY += dirtSize;
			}

			GameObject currentGrass = Instantiate(grass, this.transform);
			currentGrass.transform.localPosition = new Vector3(currentX, currentY, 1);
			currentX += dirtSize;
		}
		//Debug.Log(mountainHeight);
	}
}
