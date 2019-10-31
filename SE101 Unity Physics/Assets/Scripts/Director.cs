using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    public GameObject BallPrefab;
    public GameObject Ball;
    public GameObject WallPrefab;
    public GameObject PlatformPrefab;
    public GameObject Platform;
    public GameObject TilePrefab;
    public GameObject Tile;

    void Awake()
    {
        BallPrefab = (GameObject)Resources.Load("Prefabs/Ball");
        WallPrefab = (GameObject)Resources.Load("Prefabs/Wall");
        //TilePrefab = (GameObject)Resources.Load("Prefabs/PlatformTile");
        PlatformPrefab = (GameObject)Resources.Load("Prefabs/Platform");
    }
    // Use this for initialization
    void Start () {
        boxPreset = new int[3, 10, 10];
        for (int i = 0; i < 10; i++)
        {
            boxPreset[0, 0, i] = 1;
            boxPreset[0, 9, i] = 1;
            boxPreset[0, 1, i/2] = 1;
            boxPreset[1, i, 0] = 1;
            boxPreset[1, i, 9] = 1;
            for (int j = 0; j < 10; j++)
            {
                boxPreset[2, i, j] = 1;
            }
            boxPreset[2, 7, 7] = 0;
        }
        MakeLevel(10,10,boxPreset);
        Ball = Instantiate(BallPrefab, new Vector3(0, (float)1.5, 0), Quaternion.identity);
    }

    public GameObject[,,] wallsArray;
    public GameObject[,] platsArray;
    public int[,,] boxPreset;
    private Vector3 origin;
    private int numWalls;
    void MakeLevel(int width, int height, int [,,] toGenerate){
        //first index in toGenerate is horizontal or vertical (0 is horiz., 1 is vert.)
        //2nd index is height, 3rd is width
        origin = new Vector3((float)(-width/2),0,(float)(-height/2+1));
        Platform = Instantiate(PlatformPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        /*Tile = Instantiate(TilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Tile.transform.SetParent(Platform.transform);
        Tile.transform.localScale = new Vector3(1,1,1);*/
        wallsArray = new GameObject[3,height, width];
        for (int i = 0; i < height; i++){
            for(int j = 0; j < width; j++){
                if(toGenerate[0,i,j] == 1){
                    GameObject wall = Instantiate(WallPrefab, new Vector3(origin.x+(float)j, (float)0.5, origin.z+(float)i), Quaternion.identity);
                    wall.transform.SetParent(Platform.transform);
                    wallsArray[0,i,j] = wall;
                }
                if (toGenerate[1, i, j] == 1)
                {
                    GameObject wall = Instantiate(WallPrefab, new Vector3(origin.x + (float)j, (float)0.5, origin.z + (float)i), Quaternion.Euler(0, 90, 0));
                    wall.transform.SetParent(Platform.transform);
                    wallsArray[1,i, j] = wall;
                }
                /*if (toGenerate[2, i, j] == 1)
                {
                    GameObject tile = Instantiate(TilePrefab, new Vector3(origin.x + (float)j, (float)0, origin.z + (float)i), Quaternion.Euler(90, 0, 90));
                    tile.transform.SetParent(Platform.transform);
                    wallsArray[2, i, j] = tile;
                }*/
            }
        }        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
