using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CalibrateMenu : MonoBehaviour {

    public void StartGameWithGyro()
    {
        Director.useGyro = true;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Director>().StartCoroutine("BeginGameCoroutine");
        
    }
    public void StartGameWithoutGyro()
    {
        Director.useGyro = false;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Director>().StartCoroutine("BeginGameCoroutine");
    }
}
