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
            Initialize();
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
                    int posX;
                    int posY;
                    bool ballNearby;
                    do {
                        ballNearby = false;
                        posX = Random.Range(0, (int)(this.maze.Width));
                        posY = Random.Range(0, (int)this.maze.Height);
                        Collider[] nearbyBalls = Physics.OverlapSphere(new Vector3(Origin.x + posX, 0.5f, Origin.z + posY), 1.5f);
                        foreach (Collider i in nearbyBalls)
                        {
                            if (i.name == "SpikeBall(Clone)")
                            {
                                ballNearby = true;
                                break;
                            }
                        }
                    } while (ballNearby);
                    SpikeBall spikeBall = new SpikeBall(posX, posY, SpikeBallPrefab, spikeBallTimer, Origin);
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