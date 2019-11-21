using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour {

    public UnityEvent OnCollisionWithBall = new UnityEvent();
    public UnityEvent OnCollisionWithTag = new UnityEvent();

    public string CheckTag { get; set; }

    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            OnCollisionWithBall.Invoke();
        }
        if (CheckTag != null && c.tag == CheckTag)
        {
            OnCollisionWithTag.Invoke();
        }
    }
}
