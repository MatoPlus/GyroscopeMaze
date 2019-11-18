using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    public GameObject MazePrefab;
    public GameObject MazeObject;
    public static int Difficulty { get; private set; }

    void Awake()
    {
        MazePrefab = (GameObject)Resources.Load("Prefabs/Maze");
    }
    // Use this for initialization
    void Start () {
        Difficulty = 0;
        MazeObject = Instantiate(MazePrefab);
        Maze maze = MazeObject.GetComponent<Maze>();
        maze.Initialize(10, 10);
    }
    
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
    // Update is called once per frame
	void Update () {
		
	}
}
