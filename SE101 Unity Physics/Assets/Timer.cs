using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private UnityEvent timeOut;
    public UnityEvent TimeOut {
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
    private int lastTime;
    private bool timerActive;
    public string TimeLeft { get { return FormatTime(); } }

    private string FormatTime()
    {
        int remaining = (int) (WaitTime - timer);
        int minutes = remaining / 60;
        int seconds = remaining % 60;
        return minutes.ToString() + ":" + seconds.ToString();
    }

    void Awake()
    {
        //width = Screen.width;
        //height = Screen.height;
    }

    void Update()
    {
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
        // TimeUpdated.Invoke();
    }

    public void Resume()
    {
        timerActive = true;
        // TimeUpdated.Invoke();
    }


    /*
    void OnGUI()
    {
        GUIStyle sliderDetails = new GUIStyle(GUI.skin.GetStyle("horizontalSlider"));
        GUIStyle sliderThumbDetails = new GUIStyle(GUI.skin.GetStyle("horizontalSliderThumb"));
        GUIStyle labelDetails = new GUIStyle(GUI.skin.GetStyle("label"));

        // Set the size of the fonts and the width/height of the slider.
        labelDetails.fontSize = 6 * (width / 200);
        sliderDetails.fixedHeight = height / 32;
        sliderDetails.fontSize = 12 * (width / 200);
        sliderThumbDetails.fixedHeight = height / 32;
        sliderThumbDetails.fixedWidth = width / 32;

        // Show the slider. Make the scale to be ten times bigger than the needed size.
        value = GUI.HorizontalSlider(new Rect(width / 8, height / 4, width - (4 * width / 8), height - (2 * height / 4)),
            value, 5.0f, 20.0f, sliderDetails, sliderThumbDetails);

        // Show the value from the slider. Make sure that 0.5, 0.6... 1.9, 2.0 are shown.
        float v = ((float)Mathf.RoundToInt(value)) / 10.0f;
        GUI.Label(new Rect(width / 8, height / 3.25f, width - (2 * width / 8), height - (2 * height / 4)),
            "timeScale: " + v.ToString("f1"), labelDetails);
        scrollBar = v;

        // Display the recorded time in a certain size.
        labelDetails.fontSize = 14 * (width / 200);
        GUI.Label(new Rect(width / 8, height / 2, width - (2 * width / 8), height - (2 * height / 4)),
            "Timer value is: " + visualTime.ToString("f4") + " seconds.", labelDetails);
    }*/
}