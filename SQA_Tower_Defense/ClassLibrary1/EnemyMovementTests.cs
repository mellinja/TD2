using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQA_Tower_Defense;
using Microsoft.Xna.Framework;
namespace ClassTests
{
    class EnemyMovementTests
    {


        Map map;
        Rectangle rec;
        Tower tower;
        Enemy enemy;
        Wave wave;


        [SetUp()]
        public void SetUp()
        {


            map = new Map("normal", 100000, 1);
            rec = new Rectangle(0, 0, 5, 5);
            tower = new Tower("basic", 10, 20, 30, 40, new Rectangle(1, 0, 1, 1));
            enemy = new Enemy(10, 1.0f, "basic", 20, new Rectangle(rec.X + 5, rec.Y + 5, rec.Width, rec.Height));

            wave = new Wave(new Enemy(10, 1.0f, "basic", 20, new Rectangle(rec.X + 5, rec.Y + 5, rec.Width, rec.Height)), 5);
            map.newWave(wave);
            map.PlaceTower(tower);
        }



        [Test()]
        //Map class spawns enemys every 5 updates
        public void mapSpawnsEnemys()
        {

            for (int i = 0; i < 49; i++)
                map.Update();
            Assert.AreEqual(0, map.enemiesOnMap.Count());//check to see if map has no enemys
            map.Update();//5
            Assert.AreEqual(1, map.enemiesOnMap.Count());//check to see if map has 1 enemy

        }


        [Test()]
        //Map class spawns enemys every 5 updates,enemies move every update.
        public void mapUpdatesMoveSingleEnemy()
        {

            for (int i = 0; i < 50; i++)
            {
                map.Update();
            }
            map.Update();//51, moves enemy
            Rectangle newLocation = new Rectangle(6, 6, 5, 5);
            Assert.AreEqual(map.enemiesOnMap[0].Location, newLocation);

        }

        [Test()]
        //Map class spawns enemys every 5 updates,enemies move every update. Testing that all enemies move
        public void mapUpdatesMoveAllEnemy()
        {

            for (int i = 0; i < 50; i++)
            {
                map.Update();
            }
            map.SpawnEnemy(new Enemy(10, 1.0f, "basic", 20, new Rectangle(10, 10, 1, 1)));
            map.Update();//6, moves enemies
            Rectangle newLocation = new Rectangle(6, 6, 5, 5);
            Assert.AreEqual(map.enemiesOnMap[0].Location, newLocation);
            Assert.AreEqual(map.enemiesOnMap[1].Location, new Rectangle(11, 11, 1, 1));
        }
        [Test()]
        //Map class spawns enemys every 5 updates,enemies move every update, and adds enemies to towers attack list.
        public void mapUpdatesAddsEnemiesToTowerLists()
        {
            for (int i = 0; i < 50; i++)
            {
                map.Update();
            }
            map.Update();//6, moves enemy
            Assert.AreEqual(tower.getCurrentTarget().Location.X, rec.X + 6);
            Assert.AreEqual(tower.getCurrentTarget().Location.Y, rec.Y + 6);
        }


        [Test()]
        //Testing to see if enemies that move into range get damaged
        public void mapUpdatesDamageEnemiesWithTowers()
        {
            Tower t = new Tower("basic", 10, 10, 10, 500, new Rectangle(70, 70, 1, 1));
            Enemy special = new Enemy(20, 1.0f, "basic", 20, new Rectangle(1, 1, 1, 1));
            map.SpawnEnemy(special);
            map.SellTower(t);
            map.PlaceTower(t);
            for (int i = 0; i <= 60; i++)
            {
                map.Update();
                t.Update();
            }
            Assert.AreEqual(special, t.getCurrentTarget());//Special should be the closest to the tower
            Assert.AreEqual(10, t.getCurrentTarget().Health);
        }







    }
}

