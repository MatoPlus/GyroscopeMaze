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
    private GameObject TitlePrefab;

    private GameObject CanvasPrefab;
    private GameObject CursorPrefab;
    private GameObject PivotPrefab;
    


    public MenuHandler(Director director)
    {
        this.director = director;
        buttons = new List<Button>();
        StageButtonPrefab = (GameObject)Resources.Load("Prefabs/Menu/StageButton");
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
        StageButton BackButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 40, StageButtonPrefab, "Back", buttonActions, Canvas);
        buttons.Add(BackButton);
        BackButton.attached.transform.SetParent(SettingsMenu.transform);

        /*buttonActions.Clear();
        buttonActions.Add(director.Empty);
        StageButton SettingsButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 30, StageButtonPrefab, "Settings", buttonActions, Canvas);
        buttons.Add(SettingsButton);
        SettingsButton.attached.transform.SetParent(MainMenu.transform);

        StageButton QuitButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, StageButtonPrefab, "Quit", buttonActions, Canvas);
        buttons.Add(QuitButton);
        QuitButton.attached.transform.SetParent(MainMenu.transform);*/
    }

    private void GoToSettings()
    {
        Debug.Log("going to settings");
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    private void GoToMainMenu()
    {
        Debug.Log("going to menu");
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