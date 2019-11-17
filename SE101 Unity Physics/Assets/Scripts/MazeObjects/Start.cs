using UnityEngine;

namespace MazeObjects
{

    class Start : FeatureObject
    {

        GameObject start;
        GameObject startPrefab;
        Maze maze;
        public int XCoord { get; private set; }
        public int YCoord { get; private set; }
        public GameObject StartPrefab { get; private set; }
        public bool [,] UniqueObjects { get; private set; }


        public Start(int xCoord, int yCoord, bool [,]  uniqueObjects) : base(xCoord, yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
            UniqueObjects[xCoord, yCoord] = true;
            StartPrefab = (GameObject)Resources.Load("Prefabs/Start");
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(start);
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
