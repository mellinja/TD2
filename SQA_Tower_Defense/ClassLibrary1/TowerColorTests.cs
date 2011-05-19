using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQA_Tower_Defense;
using Microsoft.Xna.Framework;

namespace ClassTests
{
    [TestFixture()]
    class TowerColorTests
    {
        [Test()]
        //Tests that normal towers do normal damage
        public void NormalTowerTest()
        {
            Map map = new Map("normal", 100000, 2);
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
            Assert.AreEqual(Color.White, t.Color);
        }
        [Test()]
        //Slow towers cause enemies to move every other cycle, only effects enemies in range, all enemies
        public void SlowTowerTest()
        {
            Map map = new Map("normal", 100000, 2);
            Tower t = new Tower("slow", 10, 10, 10, 1, new Rectangle(70, 70, 1, 1));
            Enemy e1 = new Enemy(100, 1, "basic", 100, new Rectangle(65, 65, 1, 1));
            Enemy e2 = new Enemy(100, 1, "basic", 100, new Rectangle(75, 75, 1, 1));
            Enemy e3 = new Enemy(100, 1, "basic", 100, new Rectangle(105, 105, 1, 1));
            map.PlaceTower(t);
            map.SpawnEnemy(e1);
            map.SpawnEnemy(e2);
            map.SpawnEnemy(e3);
            t.updateCounter = t.UpdateMax - 1;
            //e1.Counter = 1;
            //e2.Counter = 1;
            //e3.Counter = 1;
            //map.Update();
            t.Update();
            Assert.AreEqual(new Rectangle(65, 65, 1, 1), e1.Location);

            Assert.AreEqual(new Rectangle(75, 75, 1, 1), e2.Location);

          //  Assert.AreEqual(new Rectangle(106, 106, 1, 1), e3.Location);
            map.Update();
            Assert.AreEqual(new Rectangle(66, 66, 1, 1), e1.Location);

            Assert.AreEqual(new Rectangle(76, 76, 1, 1), e2.Location);

            Assert.AreEqual(new Rectangle(107, 107, 1, 1), e3.Location);
            Assert.AreEqual(t.Color, Color.Blue);

        }
        //Red towers give enemy damage over time (DoT), 1 damage per update, goes out after 200 updates;
        [Test()]
        public void RedTowerTest()
        {
            Map map = new Map("normal", 100000, 2);
            Tower t = new Tower("dot", 10, 10, 10, 30, new Rectangle(70, 70, 1, 1));
            Enemy e1 = new Enemy(30000, 1, "basic", 100, new Rectangle(65, 65, 1, 1));
            map.PlaceTower(t);
            map.SpawnEnemy(e1);
            t.updateCounter = t.UpdateMax - 1;
            map.Update();
            t.Update();
            Assert.True(e1.BurnStop > e1.Counter);
            Assert.AreEqual(30000f, e1.Health);
            map.Update();
            e1.Update();
            Assert.AreEqual(29999f, e1.Health);
            for (int i = 0; i < 200; i++)
                e1.Update();

            Assert.False(e1.BurnStop > e1.Counter);

            Assert.AreEqual(t.Color, Color.Red);
        }
    }
}
