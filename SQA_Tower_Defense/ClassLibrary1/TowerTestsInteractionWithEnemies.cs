using System;
using System.Collections.Generic;
using SQA_Tower_Defense;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace ClassTests
{
    class TowerTestsInteractionWithEnemies
    {


        //Testing that if there are two towers and two enemies, each tower only attacks the one closest to it. 
        [Test()]
        public void testTwoTowerswithTwoEnemies()
        {
            Tower t1 = new Tower("tower", 10, 1, 10, 20, new Rectangle(10, 10, 1, 1));
            Tower t2 = new Tower("tower", 10, 10, 10, 20, new Rectangle(20, 20, 1, 1));
            Enemy e1 = new Enemy(20, 1.0, "basic", 1, new Rectangle(11, 11, 1, 1)); // In range of tower and tower 2, closer to tower 1
            Enemy e2 = new Enemy(20, 1.0, "basic", 1, new Rectangle(19, 19, 1, 1)); // In range of tower and tower 2, closer to tower 2

            Map m = new Map("normal", 100, 1);
            m.PlaceTower(t1);
            m.PlaceTower(t2);
            m.SpawnEnemy(e1);
            m.SpawnEnemy(e2);
            m.Update();//adds all the approiate enemies to the towers lists
            //Assert.IsNull(t1.getCurrentTarget());
            t1.updateCounter = 59;
            t2.updateCounter = 59;
            t1.Update();
            Assert.AreEqual(2, t1.Enemies.Count);
            Assert.AreEqual(2, t2.Enemies.Count);
            Assert.AreEqual(1, t1.AttackDamage);
           // e1.Health -= 19;
            t2.Update();

            //Assert.AreEqual(19, e1.Health);
            Assert.AreEqual(e2.Health, 10);
        }


        //Testing towers change targets to the closest enemy if another enemy moves closer. 
        [Test()]
        public void testTowerChangesTarget()
        {
			Tower t1 = new Tower("tower", 10, 1, 10, 100, new Rectangle(0, 0, 1, 11));
			Enemy e1 = new Enemy(20, 1.0, "basic", 1, new Rectangle(30, 30, 1, 1)); 
			Enemy e2 = new Enemy(20, 1.0, "basic", 1, new Rectangle(110, 110, 1, 1)); 
			
			Map m = new Map("normal", 100, 1);
			m.PlaceTower(t1);
			m.SpawnEnemy(e1);
			m.SpawnEnemy(e2);
			m.Update();//adds all the approiate enemies to the towers lists
			t1.Update();
			Assert.AreEqual(e1, t1.getCurrentTarget());

			e2.moveTo(25,25);
            m.Update();
			Assert.AreEqual(e2, t1.getCurrentTarget());
			
			
        }


        //Testing tower updates enemy list, even with no enemies in is range. 
        [Test()]
        public void testTowerUpdatesEnemiesList()
        {
            Tower t1 = new Tower("tower", 10, 1, 10, 10, new Rectangle(0, 0, 10, 10));
            Enemy e1 = new Enemy(20, 1.0, "basic", 1, new Rectangle(150, 150, 155, 155)); //Out of t1's range
            Map m = new Map("normal", 100, 1);
            m.PlaceTower(t1);
            m.SpawnEnemy(e1);
            m.Update();//adds all the approiate enemies to the towers lists
            t1.Update();
            //Assert.AreEqual(e1, t1.getEnemyList.get(0));
            Assert.AreEqual(0, t1.Enemies.Count);
        }






    }
}

