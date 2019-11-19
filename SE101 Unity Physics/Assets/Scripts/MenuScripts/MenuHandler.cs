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
    }

    private void BuildMainMenu()
    {
        SceneButton PlayButton = new SceneButton(UnityEngine.Object.Instantiate(PlayButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f + 20f, 0), Quaternion.identity));
        buttons.Add(PlayButton);
        PlayButton.attached.transform.SetParent(Canvas.transform);
        //PlayButton.attached.GetComponent<PressScript>();

        //SettingsButton = UnityEngine.Object.Instantiate(ButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f, 0), Quaternion.identity);
    }

}