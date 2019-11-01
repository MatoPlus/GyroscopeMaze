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
        boxPreset = new int[3, 11, 11];
        for (int i = 0; i < 10; i++)
        {
            boxPreset[0, 0, i] = 1;
            boxPreset[1, i, 2] = 1;
            boxPreset[0, 10, i] = 1;
            boxPreset[1, i, 0] = 1;
            boxPreset[1, i, 10] = 1;
            for (int j = 0; j < 10; j++)
            {
                boxPreset[2, i, j] = 1;
            }
        }
        MakeLevel(boxPreset);
        Ball = Instantiate(BallPrefab, new Vector3(0, (float)1.5, 0), Quaternion.identity);
    }

    public GameObject[,,] wallsArray;
    public int[,,] boxPreset;

    /*
    MakeLevel takes in a 3D array: [Type, Y, X]. Dimensions: [3, Width of Maze, Height of Maze]
    The origin is at the bottom left of the maze; x goes right, y goes up. Currently, the platform size doesn't scale properly, but I will fix this. 
    (Don't worry about setting the origin; the maze as a whole will always have its center at 0,0,0 in the game world.)
    Index 0 (Type): 0 is horizontal walls, 1 is vertical walls. 2 is currently unused, but required (place a dummy value at 2)
    Index 1 (Y): Should be an array of [[height]] 0's and 1's. If a value is 1, a wall will be placed there, else no wall.
    Index 1 (X): Should be an array of [[width]] 0's and 1's. If a value is 1, a wall will be placed there, else no wall.
    e.g. array[0,0,0] = 1 means a horizontal wall will be placed at position 0,0 on the grid.
    e.g. array[1,2,3] = 1 means a vertical wall will be placed at position 3,2 on the grid (3 right, 2 up)  
     */
    void MakeLevel(int [,,] toGenerate){
        int height = toGenerate.GetLength(1);
        int width = toGenerate.GetLength(2);
        Vector3 origin = new Vector3((float)(-width/2),0,(float)(-height/2));
        Platform = Instantiate(PlatformPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        /*Tile = Instantiate(TilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Tile.transform.SetParent(Platform.transform);
        Tile.transform.localScale = new Vector3(1,1,1);*/
        wallsArray = new GameObject[3,height, width];
        for (int i = 0; i < height; i++){
            for(int j = 0; j < width; j++){
                if(toGenerate[0,i,j] == 1){
                    GameObject wall = Instantiate(WallPrefab, new Vector3(origin.x + (float)j, (float)0.5, origin.z + (float)i), Quaternion.identity);//Euler(0, 180, 0));
                    wall.transform.SetParent(Platform.transform);
                    wallsArray[0,i,j] = wall;
                }
                if (toGenerate[1, i, j] == 1)
                {
                    GameObject wall = Instantiate(WallPrefab, new Vector3(origin.x + (float)j, (float)0.5, origin.z + (float)i), Quaternion.Euler(0, 270, 0));
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
