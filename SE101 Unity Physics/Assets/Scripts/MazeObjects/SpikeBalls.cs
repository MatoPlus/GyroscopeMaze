using UnityEngine;

namespace MazeObjects
{
    class SpikeBalls : Feature
    {

        public Vector3 Origin { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int[,,] Map { get; private set; }
        public GameObject MazeObject { get; private set; }
        public GameObject SpikeBallPrefab { get; private set; }


        public SpikeBalls(Maze maze, int[,,] map, bool [,] uniqueObjects) : base(uniqueObjects)
        {
            Origin = maze.Origin;
            Height = maze.Height + 1;
            Width = maze.Width + 1;
            Map = map;
            MazeObject = maze.gameObject;
            SpikeBallPrefab = (GameObject)Resources.Load("Prefabs/SpikeBall");

        }

        // Use this for initialization
        public void Initialize()
        {
            
        }

        public override void Build()
        {

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Map[0, i, j] == 1)
                    {

                        GameObject ball = Object.Instantiate(SpikeBallPrefab, new Vector3(Origin.x + (float)i, (float)0.5, Origin.z + (float)j), Quaternion.identity);//Euler(0, 180, 0));
                        ball.transform.SetParent(MazeObject.transform);
                        uniqueObjects[i, j] = ball;
                    }
                    if (Map[1, i, j] == 1)
                    {
                        GameObject ball = Object.Instantiate(SpikeBallPrefab, new Vector3(Origin.x + (float)i + 0.5f, (float)0.5, Origin.z + (float)j + 0.5f), Quaternion.Euler(0, 270, 0));
                        ball.transform.SetParent(MazeObject.transform);
                        uniqueObjects[i, j] = ball;
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