using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{

    class Turret : FeatureObject
    {

        private bool active;
        GameObject turret, bullet1, bullet2, bullet3, bullet4;
        int bullets_killed = 0;
        GameObject turretPrefab;
        GameObject bulletPrefab;
        private float timerMax;
        private float timer;
        private float warningTimer;
        Maze maze;

        public Turret(int xCoord, int yCoord, GameObject turretPrefab, GameObject bulletPrefab, float timerMax, Maze maze) : base(xCoord, yCoord)
        {
            this.maze = maze;
            active = false;
            this.timerMax = timerMax;
            this.turretPrefab = turretPrefab;
            this.bulletPrefab = bulletPrefab;
            turret = Object.Instantiate(turretPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.5f), Quaternion.Euler(90, 0, 0));
            warningTimer = 2;
        }

        // Kill object from game
        public override void KillObject()
        {
            Object.Destroy(turret);
            Dead = true;
        }

        public void KillBullet1()
        {
            Object.Destroy(bullet1);
            bullets_killed++;
            KillIfBulletsDead();
        }

        public void KillBullet2()
        {
            Object.Destroy(bullet2);
            bullets_killed++;
            KillIfBulletsDead();
        }

        public void KillBullet3()
        {
            Object.Destroy(bullet3);
            bullets_killed++;
            KillIfBulletsDead();
        }

        public void KillBullet4()
        {
            Object.Destroy(bullet4);
            bullets_killed++;
            KillIfBulletsDead();
        }

        private void KillIfBulletsDead()
        {
            if (bullets_killed > 4) throw new System.Exception();
            if (bullets_killed == 4) KillObject();
        }

        // Builds object in unity
        public override void Build()
        {
            bullet1 = Object.Instantiate(bulletPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.5f), Quaternion.identity);
            bullet2 = Object.Instantiate(bulletPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.5f), Quaternion.identity);
            bullet3 = Object.Instantiate(bulletPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.5f), Quaternion.identity);
            bullet4 = Object.Instantiate(bulletPrefab, maze.CoordsToPosition(X, Y, 0.5f, 0.5f, 0.5f), Quaternion.identity);

            bullet1.GetComponent<TriggerObject>().OnCollisionWithBall.AddListener(maze.KillBall);
            bullet2.GetComponent<TriggerObject>().OnCollisionWithBall.AddListener(maze.KillBall);
            bullet3.GetComponent<TriggerObject>().OnCollisionWithBall.AddListener(maze.KillBall);
            bullet4.GetComponent<TriggerObject>().OnCollisionWithBall.AddListener(maze.KillBall);

            bullet1.GetComponent<TriggerObject>().CheckTag = "Wall";
            bullet2.GetComponent<TriggerObject>().CheckTag = "Wall";
            bullet3.GetComponent<TriggerObject>().CheckTag = "Wall";
            bullet4.GetComponent<TriggerObject>().CheckTag = "Wall";

            bullet1.GetComponent<TriggerObject>().OnCollisionWithTag.AddListener(KillBullet1);
            bullet2.GetComponent<TriggerObject>().OnCollisionWithTag.AddListener(KillBullet2);
            bullet3.GetComponent<TriggerObject>().OnCollisionWithTag.AddListener(KillBullet3);
            bullet4.GetComponent<TriggerObject>().OnCollisionWithTag.AddListener(KillBullet4);

            bullet1.GetComponent<Bullet>().MoveDirection = new Vector3(0.01f, 0, 0);
            bullet2.GetComponent<Bullet>().MoveDirection = new Vector3(0, 0, 0.01f);
            bullet3.GetComponent<Bullet>().MoveDirection = new Vector3(-0.01f, 0, 0);
            bullet4.GetComponent<Bullet>().MoveDirection = new Vector3(0, 0, -0.01f);

            timer = timerMax;
        }

        // Update is called once per frame
        public override void Update()
        {
            warningTimer -= Time.deltaTime;
            if (!active && warningTimer <= 0)
            {
                active = true;
                Build();
            }
            if (active)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (!Dead)
                    {
                        KillObject();
                    }
                }
            }
        }
    }
}
