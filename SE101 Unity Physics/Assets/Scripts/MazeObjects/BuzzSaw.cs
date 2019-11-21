using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{

    class BuzzSaw : FeatureObject
    {

        private bool active;
        GameObject buzzSaw;
        GameObject warningPad;
        GameObject buzzSawPrefab;
        private float timerMax;
        private float timer;
        private float warningTimer;
        Maze maze;

        public BuzzSaw(int xCoord, int yCoord, GameObject buzzSawPrefab, GameObject warningPadPrefab, float timerMax, Maze maze) : base(xCoord, yCoord)
        {
            this.maze = maze;
            active = false;
            this.timerMax = timerMax;
            this.buzzSawPrefab = buzzSawPrefab;
            warningPad = Object.Instantiate(warningPadPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.1f), Quaternion.Euler(90, 0, 0));
            warningTimer = 2;
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(buzzSaw);
            Dead = true;
            //AllSpikeBalls.Remove(this);
        }

        // Builds object in unity
        public override void Build()
        {
            buzzSaw = Object.Instantiate(buzzSawPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.5f), Quaternion.identity);
            buzzSaw.GetComponent<TriggerObject>().OnCollisionWithBall.AddListener(maze.KillBall);
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
