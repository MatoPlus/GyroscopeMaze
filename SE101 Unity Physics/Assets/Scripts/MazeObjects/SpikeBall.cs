using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{

    class SpikeBall : FeatureObject
    {

        GameObject spikeBall;
        GameObject spikeBallPrefab;
        private Vector3 Origin;
        private float timerMax;
        private float timer;
        Maze maze;

        public SpikeBall(int xCoord, int yCoord, GameObject spikeBallPrefab, float timerMax, Vector3 Origin) : base(xCoord, yCoord)
        {
            this.Origin = Origin;
            this.timerMax = timerMax;
            this.spikeBallPrefab = spikeBallPrefab;
            Build();
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
            this.spikeBall = Object.Instantiate(spikeBallPrefab, new Vector3(Origin.x + X + 0.5f, 0.5f, Origin.z + Y + 0.5f), Quaternion.identity);
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
