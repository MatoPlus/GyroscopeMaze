using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LethalObject : MonoBehaviour {

    public UnityEvent OnCollisionWithBall = new UnityEvent();

    void Start()
    {
        // OnCollisionWithBall = new UnityEvent();
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            print("collided");
            OnCollisionWithBall.Invoke();
        }
    }
}
