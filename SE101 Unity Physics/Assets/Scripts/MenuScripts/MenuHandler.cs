using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler
{
    public List<Button> buttons;
    private Director director;
    private GameObject Title;
    private GameObject Canvas;
    private GameObject Cursor;
    private GameObject Pivot;

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
    }

    public void InstantiateMenu()
    {
        Canvas = UnityEngine.Object.Instantiate(CanvasPrefab,  new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        Cursor = UnityEngine.Object.Instantiate(CursorPrefab, new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        Cursor.transform.SetParent(Canvas.transform);
        Pivot = UnityEngine.Object.Instantiate(PivotPrefab);
        Pivot.GetComponent<Mouse>().mouse = Cursor;
    }
    public void BuildMainMenu()
    {
        ClearScreen();
        Title = UnityEngine.Object.Instantiate(TitlePrefab, new Vector3(Screen.width / 2f, Screen.height - 70f, 0), Quaternion.identity);
        Title.transform.SetParent(Canvas.transform);
        Title.GetComponent<Text>().text = "Gyroscope Maze";
        List<Action> buttonActions = new List<Action>();
        buttonActions.Add(director.StartGame);
        StageButton PlayButton = new StageButton(Screen.width / 2f, Screen.height / 2f + 20, StageButtonPrefab, "Play", buttonActions, Canvas);
        buttons.Add(PlayButton);

        buttonActions.Clear();
        buttonActions.Add(director.Empty);
        StageButton SettingsButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 30, StageButtonPrefab, "Settings", buttonActions, Canvas);
        buttons.Add(SettingsButton);

        StageButton QuitButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, StageButtonPrefab, "Quit", buttonActions, Canvas);
        buttons.Add(QuitButton);

        //SettingsButton = UnityEngine.Object.Instantiate(ButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f, 0), Quaternion.identity);
    }

    private void ClearScreen()
    {
        foreach (Button i in buttons)
        {
            i.Remove();
        }
        UnityEngine.Object.Destroy(Title);
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