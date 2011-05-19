using System;
using System.Collections.Generic;
using SQA_Tower_Defense;
using NUnit.Framework;


namespace ClassTests
{
    [TestFixture()]
    public class TowerTests
    {
        private Tower tower;

        #region Initializates
        
        
        [Test()]
        public void testInit()
        {
            tower = new Tower("", 0, 0, 0, 0);
            Assert.IsNotNull(tower);
        }
        
        #endregion

        #region Testing Health
        
        [Test()]
        public void towerHasCorrectHealthWhenInitialized()
        {
            tower = new Tower("", 10, 20,30,40);
            Assert.AreEqual(tower.Health,10);

        }
        
        [Test()]
        public void towerHasCorrectHealthAfterModified()
        {
            tower = new Tower("", 10, 20,30,40);
            tower.Health -= 1;
            Assert.AreEqual(tower.Health,9);
        }
        
        #endregion

        #region Testing Range
        
        [Test()]
        public void towerHasCorrectRangeWhenInitialized()
        {
            tower= new Tower("",10, 20, 30, 40);
            Assert.AreEqual(tower.Range, 40);
        }

        #endregion

        #region Testing Cost
        
        [Test()]
        public void towerHasCorrectCostWhenInitialized()
        {
            tower= new Tower("",10,20,30,40);
            Assert.AreEqual(tower.Cost, 30);

        }

        #endregion

        #region Testing Damage
        
        [Test()]
        public void towerHasCorrectAttackDamageWhenInitialized()
        {
             tower= new Tower("",10,20,30,40);
            Assert.AreEqual(tower.AttackDamage, 20);
        }
        
        #endregion

        #region Enemy Interaction

        [Test()]
        public void towerGetsEnemy()
        {
            Enemy enemy = new Enemy(0, 0.0f, "", 0);
            tower = new Tower("", 0, 0, 0, 0);
            tower.AddNearbyEnemy(enemy);
            Assert.AreSame(enemy, tower.Enemies(0));
        }

        #endregion 

        #region Miscellaneous



        #endregion
    }
}
