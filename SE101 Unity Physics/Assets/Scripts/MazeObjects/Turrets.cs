using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{
    class Turrets : Feature
    {
        public GameObject MazeObject { get; private set; }
        public GameObject TurretPrefab { get; private set; }
        public GameObject BulletPrefab { get; private set; }
        private List<Turret> turrets;
        private float counterMax;
        private float counter;
        private float turretTimer;
        private float spawnChance;

        public Turrets(Maze maze, Vector3 Origin, bool[,] uniqueObjects) : base(maze, Origin, uniqueObjects)
        {
            turrets = new List<Turret>();
            this.maze = maze;
            MazeObject = maze.gameObject;
            TurretPrefab = (GameObject)Resources.Load("Prefabs/Maze/Turret");
            BulletPrefab = (GameObject)Resources.Load("Prefabs/Maze/Bullet");
            Initialize();
        }

        // Use this for initialization
        public void Initialize()
        {
            //Time between spawns
            counterMax = 2.5f - (Director.Difficulty) / 100f;
            counter = 0;
            //Time for a ball to despawn
            turretTimer = 5 + Director.Difficulty / 100f;
            spawnChance = Director.Difficulty / 100f;
        }

        public override void Update()
        {
            counter += Time.deltaTime;
            if (counter >= counterMax)
            {
                //Chance of spawning
                if (Random.value <= spawnChance)
                {
                    Vector2 coords = getValidLocation(1);
                    Turret turret = new Turret((int)coords.x, (int)coords.y, TurretPrefab, BulletPrefab, turretTimer, maze);
                    turrets.Add(turret);
                }
                counter = 0;
            }
            for (int i = turrets.Count - 1; i >= 0; --i)
            {
                if (turrets[i].Dead)
                {
                    turrets.RemoveAt(i);
                    continue;
                }
                turrets[i].Update();
            }
        }

        public override void Build()
        {

        }
    }
}