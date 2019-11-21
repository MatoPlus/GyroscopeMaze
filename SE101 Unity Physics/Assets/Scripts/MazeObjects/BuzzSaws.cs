using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{
    class BuzzSaws : Feature
    {
        public GameObject MazeObject { get; private set; }
        public GameObject BuzzSawPrefab { get; private set; }
        public GameObject WarningPadPrefab { get; private set; }
        private List<BuzzSaw> buzzSaws;
        private float counterMax;
        private float counter;
        private float buzzSawTimer;
        private float spawnChance;

        public BuzzSaws(Maze maze, Vector3 Origin, bool[,] uniqueObjects) : base(maze, Origin, uniqueObjects)
        {
            buzzSaws = new List<BuzzSaw>();
            this.maze = maze;
            MazeObject = maze.gameObject;
            BuzzSawPrefab = (GameObject)Resources.Load("Prefabs/Maze/BuzzSaw");
            WarningPadPrefab = (GameObject)Resources.Load("Prefabs/Maze/WarningPad");
            Initialize();
        }

        // Use this for initialization
        public void Initialize()
        {
            //Time between spawns
            counterMax = 2.5f - (Director.Difficulty) / 100f;
            counter = 0;
            //Time for a ball to despawn
            buzzSawTimer = 5 + Director.Difficulty / 100f;
            spawnChance = Director.Difficulty / 100f;
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
                    BuzzSaw buzzSaw = new BuzzSaw((int)coords.x, (int)coords.y, BuzzSawPrefab, WarningPadPrefab, buzzSawTimer, maze);
                    buzzSaws.Add(buzzSaw);
                }
                counter = 0;
            }
            for (int i = buzzSaws.Count - 1; i >= 0; --i)
            {
                if (buzzSaws[i].Dead)
                {
                    buzzSaws.RemoveAt(i);
                    continue;
                }
                buzzSaws[i].Update();
            }
        }

        public override void Build()
        {

        }
    }
}