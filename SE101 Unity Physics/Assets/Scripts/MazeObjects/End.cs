using UnityEngine;

namespace MazeObjects
{

    class End : FeatureObject
    {

        GameObject end;
        Maze maze;
        public int XCoord { get; private set; }
        public int YCoord { get; private set; }
        public GameObject EndPrefab { get; private set; }
        public bool[,] UniqueObjects { get; private set; }

        public End(int xCoord, int yCoord) : base(xCoord, yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
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
            end = Object.Instantiate(EndPrefab, new Vector3(XCoord + 0.5f, 0.1f, YCoord + 0.5f), Quaternion.identity);
            return;

        }

        // Update is called once per frame
        public override void Update()
        {
            return;
        }
    }
}
