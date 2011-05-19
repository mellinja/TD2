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

    class AutomaticWaveTests
    {

        Map map;

        Enemy e;


        [SetUp()]

        public void setup()
        {

            e = new Enemy(10, 1.0f, "basic", 10, new Rectangle(7, 8, 1, 1));

            map = new Map("normal", 10000, 1);

            map.setStandardEnemy(e);//Allows the map to create waves of this enemy

        }

        //Test that after 5000 updates, map creates a new Wave

        [Test()]

        public void TestAutoWave()
        {

            for (int i = 0; i < 999; i++)
            {

                map.Update();//Callls 999 updates

            }

            Assert.IsNull(map.currentWave());

            map.Update();//1000th update

            Assert.IsNotNull(map.currentWave());

        }

        //Test that 2nd wave has higher health

        [Test()]

        public void TestAutoWaveBecomesHarder()
        {

            for (int i = 0; i < 1000; i++)
            {

                map.Update();//Callls 1000 updates, creates wave

            }
            
            Enemy t = map.currentWave().Pop();

            for (int i = 0; i < 1000; i++)
            {

                map.Update();//Callls 1000  moreupdates, creates wave

            }

            Assert.IsTrue(map.currentWave().Pop().Health > t.Health);

        }

        //Test that 3rd health is the same as 2nd, more baddies

        [Test()]

        public void TestAutoWaveBecomesHarderAndBigger()
        {

            for (int i = 0; i < 2000; i++)
            {

                map.Update();//Callls 2000 updates, creates 2 waves, first one dies off

            }

            int Count = map.currentWave().Count();

            Enemy t = map.currentWave().Pop();

            for (int i = 0; i < 1000; i++)
            {

                map.Update();//Callls 1000  moreupdates, creates wave

            }

            Assert.Greater(map.currentWave().Count(), Count);

            Assert.IsTrue(map.currentWave().Pop().Health == t.Health);

        }






    }

}

