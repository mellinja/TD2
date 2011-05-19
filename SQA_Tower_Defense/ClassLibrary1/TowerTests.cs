using System;
using System.Collections.Generic;
using SQA_Tower_Defense;
using NUnit.Framework;
using Microsoft.Xna.Framework;


namespace ClassTests
{
    [TestFixture()]
    public class TowerTests
    {
        private Map map;
        private Enemy enemy;
        private Tower tower;
        private Rectangle rec;


        [SetUp()]
        public void SetUp()
        {
            map = new Map("normal", 0, 1);
            rec = new Rectangle(0, 0, 5, 5);
            tower = new Tower("", 10, 20, 30, 40, rec);
            enemy = new Enemy(10, 1.0f, "basic", 10, new Rectangle(30, 30, 50, 50));
        }
        #region Initializates
        
        
        [Test()]
        public void testInit()
        {
            tower = new Tower("", 1,10, 10, 10, rec);
            Assert.IsNotNull(tower);
        }


        //Testing that tower throws an Exception with null area
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testTowerThrowsExceptioOnNullArea()
        {
            Tower t = new Tower("tower", 10, 10, 10, 10, new Rectangle());
        }


        //Testing that tower throws an Exception with negative range
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnNegativeRange()
        {
            Tower t = new Tower("tower", 10, 10, 10, -10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that tower throws an Exception with zero range
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnZeroRange()
        {
            Tower t = new Tower("tower", 10, 10, 10, 0, new Rectangle(0, 0, 10, 10));
        }


        //Testing that tower throws an Exception with negative damage
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnNegativeDamage()
        {
            Tower t = new Tower("tower", 10, -10, 10, 10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that tower throws an Exception with zero damage
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnZeroDamage()
        {
            Tower t = new Tower("tower", 10, 0, 10, 10, new Rectangle(0, 0, 10, 10));
        }



        //Testing that tower throws an Exception with negative cost
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnNegativeCost()
        {
            Tower t = new Tower("tower", 10, 10, -10, 10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that tower throws an Exception with zero cost
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnZeroCost()
        {
            Tower t = new Tower("tower", 10, 10, 0, 10, new Rectangle(0, 0, 10, 10));
        }



        //Testing that tower throws an Exception with negative health
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnNegativeHealth()
        {
            Tower t = new Tower("tower", -10, 10, 10, 10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that tower throws an Exception with zero health
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTowerThrowsExceptioOnZeroHealth()
        {
            Tower t = new Tower("tower", 0, 10, 10, 10, new Rectangle(0, 0, 10, 10));
        }

        
        #endregion

        #region Testing Health
        
        [Test()]
        public void towerHasCorrectHealthWhenInitialized()
        {
            tower = new Tower("", 10, 20,30,40, rec);
            Assert.AreEqual(tower.Health,10);

        }
        
        [Test()]
        public void towerHasCorrectHealthAfterModified()
        {
            tower = new Tower("", 10, 20,30,40, rec);
            tower.Health -= 1;
            Assert.AreEqual(tower.Health,9);
        }
        
        #endregion

        #region Testing Range
        
        [Test()]
        public void towerHasCorrectRangeWhenInitialized()
        {
            tower= new Tower("",10, 20, 30, 40, rec);
            Assert.AreEqual(tower.Range, 40);
        }

        #endregion

        #region Testing Cost
        
        [Test()]
        public void towerHasCorrectCostWhenInitialized()
        {
            tower= new Tower("",10,20,30,40, rec );
            Assert.AreEqual(tower.Cost, 30);

        }

        #endregion

        #region Testing Damage
        
        [Test()]
        public void towerHasCorrectAttackDamageWhenInitialized()
        {
             tower= new Tower("",10,20,30,40, rec);
            Assert.AreEqual(tower.AttackDamage, 20);
        }
        
        #endregion

        #region Enemy Interaction

        [Test()]
        public void TowerAddsEnemySuccessfully()
        {

            tower.AddNearbyEnemy(enemy);
            Assert.AreEqual(enemy, tower.Enemies[0]);
        }

        #endregion 

        #region Miscellaneous



        #endregion



    }
}
