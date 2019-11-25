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
    private GameObject TimerObject;
    public GameObject Pivot;
    private MenuHandler menuHandler;
    public static SerialPort sp;
    private Timer Timer { get; set; }
    public int TimeLimit;
    private bool winning = false;

    public static int Difficulty;
    public static bool useGyro = false;
    public static int gyroSensitivity = 5;
    public static bool isPressed = false;
    public static float buttonDelayMax = 2;
    public static float buttonDelay = 0;
    private static bool isPlaying = false;
    
    IEnumerator BeginGameCoroutine()
    {
        isPlaying = false;
        SceneManager.LoadScene(1);
        do
        {
            yield return null;

        } while (SceneManager.sceneCount == 0);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        menuHandler.InstantiateMenu();
        menuHandler.CreateOpeningScreen();
        if (useGyro)
        {
            SetupController();
        }
    }

    public void BeginGame()
    {
        StartCoroutine("BeginGameCoroutine");
    }

    IEnumerator ToMainMenuCoroutine()
    {
        isPlaying = false;
        SceneManager.LoadScene(1);
        do
        {
            yield return null;

        } while (SceneManager.sceneCount == 0);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        menuHandler.InstantiateMenu();
        menuHandler.CreateOpeningScreen();
    }

    public void ToMainMenu()
    {
        StartCoroutine("ToMainMenuCoroutine");
    }
    IEnumerator StartGameCoroutine()
    {
        isPlaying = true;
        SceneManager.LoadScene(2);
        do
        {
            yield return new WaitForSeconds(0.1f);

        } while (SceneManager.sceneCount == 0);
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
        menuHandler = new MenuHandler(this);
        Difficulty = 30;
        TimeLimit = 90;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") || isPressed)
        {
            // If game session is currently going on, press button will quit the game session instead
            if (isPlaying)
            {
                ToMainMenu();
            }
            // In menu, try to press all active buttons
            else
            {
                menuHandler.PressAll();
            }
            isPressed = false;
        }
    }

    void TimerDone()
    {
        Timer.Pause();
        // END GAME HERE, THIS IS CALLED WHEN TIMER IS UP
        //"QuitCurrentSession();"
    }

    void CreateMaze()
    {
        MazeObject = Instantiate(MazePrefab);
        Maze maze = MazeObject.GetComponent<Maze>();
        maze.Initialize(10, 10, this);
    }

    void CreateTimer()
    {
        TimerObject = Instantiate(TimerPrefab);
        Timer = TimerObject.GetComponent<Timer>();
        Timer.Initialize(TimeLimit);
        Timer.Resume();
        Timer.TimeOut.AddListener(TimerDone);
    }

    void LoadPrefabs()
    {
        MazePrefab = (GameObject)Resources.Load("Prefabs/Maze/Maze");
        TimerPrefab = (GameObject)Resources.Load("Prefabs/Timer");
        PivotPrefab = (GameObject)Resources.Load("Prefabs/Pivot");
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
        TimeLimit = Math.Max(TimeLimit - 10, 30);
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

    public void QuitGame()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
    public void Empty()
    {
        Debug.Log("Make this do something");
    }

    public static void SetupController()
    {
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
        if(sp == null || !sp.IsOpen)
        {
            Debug.Log("Failed to connect to a port, switching to arrow keys...");
            useGyro = false;
            //Director.ToggleGyro();
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
                print("Selected invalid port, switching to arrow keys...");
                useGyro = false;
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

    public static List<string> GetFilteredPorts()
    {
        List<string> ports = new List<string>(SerialPort.GetPortNames());
        FilterPorts(ports);
        return ports;

    }

    public static void FilterPorts(List<string> ports)
    {
        List<string> search = ports.GetRange(0, ports.Count);

        // Search and remove inappropriate window and macOS ports
        foreach (string port in search)
        {
            if (port == "COM3" || port == "COM4" || (!port.Contains("usbserial") && port.Contains("/dev/tty")))
            {
                ports.Remove(port);
            }
        }
    }

    public static void Recalibrate()
    { 
        if (useGyro && sp.IsOpen)
        {
            sp.Close();
            sp.Open();
        }
    }

    public static void SetPressed(bool press)
    {
        isPressed = press;
    }

    public void ToggleGyro()
    {
        useGyro = !useGyro;
        if (useGyro)
        {
            if (sp != null)
            {
                sp.Close();
                SetupController(sp.PortName);
            }
            else
            {
                SetupController();
            }
            if (menuHandler != null)
            {
                menuHandler.MenuPivot.transform.rotation = Quaternion.identity;
                menuHandler.GyroButtonText.text = "Y";
            }
        }
        else
        {
            if (menuHandler != null)
            {
                menuHandler.MenuPivot.transform.rotation = Quaternion.identity;
                menuHandler.GyroButtonText.text = "N";
            }
        }
    }

    IEnumerator WinScreenCoroutine()
    {
        isPlaying = false;
        SceneManager.LoadScene(3);
        do
        {
            yield return new WaitForSeconds(0.1f);

        } while (SceneManager.sceneCount == 0);
        //menuHandler.RemoveMenu();
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        menuHandler.InstantiateMenu();
        menuHandler.MakeWinScreen(Timer.RemainingTime, TimeLimit);
        winning = false;

    }

    public void Win()
    {
        if (!winning)
        {
            StartCoroutine("WinScreenCoroutine");
            winning = true;
        }
    }
}
