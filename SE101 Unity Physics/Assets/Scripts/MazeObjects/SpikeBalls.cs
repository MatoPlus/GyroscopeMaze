using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{
    class SpikeBalls : Feature
    {
        public Vector3 Origin { get; private set; }
        public GameObject MazeObject { get; private set; }
        public GameObject SpikeBallPrefab { get; private set; }
        private List<FeatureObject> spikeBalls;


        public SpikeBalls(Maze maze, bool [,] uniqueObjects) : base(uniqueObjects)
        {
            Origin = maze.Origin;
            MazeObject = maze.gameObject;
            SpikeBallPrefab = (GameObject)Resources.Load("Prefabs/SpikeBall");

        }

        // Use this for initialization
        public void Initialize()
        {
            int a = Director.Difficulty;
        }

        public override void Build()
        {
            foreach (FeatureObject spikeBall in spikeBalls)
            {
                spikeBall.Build();
            }
        }

        public override void Update()
        {
            return;
        }
    }
}