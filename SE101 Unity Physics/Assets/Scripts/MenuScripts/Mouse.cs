using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class Mouse : MonoBehaviour
{
    // Public Variables (for changing in insepctor)
    public float magnitude;
    public GameObject mouse;


    // Private Variables
    char[] packet = new char[14];  // InvenSense packets
    int serialCount = 0;                 // current packet byte position
    int synced = 0;
    float[] q = new float[4];

    void Start()
    {
        // Set up Gyroscope if required
        if (Director.useGyro && !Director.sp.IsOpen)
        {
            Director.SetupController();
        }
    }

    void Update()
    {
        // if useGyro is false use the 
        if (!Director.useGyro)
        {
            if (Input.GetAxis("Horizontal") > .2)
            {
                mouse.transform.position = new Vector3(Math.Min(Screen.width, mouse.transform.position.x + Director.gyroSensitivity*1000f/Screen.width), mouse.transform.position.y, mouse.transform.position.z);
                //this.transform.rotation *= Quaternion.Euler(0, 0, -0.1f* Director.gyroSensitivity);
            }
            if (Input.GetAxis("Horizontal") < -.2)
            {
                mouse.transform.position = new Vector3(Math.Max(0, mouse.transform.position.x - Director.gyroSensitivity * 1000f / Screen.width), mouse.transform.position.y, mouse.transform.position.z);
                //this.transform.rotation *= Quaternion.Euler(0, 0, 0.1f* Director.gyroSensitivity);
            }
            if (Input.GetAxis("Vertical") > .2)
            {
                mouse.transform.position = new Vector3(mouse.transform.position.x, Math.Min(Screen.height, mouse.transform.position.y + Director.gyroSensitivity * 1000f / Screen.width), mouse.transform.position.z);
                //this.transform.rotation *= Quaternion.Euler(0.1f* Director.gyroSensitivity, 0, 0);
            }
            if (Input.GetAxis("Vertical") < -.2)
            {
                mouse.transform.position = new Vector3(mouse.transform.position.x, Math.Max(0, mouse.transform.position.y - Director.gyroSensitivity * 1000f / Screen.width), mouse.transform.position.z);
                //this.transform.rotation *= Quaternion.Euler(-0.1f* Director.gyroSensitivity, 0, 0);
            }
        }
        else
        {
            // Set up the controller if not instantiated
            if (Director.sp == null)
            {
                Director.SetupController();
            }

            // Button press delay
            Director.buttonDelay -= Time.deltaTime;

            // Read from serial port that the controller is connected to
            while (Director.sp.BytesToRead > 0)
            {
                int ch = Director.sp.ReadByte();
               
                // # indicates that arduino has received button press
                if (serialCount == 0 && ch == '#')
                {
                    if (Director.buttonDelay <= 0)
                    {
                        Director.buttonDelay = Director.buttonDelayMax;
                        Director.SetPressed(true);
                    }
                    else
                    {
                        Director.SetPressed(false);
                    }
                }
                // @ indicates that arduino has speifically has no button press
                else if (serialCount == 0 && ch == '@')
                {
                    Director.buttonDelay = 0;
                    Director.SetPressed(false);
                }


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
                        this.transform.rotation = new Quaternion(q[2], q[0], -q[1], q[3]);


                    }
                }
            }
        }
        
        if(Director.useGyro)mouse.transform.position = new Vector3(transform.up.x * Director.gyroSensitivity*100 + Screen.width/2, transform.up.z * Director.gyroSensitivity * 100 + Screen.height/2, 0);
    }



}