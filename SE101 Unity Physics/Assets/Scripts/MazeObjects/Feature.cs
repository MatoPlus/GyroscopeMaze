using System;
using UnityEngine;

namespace MazeObjects
{
	public abstract class Feature
	{
        protected Maze maze;
        protected Vector3 Origin;
		protected bool [,] uniqueObjects;

        public Feature(Maze maze, Vector3 Origin)
        {
            this.maze = maze;
            this.Origin = Origin;
        }

        public Feature(Maze maze, Vector3 Origin, bool [,] uniqueObjects)
		{
			this.uniqueObjects = uniqueObjects;
            this.maze = maze;
            this.Origin = Origin;
        }

        public Vector2 getValidLocation()
        {
            int posX;
            int posY;
            bool objNearby;
            do
            {
                objNearby = false;
                posX = UnityEngine.Random.Range(0, (int)(maze.Width));
                posY = UnityEngine.Random.Range(0, (int)(maze.Height));
                Collider[] nearbyObjects = Physics.OverlapSphere(new Vector3(Origin.x + posX, 0.5f, Origin.z + posY), 1);
                foreach (Collider i in nearbyObjects)
                {
                    if (i.tag == "UniqueObject" || i.tag == "Player")
                    {
                        objNearby = true;
                        break;
                    }
                }
            } while (objNearby);
            return new Vector2(posX, posY);
        }
        public abstract void Build();

        public abstract void Update();
	}
}