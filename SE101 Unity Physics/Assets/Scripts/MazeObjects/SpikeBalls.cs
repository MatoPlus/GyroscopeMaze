using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        List<FeatureObject> spikeBalls;


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