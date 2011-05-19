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
    public class CastleTests
    {



        //Tests init of Castle
        [Test()]
        public void CastleInitTest()
        {
            Castle c = new Castle(100, new Rectangle(1, 1, 1, 1));
            Assert.IsNotNull(c);
        }
        // Tests that castles with zero health on init throws error
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CastleInitTestHealth0()
        {
            Castle c = new Castle(0, new Rectangle(1, 1, 1, 1));
        }

        // Tests that castles with negative health on init throws error
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CastleInitTestHealthNegative()
        {
            Castle c = new Castle(-1, new Rectangle(1, 1, 1, 1));
        }

        // Tests that castles with null recatangle (0 width and length) throws exception
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CastleInitTestBadLocation()
        {
            Castle c = new Castle(1, new Rectangle(1, 1, 0, 0));
        }

        //Castle can be destroyed (removed from map)
        [Test()]
        public void castleCanBeDestroyed()
        {


            Map map = new Map("normal", 100000, 1);
            map.PlaceCastle(new Castle(1, new Rectangle(1, 1, 1, 1)));
            Assert.IsNotNull(map.Castle);
            map.Castle.takeDamage(1);
            map.Update();
            Assert.IsNull(map.Castle);

        }


        //Castle can be destroyed (removed from map), stops everything (movements)
        [Test()]
        public void castleCanBeDestroyedStopsAll()
        {
            Map map = new Map("normal", 100000, 1);
            map.PlaceCastle(new Castle(1, new Rectangle(1, 1, 1, 1)));
            map.SpawnEnemy(new Enemy(1, 1, "basic", 1, new Rectangle(12, 12, 12, 12)));
            map.Castle.takeDamage(1);
            map.Update();
            Assert.AreEqual(map.enemiesOnMap[0].Location, new Rectangle(12, 12, 12, 12));

        }

    }

}