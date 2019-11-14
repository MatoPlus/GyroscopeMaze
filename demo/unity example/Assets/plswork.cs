﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class plswork : MonoBehaviour
{
    // Start is called before the first frame update
    SerialPort sp = new SerialPort("COM6", 9600);
    char[] teapotPacket = new char[14];  // InvenSense Teapot packet
    int serialCount = 0;                 // current packet byte position
    int synced = 0;
    int interval = 0;
    float[] q = new float[4];
    Quaternion quat = new Quaternion(1, 0, 0, 0);
    void Start()
    {
        sp.Open();
    }

    void Update()
    {
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
                    //quat = new Quaternion(q[0], q[1], q[2], q[3]);
                    transform.rotation = new Quaternion(q[2], q[0], -q[1], q[3]);
                    //print("q:\t" + Math.Round(q[0] * 100.0f) / 100.0f + "\t" + Math.Round(q[1] * 100.0f) / 100.0f + "\t" + Math.Round(q[2] * 100.0f) / 100.0f + "\t" + Math.Round(q[3] * 100.0f) / 100.0f);

                }
            }
        }



        /*
            try
            {
                print(sp.BytesToRead);
                sp.rea
            }
    #pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch
    #pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
            {

            }*/

    }

}