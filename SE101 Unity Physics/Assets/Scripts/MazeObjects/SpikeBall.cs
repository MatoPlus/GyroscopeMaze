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
        private float timerMax;
        private float timer;
        private float warningTimer;
        Maze maze;

        public SpikeBall(int xCoord, int yCoord, GameObject spikeBallPrefab, GameObject warningPadPrefab, float timerMax, Maze maze) : base(xCoord, yCoord)
        {
            this.maze = maze;
            active = false;
            this.timerMax = timerMax;
            this.spikeBallPrefab = spikeBallPrefab;
            warningPad = Object.Instantiate(warningPadPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.1f), Quaternion.Euler(90, 0, 0));
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
            spikeBall = Object.Instantiate(spikeBallPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.5f), Quaternion.identity);
            spikeBall.GetComponent<TriggerObject>().OnCollisionWithBall.AddListener(maze.KillBall);
            timer = timerMax;
        }

        // Update is called once per frame
        public override void Update()
        {
            warningTimer -= Time.deltaTime;
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
