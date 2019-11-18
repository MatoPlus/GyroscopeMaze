using UnityEngine;

namespace MazeObjects
{

    class Start : FeatureObject
    {

        GameObject start;
        Maze maze;
        public int XCoord { get; private set; }
        public int YCoord { get; private set; }
        public GameObject StartPrefab { get; private set; }
        public bool [,] UniqueObjects { get; private set; }

        public Start(int xCoord, int yCoord) : base(xCoord, yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
            StartPrefab = (GameObject)Resources.Load("Prefabs/Start");
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
            start = Object.Instantiate(StartPrefab, new Vector3(XCoord + 0.5f, 0.1f, YCoord + 0.5f), Quaternion.identity);
        }

        // Update is called once per frame
        public override void Update()
        {
            return;
        }
    }
}
