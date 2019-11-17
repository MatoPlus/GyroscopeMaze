using UnityEngine;

namespace MazeObjects
{
    class Walls : Feature
    {

        public Vector3 Origin { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int[,,] Map { get; private set; }
        public GameObject[,,] WallsArray { get; private set; }
        public GameObject MazeObject { get; private set; }
        public GameObject WallPrefab { get; private set; }


        public Walls(Maze maze, int [,,] map): base()
        {
            Origin = maze.Origin;
            Height = maze.Height + 1;
            Width = maze.Width + 1;
            Map = map;
            MazeObject = maze.gameObject;
            WallPrefab = (GameObject)Resources.Load("Prefabs/Wall");

        }

        // Use this for initialization
        public void Initialize()
        {
        }

        public override void Build()
        {
            WallsArray = new GameObject[3, Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Map[0, i, j] == 1)
                    {
                        
                        GameObject wall = Object.Instantiate(WallPrefab, new Vector3(Origin.x + (float)i, (float)0.5, Origin.z + (float)j), Quaternion.identity);//Euler(0, 180, 0));
                        wall.transform.SetParent(MazeObject.transform);
                        WallsArray[0, i, j] = wall;
                    }
                    if (Map[1, i, j] == 1)
                    {
                        GameObject wall = Object.Instantiate(WallPrefab, new Vector3(Origin.x + (float)i, (float)0.5, Origin.z + (float)j), Quaternion.Euler(0, 270, 0));
                        wall.transform.SetParent(MazeObject.transform);
                        WallsArray[1, i, j] = wall;
                    }
                }
            }
        }

        public override void Update()
        {
            return;
        }
    }
}