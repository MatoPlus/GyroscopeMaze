using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler
{
    public List<Button> buttons;

    private GameObject StageButtonPrefab;
    GameObject Canvas;
    public MenuHandler()
    {
        buttons = new List<Button>();
        StageButtonPrefab = (GameObject)Resources.Load("Prefabs/StageButton");
        Canvas = GameObject.Find("Canvas");
        BuildMainMenu();
    }

    public void BuildMainMenu()
    {

        StageButton PlayButton = new StageButton(Screen.width / 2f, Screen.height / 2f + 20, StageButtonPrefab, "Play", "Game", Canvas);
        buttons.Add(PlayButton);
        StageButton SettingsButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 30, StageButtonPrefab, "Settings", "Settings", Canvas);
        buttons.Add(SettingsButton);
        StageButton QuitButton = new StageButton(Screen.width / 2f, Screen.height / 2f - 80, StageButtonPrefab, "Quit", "Quit", Canvas);
        buttons.Add(QuitButton);

        //SettingsButton = UnityEngine.Object.Instantiate(ButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f, 0), Quaternion.identity);
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