using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

public class Line : MonoBehaviour
{

	public GameObject Point1;

	public GameObject Point2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		LineRenderer _lineRenderer = GetComponent<LineRenderer>();
		Vector3[] vector3s = new Vector3[] {Point1.transform.position, Point2.transform.position};
		_lineRenderer.SetPositions(vector3s);
	}
}
