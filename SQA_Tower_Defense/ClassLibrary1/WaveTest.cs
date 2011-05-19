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
    class WaveTest
    {

        //Tests wave initializes
        Wave wave;
        [Test()]
        public void WaveInitializes()
        {
            Wave wave = new Wave(new Enemy(10, 1.0f, "basic", 20, new Rectangle(5, 5, 1, 1)), 1);
            Assert.IsNotNull(wave); 
        }



        //Testing that Wave throws an exception when init with null enemy
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WaveThrowsOnNullEnemy()
        {
             Wave wave = new Wave(null, 1);
        }

        //Testing that Wave throws an exception when init with 0 count
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WaveThrowsOn0Count()
        {
             Wave wave = new Wave(new Enemy(10, 1.0f, "basic", 20, new Rectangle(5, 5, 1, 1)), 0);
        }

        
        //Testing that Wave throws an exception when init with negative count
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WaveThrowsOnNegativeCount()
        {
             Wave wave = new Wave(new Enemy(10, 1.0f, "basic", 20, new Rectangle(5, 5, 1, 1)), -1);
        }


        //Testing that waves return an enemy when getEnemy is called
        [Test()]
        public void WaveReturnsAnEnemy()
        {
            Enemy e = new Enemy(10, 1.0f, "basic", 20, new Rectangle(5, 5, 1, 1));
             Wave wave = new Wave(e, 10);
            Enemy returned = wave.getEnemy();
            Assert.AreEqual(e.Type, returned.Type); //Checking to see if the types of enemies are the same


        }

        
        //Testing that waves return an enemy when getEnemy is called
        [Test()]
        public void WaveReturnsMultipleEnemy()
        {
            Enemy e = new Enemy(10, 1.0f, "basic", 20, new Rectangle(5, 5, 1, 1));
             Wave wave = new Wave(e, 10);
            Enemy returned = wave.getEnemy();
            Enemy returned2 = wave.getEnemy();
            Assert.AreEqual(e.Type, returned.Type); //Checking to see if the types of enemies are the same
            Assert.AreEqual(e.Type,returned.Type);

        }

        //Testing that waves returns unique enemies (not pointing to the same enemy)
        [Test()]
        public void WaveReturnsUniqueEnemies()
        {
            Enemy e = new Enemy(10, 1.0f, "basic", 20, new Rectangle(5, 5, 1, 1));
             Wave wave = new Wave(e, 10);
            Enemy returned = wave.getEnemy();
            Enemy returned2 = wave.getEnemy();
            returned.Health -= 1;
            Assert.AreNotEqual(returned.Health,returned2.Health);

        }






    }
}
