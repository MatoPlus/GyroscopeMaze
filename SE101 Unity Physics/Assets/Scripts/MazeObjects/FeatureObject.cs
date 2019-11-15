using System;
using UnityEngine;

namespace MazeObjects
{
    public abstract class FeatureObject
    {
        private int xCoord;
        private int yCoord;
        private int type;
        private bool isDead;

        public Feature(int xCoord, int yCoord, int type)
        {
            X = xCoord;
            Y = yCoord;
            Dead = false;
            Type = type;
        }

        public int X
        {
            get
            {
                return xCoord;
            }

            set
            {
                xCoord = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public int Y
        {
            get
            {
                return yCoord;
            }

            set
            {
                yCoord = value;
            }
        }

        public bool Dead
        {
            get
            {
                return isDead;
            }

            set
            {
                isDead = value;
            }
        }

        // Play corresponding interaction sound affect 
        public abstract void PlaySound();
        
        // Kill object from game
        public abstract void KillObject();

        // Builds object in unity
        public abstract void Build();

        // Update is called once per frame
        public abstract void Update();
    }
}
