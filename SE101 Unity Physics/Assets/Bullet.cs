using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector3 MoveDirection { get; set; }
	
	void FixedUpdate () {
        transform.position += MoveDirection;
	}
}
