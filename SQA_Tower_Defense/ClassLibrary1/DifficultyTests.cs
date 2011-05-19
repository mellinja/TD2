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

        public class DifficultyTests

        {
        Enemy e;
        Tower tower;
        Castle c;

[SetUp()]

        public void setUp()
            {

                tower = new Tower("basic", 10, 10, 10, 10, new Rectangle(5, 6, 1, 1));

                e = new Enemy(10, 1.0f, "basic", 10, new Rectangle(7, 8, 1, 1));
    
                c = new Castle(100, new Rectangle(8, 8, 1, 1));

            }

//Test that the enemy health increases on “hard” difficulty, going from 10 to 15

        [Test()]
        public void EnemyHealthHigherOnHard()
            {
            Map m = new Map("normal",1000,3);
            m.SpawnEnemy(e);
            Assert.AreEqual(15f, m.enemiesOnMap[0].Health);   

            }

//Test that the enemy health decreases on “easy” difficulty to 75% of original

[Test()]

public void EnemyHealthLowerOnEasy()

            {

            Map m = new Map("normal",1000,1);
            m.SpawnEnemy(e);
            Assert.AreEqual(7.5f, m.enemiesOnMap[0].Health);   

           

            }

//Test that Castle has lower health on Hard (100 to 75)

[Test()]

public void TestCastleHasLowHealthOnHard()

{
    Map m = new Map("normal",1000,3);

    m.PlaceCastle(c);

    Assert.AreEqual(75f,m.Castle.Health);

}

//Test that Castle has more health on Easy (100 to 125)

[Test()]

public void TestCastleHasHigherHealthOnEasy()

{
    Map m = new Map("normal",1000,1);

    m.PlaceCastle(c);

    Assert.AreEqual(125f,m.Castle.Health);

}
//Test that enemies give a score equivalent to 10x the gold rewarded on Normal

            [Test()]

            public void TestThatScoreIncreasesByTenTimesGoldOnNormal()

                {

                Map m = new Map("normal",1000,2);

                m.SpawnEnemy(e);

                Assert.AreEqual(m.Score, 0);
                m.KillEnemy(e);

                Assert.AreEqual(m.Score, 100);
            }   
        //Test that enemies give a score equivalent to 7.5x the gold rewarded on Easy
        [Test()]
        public void TestThatScoreIncreasesBy75xGoldOnNormal()
        {
            Map m = new Map("normal",100,1);
            m.SpawnEnemy(e);
            Assert.AreEqual(m.Score, 0);
            m.KillEnemy(e);
            Assert.AreEqual(m.Score, 75);
           
        }





        //Test that enemies give a score equivalent to 12.5x the gold rewarded on Hard
        [Test()]
        public void TestThatScoreIncreasesBy125xGoldOnHard()
        {
            Map m = new Map("normal",100,3);
            m.SpawnEnemy(e);
            Assert.AreEqual(m.Score, 0);
            m.KillEnemy(e);
            Assert.AreEqual(m.Score, 125);
        }

    }
}