using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler
{
    public List<Button> buttons;

    private GameObject PlayButtonPrefab;
    GameObject Canvas;
    SceneButton SettingsButton;
    public MenuHandler()
    {
        buttons = new List<Button>();
        PlayButtonPrefab = (GameObject)Resources.Load("Prefabs/PlayButton");
        Canvas = GameObject.Find("Canvas");
        BuildMainMenu();
    }

    public void BuildMainMenu()
    {
        SceneButton PlayButton = new SceneButton(Screen.width / 2f, Screen.height / 2f + 20, PlayButtonPrefab, "Game", Canvas);
        buttons.Add(PlayButton);

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