using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGravity : MonoBehaviour {
    public Vector3 grav;
    public float magnitude;
	// Use this for initialization
	void Awake () {
        magnitude = 9.81f;
        grav = Vector3.down*magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().AddForce(grav, ForceMode.Acceleration);
    }
}
