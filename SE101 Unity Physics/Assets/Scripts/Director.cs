using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    private GameObject MazePrefab;
    private GameObject MazeObject;
    private GameObject TimerPrefab;
    private GameObject TimerObject;
    private MenuHandler menuHandler;
    private Timer Timer { get; set; }
    
    public static int Difficulty { get; private set; }
    public static bool isPressed = false;

    // Use this for initialization
    void Start()
    {
        menuHandler = new MenuHandler(this);
        Difficulty = 0;
    }

    public void StartGame()
    {
        CreateMaze();
        CreateTimer();
    }

    void Awake()
    {
        LoadPrefabs();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") || isPressed)
        {
            menuHandler.PressAll();
        }
    }

    void TimerDone()
    {
        // Timer.ResetTime();
        Timer.Pause();
    }

    void TimerUpdate()
    {
        print(Timer.TimeLeft);
    }

    void CreateMaze()
    {
        MazeObject = Instantiate(MazePrefab);
        Maze maze = MazeObject.GetComponent<Maze>();
        maze.Initialize(10, 10);
    }

    void CreateTimer()
    {
        TimerObject = Instantiate(TimerPrefab);
        Timer = TimerObject.GetComponent<Timer>();
        Timer.Initialize(4);
        Timer.Resume();
        Timer.TimeOut.AddListener(TimerDone);
        Timer.TimeUpdated.AddListener(TimerUpdate);
    }

    void LoadPrefabs()
    {
        MazePrefab = (GameObject)Resources.Load("Prefabs/Maze/Maze");
        TimerPrefab = (GameObject)Resources.Load("Prefabs/Timer");
    }

    public void Empty()
    {

    }

    public static void SetPressed(bool press)
    {
        isPressed = press;
    }
}
