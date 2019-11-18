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
        private List<SpikeBall> spikeBalls;
        public Maze maze;
        private float counterMax;
        private float counter;
        private float spikeBallTimer;

        public SpikeBalls(Maze maze, bool [,] uniqueObjects) : base(uniqueObjects)
        {
            spikeBalls = new List<SpikeBall>();
            this.maze = maze;
            Origin = maze.Origin;
            MazeObject = maze.gameObject;
            SpikeBallPrefab = (GameObject)Resources.Load("Prefabs/SpikeBall");
            this.Initialize();
        }

        // Use this for initialization
        public void Initialize()
        {
            //Time between spawns
            counterMax = 1 - (Director.Difficulty)/100;
            counter = 0;
            //Time for a ball to despawn
            spikeBallTimer = 2 + Director.Difficulty/100;
        }

        public override void Update()
        {
            counter += Time.deltaTime;
            if (counter >= counterMax)
            {
                //Chance of spawning
                if (Random.value < 0.25)
                {
<<<<<<< HEAD
                    SpikeBall spikeBall = new SpikeBall(Random.Range(0, (int)(this.maze.Width)), Random.Range(0, (int)this.maze.Height), SpikeBallPrefab, spikeBallTimer, Origin);
=======
                    SpikeBall spikeBall = new SpikeBall(
                        Random.Range(
                            (int)Origin.x, 
                            (int)(Origin.x + this.maze.Width)), 
                        Random.Range(
                            (int)Origin.z, 
                            (int)(Origin.z + this.maze.Height)), 
                        SpikeBallPrefab, 
                        spikeBallTimer, 
                        this.spikeBalls);

>>>>>>> a7e000b168209bf61bd0e6aa359e8614d8be3efa
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