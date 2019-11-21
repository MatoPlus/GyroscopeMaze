using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour {
    private GameObject MazePrefab;
    private GameObject MazeObject;
    private GameObject TimerPrefab;
    private GameObject TimerObject;
    private MenuHandler menuHandler;
    private Timer Timer { get; set; }
    public int TimeLimit;

    public static int Difficulty;
    public static bool isPressed = false;

    // Use this for initialization
    void Start()
    {
        Difficulty = 50;
        TimeLimit = 90;
        menuHandler = new MenuHandler(this);
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

    public void IncreaseDifficulty()
    {
        Difficulty = Math.Min(Difficulty + 10, 100);
        menuHandler.DiffAmount.GetComponent<Text>().text = Difficulty.ToString();
    }

    public void DecreaseDifficulty()
    {
        Difficulty = Math.Max(Difficulty - 10, 0);
        menuHandler.DiffAmount.GetComponent<Text>().text = Difficulty.ToString();
    }

    public void IncreaseTimer()
    {
        TimeLimit += 10;
        menuHandler.TimerAmount.GetComponent<Text>().text = TimeLimit.ToString();
    }

    public void DecreaseTimer()
    {
        TimeLimit = Math.Max(TimeLimit - 10, 90);
        menuHandler.TimerAmount.GetComponent<Text>().text = TimeLimit.ToString();
    }

    public void Empty()
    {

    }

    public static void SetPressed(bool press)
    {
        isPressed = press;
    }
}
