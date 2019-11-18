using UnityEngine;

namespace MazeObjects
{

    class End : FeatureObject
    {

        GameObject end;
        GameObject endPrefab;
        Maze maze;
        public int XCoord { get; private set; }
        public int YCoord { get; private set; }
        public GameObject EndPrefab { get; private set; }
        public bool[,] UniqueObjects { get; private set; }


        public End(int xCoord, int yCoord, bool[,] uniqueObjects) : base(xCoord, yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
            UniqueObjects[xCoord, yCoord] = true;
            EndPrefab = (GameObject)Resources.Load("Prefabs/End");

        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(end);
        }

        // Builds object in unity
        public override void Build()
        {
            return;

        }

        // Update is called once per frame
        public override void Update()
        {
            return;
        }
    }
}
