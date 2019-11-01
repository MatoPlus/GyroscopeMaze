using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class Tilt : MonoBehaviour
{
    // Public Variables (for changing in insepctor)
    public bool useGyro = false;

    // Private Variables
    SerialPort sp;
    char[] teapotPacket = new char[14];  // InvenSense Teapot packet
    int serialCount = 0;                 // current packet byte position
    int synced = 0;
    float[] q = new float[4];

    void Start()
    {
        // /dev/cu.wchusbserial14xx0   OR    COMx
        if (useGyro)
        {
            SetUpGyro();
        }
    }

    void FixedUpdate()
    {
        // if useGyro is false use the 
        if (!useGyro)
        {
            if (Input.GetAxis("Horizontal") > .2)
            {
                transform.Rotate(0, 0, (float)0.5);
            }
            if (Input.GetAxis("Horizontal") < -.2)
            {
                transform.Rotate(0, 0, (float)-0.5);
            }
            if (Input.GetAxis("Vertical") > .2)
            {
                transform.Rotate((float)0.5, 0, 0);
            }
            if (Input.GetAxis("Vertical") < -.2)
            {
                transform.Rotate((float)-0.5, 0, 0);
            }
        }
        else
        {

            if (sp == null)
            {
                SetUpGyro();
            }

            while (sp.BytesToRead > 0)
            {
                int ch = sp.ReadByte();

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
                    teapotPacket[serialCount++] = (char)ch;
                    if (serialCount == 14)
                    {
                        serialCount = 0; // restart packet byte position

                        // get quaternion from data packet
                        q[0] = ((teapotPacket[2] << 8) | teapotPacket[3]) / 16384.0f;
                        q[1] = ((teapotPacket[4] << 8) | teapotPacket[5]) / 16384.0f;
                        q[2] = ((teapotPacket[6] << 8) | teapotPacket[7]) / 16384.0f;
                        q[3] = ((teapotPacket[8] << 8) | teapotPacket[9]) / 16384.0f;
                        for (int i = 0; i < 4; i++) if (q[i] >= 2) q[i] = -4 + q[i];

                        // set our toxilibs quaternion to new data
                        transform.rotation = new Quaternion(q[2], q[0], -q[1], q[3]);
                    }
                }
            }
        }
    }


    void SetUpGyro()
    {
        // sp = new SerialPort("/dev/cu.wchusbserial14430", 9600);
        sp = new SerialPort("COM6", 9600);
        sp.Open();
    }
}