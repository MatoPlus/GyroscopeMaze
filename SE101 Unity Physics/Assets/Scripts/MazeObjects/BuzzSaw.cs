using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{

    class BuzzSaw : FeatureObject
    {

        GameObject buzzSaw;
        GameObject buzzSawPrefab;
        Vector3 Origin;
        private float timerMax;
        private float timer;
        Maze maze;

        public BuzzSaw(int xCoord, int yCoord, GameObject buzzSawPrefab, float timerMax, Vector3 Origin) : base(xCoord, yCoord)
        {
            this.Origin = Origin;
            this.timerMax = timerMax;
            this.buzzSawPrefab = buzzSawPrefab;
            Build();
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(buzzSaw);
            Dead = true;
            //AllBuzzSaws.Remove(this);
        }

        // Builds object in unity
        public override void Build()
        {
            this.buzzSaw = Object.Instantiate(buzzSawPrefab, new Vector3(Origin.x + X + 0.5f, 0.5f, Origin.z + Y + 0.5f), Quaternion.identity);
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
