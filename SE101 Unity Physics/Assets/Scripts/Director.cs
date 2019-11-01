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
            boxPreset[1, 0, i] = 1;
            boxPreset[0, (i / 2), 2] = 1;
            boxPreset[0, 9-(i / 2), 4] = 1;
            boxPreset[1, 10, i] = 1;
            boxPreset[0, i, 0] = 1;
            boxPreset[0, i, 10] = 1;
            boxPreset[0, i, 10] = 1;
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
    MakeLevel takes in a 3D array: [Type, X, Y]. Dimensions: [3, Width of Maze, Height of Maze]
    The origin is at the bottom left of the maze; x goes right, y goes up. Currently, the platform size doesn't scale properly, but I will fix this. 
    (Don't worry about setting the origin; the maze as a whole will always have its center at 0,0,0 in the game world.)
    Index 0 (Type): 0 is horizontal walls, 1 is vertical walls. 2 is currently unused, but required (place a dummy value at 2)
    Horizontal walls start at the X,Y coordinate and continue 1 unit right, while vertical walls continue up.
    Index 1 (X): Should be an array of [[width]] arrays; the Y arrays. Each index corresponds to a "column," 0 being the leftmost.
    Index 1 (Y): Should be an array of [[height]] 0's and 1's. If a value is 1, a wall will be placed there, else no wall. Each index corresponds to a "row", 0 being the lowermost.
    Basically, two 2D arrays; the first indexed at 0, the other indexed at 1.
    e.g. array[0,0,0] = 1 means a horizontal wall will be placed at position 0,0 on the grid.
    e.g. array[1,2,3] = 1 means a vertical wall will be placed at position 2,3 on the grid (2 right, 3 up) 
    Please note: The blocks are 1 unit long, so a vertical wall placed at the very highest y value will reach past a horizontal wall placed at the highest y value. When generating a maze, it's best to keep this in mind 
    (e.g., for a maze of height 11, only place 10 vertical walls on the left and right borders (indices 0 through 9), and the wall at height 9 will touch the horizontal wall that has been placed at height 10, since it extends up one unit)
     */
    void MakeLevel(int [,,] toGenerate){
        int width = toGenerate.GetLength(1);
        int height = toGenerate.GetLength(2);
        Vector3 origin = new Vector3((float)(-width/2),0,(float)(-height/2));
        Platform = Instantiate(PlatformPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Platform.transform.localScale = new Vector3(width-1, (float)0.1, height-1);
        /*Tile = Instantiate(TilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Tile.transform.SetParent(Platform.transform);
        Tile.transform.localScale = new Vector3(1,1,1);*/
        wallsArray = new GameObject[3,width, height];
        for (int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(toGenerate[0,i,j] == 1){
                    GameObject wall = Instantiate(WallPrefab, new Vector3(origin.x + (float)i, (float)0.5, origin.z + (float)j), Quaternion.identity);//Euler(0, 180, 0));
                    wall.transform.SetParent(Platform.transform);
                    wallsArray[0,i,j] = wall;
                }
                if (toGenerate[1, i, j] == 1)
                {
                    GameObject wall = Instantiate(WallPrefab, new Vector3(origin.x + (float)i, (float)0.5, origin.z + (float)j), Quaternion.Euler(0, 270, 0));
                    wall.transform.SetParent(Platform.transform);
                    wallsArray[1,i, j] = wall;
                }
                /*if (toGenerate[2, i, j] == 1)
                {
                    GameObject tile = Instantiate(TilePrefab, new Vector3(origin.x + (float)i, (float)0, origin.z + (float)j), Quaternion.Euler(90, 0, 90));
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
