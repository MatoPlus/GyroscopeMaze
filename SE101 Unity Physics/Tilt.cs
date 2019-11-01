using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour
{

    // Use this for initialization
    void Start(){

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > .2)
        {
            transform.Rotate(0, 0, 90);
        }
        if (Input.GetAxis("Horizontal") < -.2)
        {
            transform.Rotate(0, 0, -1);


        }
    }
}
