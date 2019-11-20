using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{

    class SpikeBall : FeatureObject
    {
        private bool active;
        GameObject spikeBall;
        GameObject warningPad;
        GameObject spikeBallPrefab; 
        private Vector3 Origin;
        private float timerMax;
        private float timer;
        private float warningTimer;
        Maze maze;

        public SpikeBall(int xCoord, int yCoord, GameObject spikeBallPrefab, GameObject warningPadPrefab, float timerMax, Vector3 Origin, Maze maze) : base(xCoord, yCoord)
        {
            this.maze = maze;
            active = false;
            this.Origin = Origin;
            this.timerMax = timerMax;
            this.spikeBallPrefab = spikeBallPrefab;
            warningPad = Object.Instantiate(warningPadPrefab, new Vector3(Origin.x + X + 0.5f, 0.1f, Origin.z + Y + 0.5f), Quaternion.Euler(90, 0, 0));
            warningTimer = 2;
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(spikeBall);
            Dead = true;
            //AllSpikeBalls.Remove(this);
        }

        // Builds object in unity
        public override void Build()
        {
            spikeBall = Object.Instantiate(spikeBallPrefab, new Vector3(Origin.x + X + 0.5f, 0.5f, Origin.z + Y + 0.5f), Quaternion.identity);
            spikeBall.GetComponent<LethalObject>().OnCollisionWithBall.AddListener(maze.KillBall);
            timer = timerMax;
        }

        // Update is called once per frame
        public override void Update()
        {
            warningTimer -= Time.deltaTime;
            // Debug.Log(warningTimer);
            if (!active && warningTimer <= 0)
            {
                Object.Destroy(warningPad);
                active = true;
                Build();
            }
            if (active)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    KillObject();
                }
            }
        }
    }
}
