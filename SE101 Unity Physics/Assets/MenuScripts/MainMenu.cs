using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {
    GameObject ButtonPrefab;
    GameObject PlayButton;
    GameObject Canvas;
    GameObject SettingsButton;
    private void Awake()
    {
        ButtonPrefab = (GameObject)Resources.Load("Prefabs/PlayButton");
        Canvas = GameObject.Find("Canvas");
    }

    private void Start()
    {

        PlayButton = Instantiate(ButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f + 20f, 0), Quaternion.identity);
        PlayButton.transform.SetParent(Canvas.transform);

        SettingsButton = Instantiate(ButtonPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f, 0), Quaternion.identity);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
