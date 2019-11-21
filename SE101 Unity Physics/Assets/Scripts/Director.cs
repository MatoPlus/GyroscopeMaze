using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    private GameObject MazePrefab;
    private GameObject MazeObject;
    private GameObject TimerPrefab;
    private GameObject PivotPrefab;
    private GameObject MenuPivotPrefab;
    private GameObject TimerObject;
    public GameObject Pivot;
    public GameObject MenuPivot;
    private MenuHandler menuHandler;
    public static SerialPort sp;
    private Timer Timer { get; set; }
    public int TimeLimit;

    public static int Difficulty;
    public static bool useGyro = true;
    public static int gyroSensitivity = 5;
    public static bool isPressed = false;

    // Use this for initialization
    void Start()
    {
        Difficulty = 50;
        TimeLimit = 90;
        menuHandler = new MenuHandler(this);
        if (useGyro)
        {
            SetupController();
        }
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

    public void IncreaseSensitivity()
    {
        gyroSensitivity = Math.Min(gyroSensitivity + 1, 10);
        menuHandler.SensAmount.GetComponent<Text>().text = gyroSensitivity.ToString();
    }

    public void DecreaseSensitivity()
    {
        gyroSensitivity = Math.Max(gyroSensitivity - 1, 3);
        menuHandler.SensAmount.GetComponent<Text>().text = gyroSensitivity.ToString();
    }

    public void Empty()
    {
        Debug.Log("Make this do something");
    }

    public static void SetupController()
    {
        // sp = new SerialPort("/dev/cu.wchusbserial14110", 9600);
        // sp = new SerialPort("COM6", 9600);
        //sp.Open();

        //Auto detect implementation.
        List<string> ports = new List<string>(SerialPort.GetPortNames());
        FilterPorts(ports);
        foreach (string p in ports)
        {
            try
            {
                print("Attempted to connect to: " + p);
                sp = new SerialPort(p, 9600);
                sp.Open();
                // Sucessfully reads input from sp, meaning the port is valid.
                if (sp.BytesToRead != 0)
                {
                    break;
                }
                //Scan inputs for "connectAlready"
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

    public static void SetupController(string port)
    {
        // sp = new SerialPort("/dev/cu.wchusbserial14110", 9600);
        // sp = new SerialPort("COM6", 9600);
        //sp.Open();
        try
        {
            print("Attempted to connect to: " + port);
            sp = new SerialPort(port, 9600);
            sp.Open();

            if (sp.BytesToRead != 0) {
                print("Selected invalid port, automatically detecting a new port...");
                SetupController();
            }

            // Sucessfully reads input from sp, meaning the port is valid.
            //Scan inputs for "connectAlready"
        }
        catch (InvalidOperationException e)
        {
            // Port in use  
            print(e);
        }
        catch (System.IO.IOException e)
        {
            // Port can't be opened
            print(e);
        }
    }



    public static void FilterPorts(List<string> ports)
    {
        List<string> search = ports.GetRange(0, ports.Count);

        // Search and remove inappropriate window and macOS ports
        foreach (string port in search)
        {
            if (port == "COM3" || port == "COM4" || port.Contains("tty"))
            {
                ports.Remove(port);
            }
        }
    }

    public static void Recalibrate()
    {
        if (sp.IsOpen)
        {
            sp.Close();
            sp.Open();
        }
    }

    public static void SetPressed(bool press)
    {
        isPressed = press;
    }

    public static void ToggleGyro()
    {
        useGyro = !useGyro;
    }
}
