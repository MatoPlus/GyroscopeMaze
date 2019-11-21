using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastSpin : MonoBehaviour {
    // Update is called once per frame
    Vector3 rotation = new Vector3(5, 10, 5);
	void FixedUpdate () {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles + rotation), .5f);
	}
}
