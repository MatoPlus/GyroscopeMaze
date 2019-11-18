using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{
    class KeyPoints : Feature
    {
        public Vector3 Origin { get; private set; }
        public GameObject MazeObject { get; private set; }
        public GameObject StartBallPrefab { get; private set; }
        public GameObject EndBallPrefab { get; private set; }
        public bool[,] UniqueObjects { get; private set; }
        private Start start;
        private End end;
        public Maze maze;

        public KeyPoints(Maze maze, int startX, int startY, int endX, int endY, bool[,] uniqueObjects) : base(uniqueObjects)
        {
            this.maze = maze;
            Origin = maze.Origin;
            UniqueObjects = uniqueObjects;
            // TODO: Implment sanity check for based placements.
            UniqueObjects[startX, startY] = true;
            UniqueObjects[endY, endY] = true;
            MazeObject = maze.gameObject;
            StartBallPrefab = (GameObject)Resources.Load("Prefabs/Start");
            EndBallPrefab = (GameObject)Resources.Load("Prefabs/End");
            start = new Start(startX+(int)Origin.x, startY+(int)Origin.z);
            end = new End(endX + (int)Origin.x, endY+(int)Origin.z);
        }

        public override void Build()
        {
            /*foreach (FeatureObject spikeBall in spikeBalls)
            {
                spikeBall.Build();
            }*/
        }

        public override void Update()
        {
        }
    }
}