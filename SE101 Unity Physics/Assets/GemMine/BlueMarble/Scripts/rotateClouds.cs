using UnityEngine;
using System.Collections;

public class rotateClouds : MonoBehaviour {

	public float speedY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,speedY * Time.deltaTime,0));
	}
}
