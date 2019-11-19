using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTracking : MonoBehaviour {
    public Button attachedTo;
	// Use this for initialization
	void OnCollisionEnter2D()
    {
        attachedTo.active = true;
        Debug.Log("Entered!");
	}

    void OnCollisionExit2D()
    {
        attachedTo.active = false;
    }
}
