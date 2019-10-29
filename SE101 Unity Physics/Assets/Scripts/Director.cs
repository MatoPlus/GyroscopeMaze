using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    public GameObject BallPrefab;
    public GameObject Ball;
    public GameObject WallPrefab;
    public GameObject PlatformPrefab;
    public GameObject Platform;

    void Awake()
    {
        BallPrefab = (GameObject)Resources.Load("Prefabs/Ball");
        WallPrefab = (GameObject)Resources.Load("Prefabs/Wall");
        PlatformPrefab = (GameObject)Resources.Load("Prefabs/Platform");
    }
    // Use this for initialization
    void Start () {
        Ball = Instantiate(BallPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        MakeLevel("box");
    }

    public GameObject[] wallsArray;
    public int numWalls;
    void MakeLevel(string template)
    {
        Platform = Instantiate(PlatformPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (template == "box")
        {
            numWalls = 40;
            wallsArray = new GameObject[numWalls];
            for (int i = 0; i < 10; i++)
            {
                GameObject wall = Instantiate(WallPrefab, new Vector3((float)(i - 4.5), (float)0.5, -5), Quaternion.identity);
                wall.transform.SetParent(Platform.transform);
                wallsArray[i] = wall;
            }
            for (int i = 0; i < 10; i++)
            {
                GameObject wall = Instantiate(WallPrefab, new Vector3((float)(i - 4.5), (float)0.5, 5), Quaternion.identity);
                wall.transform.SetParent(Platform.transform);
                wallsArray[i+10] = wall;
            }
            for (int i = 0; i < 10; i++)
            {
                GameObject wall = Instantiate(WallPrefab, new Vector3((float)-5, (float)0.5, (float)(i-4.5)), Quaternion.Euler(0,90,0));
                wall.transform.SetParent(Platform.transform);
                wallsArray[i+20] = wall;
            }
            for (int i = 0; i < 10; i++)
            {
                GameObject wall = Instantiate(WallPrefab, new Vector3((float)5, (float)0.5, (float)(i-4.5)), Quaternion.Euler(0, 90, 0));
                wall.transform.SetParent(Platform.transform);
                wallsArray[i+30] = wall;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
