using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler
{
    public List<Button> buttons;
    private Director director;
    private GameObject Title;
    private GameObject SettingsTitle;
    private GameObject Canvas;
    private GameObject Cursor;
    private GameObject MenuPivot;
    private GameObject MainMenu;
    private GameObject SettingsMenu;

    private GameObject StageButtonPrefab;
    private GameObject PlusButtonPrefab;
    private GameObject TitlePrefab;

    private GameObject CanvasPrefab;
    private GameObject CursorPrefab;
    private GameObject MenuPivotPrefab;
    private GameObject DiffTitle;
    private GameObject TimerTitle;
    private GameObject SensTitle;
    private GameObject GyroTitle;
    public GameObject TimerAmount;
    public GameObject DiffAmount;
    public GameObject SensAmount;
    public Text GyroButtonText;
    private GameObject WinScreenTitle;
    private GameObject ScoreDisplay;

    public MenuHandler(Director director)
    {
        this.director = director;
        buttons = new List<Button>();
        StageButtonPrefab = (GameObject)Resources.Load("Prefabs/Menu/StageButton");
        PlusButtonPrefab = (GameObject)Resources.Load("Prefabs/Menu/PlusButton");
        TitlePrefab = (GameObject)Resources.Load("Prefabs/Menu/Title");
        CanvasPrefab = (GameObject)Resources.Load("Prefabs/Menu/Canvas");
        CursorPrefab = (GameObject)Resources.Load("Prefabs/Menu/Cursor");
        MenuPivotPrefab = (GameObject)Resources.Load("Prefabs/Menu/MenuPivot");
        InstantiateMenu();
    }

    public void CreateOpeningScreen()
    {
        BuildMainMenu();
        BuildSettingsMenu();
        GoToMainMenu();
    }

    public void InstantiateMenu()
    {
        Canvas = UnityEngine.Object.Instantiate(CanvasPrefab,  new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        MainMenu = new GameObject("MainMenu");
        MainMenu.transform.SetParent(Canvas.transform);
        SettingsMenu = new GameObject("SettingsMenu");
        SettingsMenu.transform.SetParent(Canvas.transform);
        Cursor = UnityEngine.Object.Instantiate(CursorPrefab, new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        Cursor.transform.SetParent(Canvas.transform);
        MenuPivot = UnityEngine.Object.Instantiate(MenuPivotPrefab);
        MenuPivot.GetComponent<Mouse>().mouse = Cursor;
    }

    private void BuildMainMenu()
    {
        Title = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        Title.transform.SetParent(MainMenu.transform);
        Title.GetComponent<Text>().text = "Gyroscope Maze";
        List<Action> buttonActions = new List<Action>();
        buttonActions.Add(director.StartGame);
        StageButton PlayButton = new StageButton(Screen.width / 2f, Screen.height / 2f + 20, StageButtonPrefab, "Play", buttonActions, Canvas);
        buttons.Add(PlayButton);
        PlayButton.attached.transform.SetParent(MainMenu.transform);

        buttonActions.Clear();
        //buttonActions.Add(director.Empty);
        buttonActions.Add(GoToSettings);
        StageButton SettingsButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 30, StageButtonPrefab, "Settings", buttonActions, Canvas);
        buttons.Add(SettingsButton);
        SettingsButton.attached.transform.SetParent(MainMenu.transform);

        buttonActions.Clear();
        buttonActions.Add(director.Empty);
        StageButton QuitButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, StageButtonPrefab, "Quit", buttonActions, Canvas);
        buttons.Add(QuitButton);
        QuitButton.attached.transform.SetParent(MainMenu.transform);
    }

    public void MakeWinScreen(float remainingTime, float initialTime)
    {
        WinScreenTitle = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        WinScreenTitle.transform.SetParent(MainMenu.transform);
        WinScreenTitle.GetComponent<Text>().text = "You win!";

        int score = (int) Math.Floor(1000 * remainingTime * Director.Difficulty / initialTime);
        ScoreDisplay = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height - 110f, 0), Quaternion.identity);
        ScoreDisplay.transform.SetParent(MainMenu.transform);
        ScoreDisplay.GetComponent<Text>().text = "Score: " + score.ToString();

        List<Action> buttonActions = new List<Action>();
        buttonActions.Add(director.BeginGame);
        StageButton NextButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 40, StageButtonPrefab, "Next", buttonActions, Canvas);
        buttons.Add(NextButton);
        NextButton.attached.transform.SetParent(MainMenu.transform);
    }

    public void BuildSettingsMenu()
    {
        SettingsTitle = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height - 30f, 0), Quaternion.identity);
        SettingsTitle.transform.SetParent(SettingsMenu.transform);
        SettingsTitle.GetComponent<Text>().text = "Settings";

        List<Action> buttonActions = new List<Action>();

        buttonActions.Add(GoToMainMenu);
        StageButton BackButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 130, StageButtonPrefab, "Back", buttonActions, Canvas);
        buttons.Add(BackButton);
        BackButton.attached.transform.SetParent(SettingsMenu.transform);

        buttonActions.Clear();
        buttonActions.Add(Director.Recalibrate);
        StageButton CalibrateButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 40, StageButtonPrefab, "Calibrate", buttonActions, Canvas);
        buttons.Add(CalibrateButton);
        CalibrateButton.attached.transform.SetParent(SettingsMenu.transform);

        /*buttonActions.Clear();
        buttonActions.Add(director.Empty);
        StageButton PortsButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, StageButtonPrefab, "Serial Port", buttonActions, Canvas);
        buttons.Add(PortsButton);
        PortsButton.attached.transform.SetParent(SettingsMenu.transform);*/

        GyroTitle = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f - 100, Screen.height / 2f - 80, 0), Quaternion.identity);
        GyroTitle.transform.SetParent(SettingsMenu.transform);
        GyroTitle.GetComponent<Text>().text = "Use gyro?";

        buttonActions.Clear();
        buttonActions.Add(director.ToggleGyro);
        StageButton ToggleButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, PlusButtonPrefab, "Y", buttonActions, Canvas);
        GyroButtonText = ToggleButton.attached.GetComponentInChildren<Text>();
        buttons.Add(ToggleButton);
        ToggleButton.attached.transform.SetParent(SettingsMenu.transform);
        if (Director.useGyro)
        {
            GyroButtonText.text = "Y";
        }
        else
        {
            GyroButtonText.text = "N";
        }

        buttonActions.Clear();
        buttonActions.Add(director.IncreaseDifficulty);
        StageButton DiffPlus = new StageButton(Screen.width / 2f + 100, Screen.height / 2f + 5, PlusButtonPrefab, "+", buttonActions, Canvas);
        buttons.Add(DiffPlus);
        DiffPlus.attached.transform.SetParent(SettingsMenu.transform);

        buttonActions.Clear();
        buttonActions.Add(director.DecreaseDifficulty);
        StageButton DiffMinus = new StageButton(Screen.width / 2f - 100, Screen.height / 2f + 5, PlusButtonPrefab, "-", buttonActions, Canvas);
        buttons.Add(DiffMinus);
        DiffMinus.attached.transform.SetParent(SettingsMenu.transform);

        DiffTitle = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f - 200, Screen.height / 2f + 5, 0), Quaternion.identity);
        DiffTitle.transform.SetParent(SettingsMenu.transform);
        DiffTitle.GetComponent<Text>().text = "Difficulty";

        DiffAmount = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height / 2f + 5, 0), Quaternion.identity);
        DiffAmount.transform.SetParent(SettingsMenu.transform);
        DiffAmount.GetComponent<Text>().text = Director.Difficulty.ToString();

        buttonActions.Clear();
        buttonActions.Add(director.IncreaseTimer);
        StageButton TimerPlus = new StageButton(Screen.width / 2f + 100, Screen.height / 2f + 50, PlusButtonPrefab, "+", buttonActions, Canvas);
        buttons.Add(TimerPlus);
        TimerPlus.attached.transform.SetParent(SettingsMenu.transform);

        buttonActions.Clear();
        buttonActions.Add(director.DecreaseTimer);
        StageButton TimerMinus = new StageButton(Screen.width / 2f - 100, Screen.height / 2f + 50, PlusButtonPrefab, "-", buttonActions, Canvas);
        buttons.Add(TimerMinus);
        TimerMinus.attached.transform.SetParent(SettingsMenu.transform);

        TimerTitle = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f - 200, Screen.height / 2f + 50, 0), Quaternion.identity);
        TimerTitle.transform.SetParent(SettingsMenu.transform);
        TimerTitle.GetComponent<Text>().text = "Timer";

        TimerAmount = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height / 2f + 50, 0), Quaternion.identity);
        TimerAmount.transform.SetParent(SettingsMenu.transform);
        TimerAmount.GetComponent<Text>().text = director.TimeLimit.ToString();
        //
        buttonActions.Clear();
        buttonActions.Add(director.IncreaseSensitivity);
        StageButton SensPlus = new StageButton(Screen.width / 2f + 100, Screen.height / 2f + 95, PlusButtonPrefab, "+", buttonActions, Canvas);
        buttons.Add(SensPlus);
        SensPlus.attached.transform.SetParent(SettingsMenu.transform);

        buttonActions.Clear();
        buttonActions.Add(director.DecreaseSensitivity);
        StageButton SensMinus = new StageButton(Screen.width / 2f - 100, Screen.height / 2f + 95, PlusButtonPrefab, "-", buttonActions, Canvas);
        buttons.Add(SensMinus);
        SensMinus.attached.transform.SetParent(SettingsMenu.transform);

        SensTitle = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f - 200, Screen.height / 2f + 95, 0), Quaternion.identity);
        SensTitle.transform.SetParent(SettingsMenu.transform);
        SensTitle.GetComponent<Text>().text = "Sensitivity";

        SensAmount = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height / 2f + 95, 0), Quaternion.identity);
        SensAmount.transform.SetParent(SettingsMenu.transform);
        SensAmount.GetComponent<Text>().text = Director.gyroSensitivity.ToString();
    }

    private void GoToSettings()
    {
        foreach(Button i in buttons)
        {
            i.active = false;
        }
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    private void GoToMainMenu()
    {
        foreach (Button i in buttons)
        {
            i.active = false;
        }
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void RemoveMenu()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        UnityEngine.Object.Destroy(MenuPivot);
    }

    public void PressAll()
    {
        foreach (Button i in buttons)
        {
            if (i.active)
            {
                i.Press();
            }
        }
    }

    /*public void BuildPortsDropdown(List<string> ports)
{
    for (int i = 0; i < ports.Count; i++)
    {

    }
}*/

}