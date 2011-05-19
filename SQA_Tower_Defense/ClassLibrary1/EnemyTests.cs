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
    public class EnemyTests
    {
        private Map map;
        private Rectangle rec;
        private Tower tower;
        private Enemy enemy;

        [SetUp()]
        public void SetUp()
        {
            map = new Map("normal", 0, 1);
            rec = new Rectangle(0, 0, 5, 5);
            tower = new Tower("basic", 10, 20, 30, 40, rec);
            enemy = new Enemy(10, 1.0f, "basic", 20, rec);
        }
        #region Initialization

        [Test()]
        public void EnemyInitializesSuccessfully()
        {
            enemy = new Enemy(10, 1.0f, "basic", 20, rec);
            Assert.IsNotNull(enemy);
        }



        //Testing that enemy throws an Exception with negative health
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testEnemyThrowsExceptioOnNegativeHealth()
        {
            Enemy e = new Enemy(-10, 10.0, "basic", 10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that enemy throws an Exception with zero health
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testEnemyThrowsExceptioOnZeroHealth()
        {
            Enemy e = new Enemy(0, 10.0, "basic", 10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that enemy throws an Exception with negative speed
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testEnemyThrowsExceptioOnNegativeSpeed()
        {
            Enemy e = new Enemy(10, -10.0, "basic", 10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that enemy throws an Exception with zero speed
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testEnemyThrowsExceptioOnZeroSpeed()
        {
            Enemy e = new Enemy(10, 0.0, "basic", 10, new Rectangle(0, 0, 10, 10));
        }


        //Testing that enemy throws an Exception with negative gold
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testEnemyThrowsExceptioOnNegativeGold()
        {
            Enemy e = new Enemy(10, 10.0, "basic", -10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that enemy throws an Exception with zero gold
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testEnemyThrowsExceptioOnZeroGold()
        {
            Enemy e = new Enemy(10, 0.0, "basic", 0, new Rectangle(0, 0, 10, 10));
        }

        //Testing that enemy throws an Exception with invalid type ("basic" is a valid type of enemy)
        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void testEnemyThrowsExceptioOnInvalidType()
        {
            Enemy e = new Enemy(10, 10.0, "invalid", 10, new Rectangle(0, 0, 10, 10));
        }

        //Testing that enemy throws an Exception with null area
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testEnemyThrowsExceptioOnNullArea()
        {
            Enemy e = new Enemy(10, 0.0, "invalid", 10, new Rectangle());
        }



        #endregion

        #region Testing Health

        [Test()]
        public void EnemyHasCorrectHealthAfterInitialized()
        {
            enemy = new Enemy(10, 1.0f, "basic",20, rec);
            Assert.AreEqual(enemy.Health, 10);

        }

        [Test()]
        public void EnemyHasCorrectHealthAfterDamaged()
        {
            enemy = new Enemy(10, 1.0f, "basic", 20, rec);
            enemy.Health -= 1;
            Assert.AreEqual(enemy.Health, 9);
        }


        #endregion 

        #region Testing Speed

        [Test()]
        public void EnemyHasCorrectSpeedAfterInitialized()
        {
            enemy = new Enemy(10, 1.0f, "basic", 20, rec);
            Assert.AreEqual(enemy.Speed, 1.0f);
        }

        [Test()]
        public void EnemyHasCorrectSpeedAfterModified()
        {
            enemy = new Enemy(10, 1.0f, "basic", 20, rec);
            enemy.Speed *= 0.95f;
            Assert.AreEqual(enemy.Speed, 0.95f);
        }

        #endregion

        #region Testing Type

        [Test()]
        public void EnemyHasCorrectTypeAfterModified()
        {
            enemy = new Enemy(10, 1.0f, "basic", 20, rec);
            Assert.AreEqual(enemy.Type, "basic");
        }

        #endregion

        #region Testing Gold

        [Test()]
        public void EnemyHasCorrectGoldAfterInitialized()
        {
            enemy = new Enemy(10, 1.0f, "basic", 20, rec);
            Assert.AreEqual(enemy.Gold, 20);
        }


        #endregion


    }
}
