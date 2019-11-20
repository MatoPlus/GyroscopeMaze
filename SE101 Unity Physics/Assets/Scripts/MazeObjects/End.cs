using UnityEngine;

namespace MazeObjects
{

    class End : FeatureObject
    {

        GameObject end;
        Maze maze;
        public GameObject EndPrefab { get; private set; }
        public bool[,] UniqueObjects { get; private set; }

        public End(int xCoord, int yCoord, Maze maze) : base(xCoord, yCoord)
        {
            this.maze = maze;
            EndPrefab = (GameObject)Resources.Load("Prefabs/Maze/End");
            Build();
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(end);
        }

        // Builds object in unity
        public override void Build()
        {
            end = Object.Instantiate(EndPrefab, new Vector3(X + 0.5f, 0.1f, Y + 0.5f), Quaternion.identity);
            end.GetComponent<TriggerObject>().OnCollisionWithBall.AddListener(maze.MazeComplete);
            return;

        }

        // Update is called once per frame
        public override void Update()
        {
            return;
        }
    }
}
