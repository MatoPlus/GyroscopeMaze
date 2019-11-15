using UnityEngine;

namespace MazeObjects
{
    enum WallDirection
    {
        Vertical, Horizontal
    }

    class Wall : FeatureObject
    {

        GameObject wall;
        GameObject wallPrefab;
        WallDirection direction;
        Maze maze;

        public Wall(int xCoord, int yCoord, int type, WallDirection direction, GameObject wallPrefab) : base(xCoord, yCoord, type)
        {
            this.direction = direction;
            this.wallPrefab = wallPrefab;
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(wall);
        }

        // Builds object in unity
        public override void Build()
        {
            return;

        }

        // Update is called once per frame
        public override void Update()
        {
            return;
        }
    }
}
