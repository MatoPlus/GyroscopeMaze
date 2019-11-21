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
    private GameObject Pivot;
    private GameObject MainMenu;
    private GameObject SettingsMenu;

    private GameObject StageButtonPrefab;
    private GameObject PlusButtonPrefab;
    private GameObject TitlePrefab;

    private GameObject CanvasPrefab;
    private GameObject CursorPrefab;
    private GameObject PivotPrefab;
    private GameObject DiffTitle;
    private GameObject TimerTitle;
    public GameObject TimerAmount;
    public GameObject DiffAmount;

    public MenuHandler(Director director)
    {
        this.director = director;
        buttons = new List<Button>();
        StageButtonPrefab = (GameObject)Resources.Load("Prefabs/Menu/StageButton");
        PlusButtonPrefab = (GameObject)Resources.Load("Prefabs/Menu/PlusButton");
        TitlePrefab = (GameObject)Resources.Load("Prefabs/Menu/Title");
        CanvasPrefab = (GameObject)Resources.Load("Prefabs/Menu/Canvas");
        CursorPrefab = (GameObject)Resources.Load("Prefabs/Menu/Cursor");
        PivotPrefab = (GameObject)Resources.Load("Prefabs/Menu/MenuPivot");
        InstantiateMenu();
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
        Pivot = UnityEngine.Object.Instantiate(PivotPrefab);
        Pivot.GetComponent<Mouse>().mouse = Cursor;
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

    public void BuildSettingsMenu()
    {
        SettingsTitle = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        SettingsTitle.transform.SetParent(SettingsMenu.transform);
        SettingsTitle.GetComponent<Text>().text = "Settings";

        List<Action> buttonActions = new List<Action>();
        buttonActions.Add(GoToMainMenu);
        StageButton BackButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, StageButtonPrefab, "Back", buttonActions, Canvas);
        buttons.Add(BackButton);
        BackButton.attached.transform.SetParent(SettingsMenu.transform);

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

        /*StageButton QuitButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, StageButtonPrefab, "Quit", buttonActions, Canvas);
        buttons.Add(QuitButton);
        QuitButton.attached.transform.SetParent(SettingsMenu.transform);*/
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

    private void RemoveMenu()
    {
        //Pivot.Destroy();
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

}