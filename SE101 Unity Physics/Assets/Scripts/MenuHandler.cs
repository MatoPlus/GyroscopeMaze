using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler
{
    public List<Button> buttons;

    GameObject ButtonPrefab;
    SceneButton PlayButton;
    GameObject Canvas;
    SceneButton SettingsButton;
    private void Awake()
    {
        ButtonPrefab = (GameObject)Resources.Load("Prefabs/PlayButton");
        Canvas = GameObject.Find("Canvas");
    }

    private void BuildMainMenu()
    {
        PlayButton = new SceneButton(UnityEngine.Object.Instantiate(ButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f + 20f, 0), Quaternion.identity));
        buttons.Add(PlayButton);
        PlayButton.attached.transform.SetParent(Canvas.transform);

        //SettingsButton = UnityEngine.Object.Instantiate(ButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f, 0), Quaternion.identity);
    }

}