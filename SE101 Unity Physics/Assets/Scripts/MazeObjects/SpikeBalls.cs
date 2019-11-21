using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{
    class SpikeBalls : Feature
    {
        public GameObject MazeObject { get; private set; }
        public GameObject SpikeBallPrefab { get; private set; }
        public GameObject WarningPadPrefab { get; private set; }
        private List<SpikeBall> spikeBalls;
        private float counterMax;
        private float counter;
        private float spikeBallTimer;
        private float spawnChance;

        public SpikeBalls(Maze maze, Vector3 Origin, bool [,] uniqueObjects) : base(maze, Origin, uniqueObjects)
        {
            spikeBalls = new List<SpikeBall>();
            this.maze = maze;
            MazeObject = maze.gameObject;
            SpikeBallPrefab = (GameObject)Resources.Load("Prefabs/Maze/SpikeBall");
            WarningPadPrefab = (GameObject)Resources.Load("Prefabs/Maze/WarningPad");
            Initialize();
        }

        // Use this for initialization
        public void Initialize()
        {
            //Time between spawns
            counterMax = 2.5f - (Director.Difficulty)/100f;
            counter = 0;
            //Time for a ball to despawn
            spikeBallTimer = 5 + Director.Difficulty/100f;
            spawnChance = Director.Difficulty/100f;
        }

        public override void Update()
        {
            counter += Time.deltaTime;
            if (counter >= counterMax)
            {
                //Chance of spawning
                if (Random.value <= spawnChance)
                {
                    Vector2 coords = getValidLocation(1);
                    SpikeBall spikeBall = new SpikeBall((int)coords.x, (int)coords.y, SpikeBallPrefab, WarningPadPrefab, spikeBallTimer, maze);
                    spikeBalls.Add(spikeBall);
                }
                counter = 0;
            }
            for (int i = spikeBalls.Count-1; i >= 0; --i)
            {
                if (spikeBalls[i].Dead)
                {
                    spikeBalls.RemoveAt(i);
                    continue;
                }
                spikeBalls[i].Update();
            }
            /*foreach (FeatureObject spikeBall in spikeBalls)
            {
                spikeBall.Update();
            }*/
        }

        public override void Build()
        {
            /*foreach (FeatureObject spikeBall in spikeBalls)
            {
                spikeBall.Build();
            }*/
        }
    }
}