using UnityEngine;

namespace MazeObjects
{

    class Start : FeatureObject
    {

        GameObject start;
        Maze maze;
        public GameObject StartPrefab { get; private set; }

        public Start(int xCoord, int yCoord, Maze maze) : base(xCoord, yCoord)
        {
            this.maze = maze;
            StartPrefab = (GameObject)Resources.Load("Prefabs/Maze/Start");
            Build();
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(start);
        }

        // Builds object in unity
        public override void Build()
        {
            start = Object.Instantiate(StartPrefab, new Vector3(X + 0.5f, 0.1f, Y + 0.5f), Quaternion.identity);
        }

        // Update is called once per frame
        public override void Update()
        {
            return;
        }
    }
}
