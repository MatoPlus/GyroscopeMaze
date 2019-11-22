using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text ScreenTimer;
    private UnityEvent timeOut;
    public UnityEvent TimeOut
    {
        get
        {
            if (timeOut == null)
            {
                timeOut = new UnityEvent();
            }
            return timeOut;
        }
        private set { timeOut = value; }
    }
    private UnityEvent timeUpdated;
    public UnityEvent TimeUpdated
    {
        get
        {
            if (timeUpdated == null)
            {
                timeUpdated = new UnityEvent();
            }
            return timeUpdated;
        }
        private set { timeUpdated = value; }
    }
    private bool initialized = false;
    public float WaitTime { get; private set; }
    private float timer;
    public float RemainingTime
    {
        get { return WaitTime - timer; }
    }
    private int lastTime;
    private bool timerActive;
    public string TimeLeft { get { return FormatTime(); } }
    private int width, height;

    private string FormatTime()
    {
        int remaining = (int)(WaitTime - timer);
        int minutes = remaining / 60;
        int seconds = remaining % 60;
        string minStr = (minutes < 10) ? "0" + minutes.ToString() : minutes.ToString();
        string secStr = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();

        return minStr + ":" + secStr;
    }

    void Awake()
    {
        ScreenTimer = gameObject.AddComponent<Text>();
        width = Screen.width;
        height = Screen.height;
    }

    void Update()
    {
        ScreenTimer.text = TimeLeft;
        // print(TimeLeft);
        if (!initialized) return;
        if (!timerActive) return;

        timer += Time.deltaTime;

        if (timer >= WaitTime)
        {
            timer = WaitTime;
            TimeOut.Invoke();
        }

        if ((int)timer != lastTime)
        {
            lastTime = (int)timer;
            TimeUpdated.Invoke();
        }
    }

    public void Initialize(float waitTime)
    {
        initialized = true;
        WaitTime = waitTime;
        timer = 0.0f;
        Pause();
    }

    public void ResetTime()
    {
        timer = 0.0f;
        lastTime = 0;
        TimeUpdated.Invoke();
    }

    public void ResetTime(float newWaitTime)
    {
        WaitTime = newWaitTime;
        ResetTime();
    }

    public void Pause()
    {
        print("paused");
        timerActive = false;
    }

    public void Resume()
    {
        timerActive = true;
    }



    void OnGUI()
    {
        GUIStyle labelDetails = new GUIStyle(GUI.skin.GetStyle("label"));

        // Display the recorded time in a certain size.
        labelDetails.fontSize = 6 * (width / 200);
        GUI.Label(new Rect(width - width/8, 10, width - (2 * width / 8), height - (2 * height / 4)),
            TimeLeft, labelDetails);
    }
}