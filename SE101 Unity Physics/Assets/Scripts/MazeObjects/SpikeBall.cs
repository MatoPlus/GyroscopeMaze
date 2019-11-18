using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{

    class SpikeBall : FeatureObject
    {

        GameObject spikeBall;
        GameObject spikeBallPrefab;
        private float timerMax;
        private float timer;
        private List<SpikeBall> AllSpikeBalls;
        Maze maze;

        public SpikeBall(int xCoord, int yCoord, GameObject spikeBallPrefab, float timerMax, List<SpikeBall> AllSpikeBalls) : base(xCoord, yCoord)
        {
            this.AllSpikeBalls = AllSpikeBalls;
            this.X = xCoord;
            this.Y = yCoord;
            this.timerMax = timerMax;
            this.spikeBallPrefab = spikeBallPrefab;
            this.Build();
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
            this.spikeBall = Object.Instantiate(spikeBallPrefab, new Vector3(X + 0.5f, 0.5f, Y + 0.5f), Quaternion.identity);
            this.timer = timerMax;
        }

        // Update is called once per frame
        public override void Update()
        {
            this.timer -= Time.deltaTime;
            if (this.timer <= 0)
            {
                this.KillObject();
            }
        }
    }
}
