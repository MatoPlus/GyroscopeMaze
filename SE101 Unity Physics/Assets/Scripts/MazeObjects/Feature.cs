using System;
using UnityEngine;

namespace MazeObjects
{
	public abstract class Feature 
	{
		protected bool uniqueObjects[,];

        Feature(bool uniqueObjects[,])
		{
			this.uniqueObjects = uniqueObjects;
        }

        void abstract public Build()

        void abstract public Update()
	}
}