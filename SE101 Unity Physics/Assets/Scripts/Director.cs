using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Director : MonoBehaviour {
    private GameObject MazePrefab;
    private GameObject MazeObject;
    private GameObject TimerPrefab;
    private GameObject PivotPrefab;
    private GameObject MenuPivotPrefab;
    private GameObject TimerObject;
    public GameObject Pivot;
    public GameObject MenuPivot;
    private MenuHandler menuHandler;
    private Timer Timer { get; set; }
    public int TimeLimit;

    public static int Difficulty;
    public static bool useGyro = true;
    public static bool isPressed = false;

    // Use this for initialization
    void Start()
    {
        Difficulty = 50;
        TimeLimit = 90;
        menuHandler = new MenuHandler(this);
    }

    IEnumerator StartGameCoroutine()
    {
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(0.1f);
        Debug.Log("this worked");
        //menuHandler.RemoveMenu();
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        CreateMaze();
        CreateTimer();
        Pivot = Instantiate(PivotPrefab);
    }
    public void StartGame()
    {
        StartCoroutine("StartGameCoroutine");
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadPrefabs();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") || isPressed)
        {
            menuHandler.PressAll();
            isPressed = false;
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
        PivotPrefab = (GameObject)Resources.Load("Prefabs/Pivot");
        MenuPivotPrefab = (GameObject)Resources.Load("Prefabs/MenuPivot");
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
        Debug.Log("Make this do something");
    }

    public static void SetPressed(bool press)
    {
        isPressed = press;
    }
}
