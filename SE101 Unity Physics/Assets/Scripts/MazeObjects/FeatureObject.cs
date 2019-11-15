using System;
using UnityEngine;

namespace MazeObjects
{
    public abstract class FeatureObject
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Type { get; set; }
        public bool Dead { get; set; }

        public FeatureObject(int xCoord, int yCoord, int type)
        {
            X = xCoord;
            Y = yCoord;
            Dead = false;
            Type = type;
        }

        // Play corresponding interaction sound affect 
        public void PlaySound()
        {
            return;
        }
        
        // Kill object from game
        public abstract void KillObject();

        // Builds object in unity
        public abstract void Build();

        // Update is called once per frame
        public abstract void Update();
    }
}
