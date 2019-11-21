using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO.Ports;
using System.Threading;

public class Tilt : MonoBehaviour
{
    // Public Variables (for changing in insepctor)
    private static float magnitude = -9.81f;
    private static GameObject gravityObject;
    public static Vector3 Gravity
    {
        get
        {
            if (gravityObject == null)
            {
                return Vector3.up;
            }
            return gravityObject.transform.up * magnitude;
        }
    }

    //public GameObject gravPointer;

    // Private Variables
    char[] packet = new char[14];  // InvenSense packet
    int serialCount = 0;                 // current packet byte position
    int synced = 0;
    float[] q = new float[4];

    void Start()
    {
        // /dev/cu.wchusbserial14xx0   OR    COMx
        if (Director.useGyro && !Director.sp.IsOpen)
        {
            SetupController();
        }
        if (gravityObject != null)
        {
            throw new Exception();
        }
        gravityObject = gameObject;
        //gravPointer = GameObject.Find("GravPointer");
    }

    void FixedUpdate()
    {
        // if useGyro is false use the 
        if (!Director.useGyro)
        {
            if (Input.GetAxis("Horizontal") > .2)
            {
                this.transform.rotation *= Quaternion.Euler(0, 0, 0.1f* Director.gyroSensitivity);
            }
            if (Input.GetAxis("Horizontal") < -.2)
            {
                this.transform.rotation *= Quaternion.Euler(0, 0, -0.1f* Director.gyroSensitivity);
            }
            if (Input.GetAxis("Vertical") > .2)
            {
                this.transform.rotation *= Quaternion.Euler(0.1f* Director.gyroSensitivity, 0, 0);
            }
            if (Input.GetAxis("Vertical") < -.2)
            {
                this.transform.rotation *= Quaternion.Euler(-0.1f* Director.gyroSensitivity, 0, 0);
            }
        }
        else
        {

            if (Director.sp == null)
            {
                SetupController();
            }

            while (Director.sp.BytesToRead > 0)
            {
                int ch = Director.sp.ReadByte();

                if (synced == 0 && ch != '$') return;   // initial synchronization - also used to resync/realign if needed
                synced = 1;
                if ((serialCount == 1 && ch != 2)
                    || (serialCount == 12 && ch != '\r')
                    || (serialCount == 13 && ch != '\n'))
                {
                    serialCount = 0;
                    synced = 0;
                    return;
                }

                if (serialCount > 0 || ch == '$')
                {
                    packet[serialCount++] = (char)ch;
                    if (serialCount == 14)
                    {
                        serialCount = 0; // restart packet byte position

                        // get quaternion from data packet
                        q[0] = ((packet[2] << 8) | packet[3]) / 16384.0f;
                        q[1] = ((packet[4] << 8) | packet[5]) / 16384.0f;
                        q[2] = ((packet[6] << 8) | packet[7]) / 16384.0f;
                        q[3] = ((packet[8] << 8) | packet[9]) / 16384.0f;
                        for (int i = 0; i < 4; i++) if (q[i] >= 2) q[i] = -4 + q[i];

                        // set our toxilibs quaternion to new data
                        //transform.rotation = new Quaternion(q[2], q[0], -q[1], q[3]);
                        transform.rotation = new Quaternion(q[2], q[0], -q[1], q[3]);


                    }
                }
            }
        }
        //GameObject.Find("Ball(Clone)").GetComponent<Rigidbody>().AddForce(transform.up * magnitude, ForceMode.Acceleration);
        //gravPointer.transform.position = transform.up*magnitude;
        //mCamera.transform.position = 10*(new Vector3(-grav.x, -grav.y, -grav.z));
        //mCamera.transform.LookAt(new Vector3(0, 0, 0));
    }


    void SetupController()
    {
        // sp = new SerialPort("/dev/cu.wchusbserial14110", 9600);
        // sp = new SerialPort("COM6", 9600);
        // sp.Open();

        //Auto detect implementation.
        string[] ports = SerialPort.GetPortNames();
        foreach (string p in ports)
        {
            try
            {
                print("Attempted to connect to: " + p);
                Director.sp = new SerialPort(p, 9600);
                Director.sp.Open();
                // Sucessfully reads input from sp, meaning the port is valid.
                if (Director.sp.BytesToRead != 0)
                {
                    break;
                }
            }
            catch (InvalidOperationException e)
            {
                // Port in use  
                print(e);
                continue;
            }
            catch (System.IO.IOException e)
            {
                // Port can't be opened
                print(e);
                continue;
            }
        }
    }
}