using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQA_Tower_Defense;
using NUnit.Framework;
namespace TestProject
{
    [TestFixture()]
    class TowerTest
    {
        

        [Test()]
        public void testTowerInit()
        {
            Tower tower = new Tower("Tower",1,1,1,1);
            Assert.IsNotNull(tower);
        }

        [Test()]
        public void testTowerGetsHealth()
        {
            Tower tower = new Tower();
            tower.setHealth = 10;
            Assert.AreEqual(tower.Health,10);

        }

        [Test()]
        public void testTowerGetsRange()
        {
            Tower tower= new Tower();
            t.setRange = 10;
            Assert.AreEqual(t.Range, 10);

        }

        [Test()]
        public void testTowerGetsCost()
        {
            Tower tower= new Tower();
            t.setCost = 10;
            Assert.AreEqual(t.Cost, 10);

        }

        [Test()]
        public void testTowerGetsAttackDamage()
        {
            Tower tower= new Tower();
            t.setAttackDamage = 10;
            Assert.AreEqual(t.AttackDamage, 10);

        }

        [Test()]
        public void testTowerGetsEnemy()
            Tower tower= new Tower();
            

    }
}

    }
}
