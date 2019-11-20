using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
	void FixedUpdate () {
        GetComponent<Rigidbody>().AddForce(Tilt.Gravity, ForceMode.Acceleration);
    }
}
