using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTracking : MonoBehaviour {
    public Button attachedTo;
	// Use this for initialization
	void OnTriggerEnter2D()
    {
        attachedTo.active = true;
	}

    void OnTriggerExit2D()
    {
        attachedTo.active = false;
    }
}
