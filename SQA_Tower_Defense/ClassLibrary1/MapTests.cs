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
    public class MapTests
    {

        Map map;
        Rectangle rec;
        Tower tower;
        Enemy enemy;


        [SetUp()]
        public void SetUp()
        {
            map = new Map("normal", 100, 1);
            rec = new Rectangle(0, 0, 5, 5);
            tower = new Tower("tower", 10, 20, 30, 40, rec);
            enemy = new Enemy(10, 1.0f, "basic", 10, rec);
        }

        #region Initialization

        [Test()]
        public void MapInitializes()
        {

            map = new Map("normal", 0, 1);
            Assert.IsNotNull(map);

        }

        //Testing that map throws an expection with bad difficulties (valid difficulties range from 1 to 3
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void test0Difficulty()
        {
            Map m = new Map("normal", 10000, 0);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testNegativeDifficulty()
        {
            Map m = new Map("normal", 10000, -1);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testTooHighDifficulty()
        {
            Map m = new Map("normal", 10000, 4);
        }

        //Testing Invalid Game type ("normal" is a valid game type)
        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void testMapInvalidGameType()
        {
            Map m = new Map("invalid", 10000, 2);
        }


        //Testing negative starting money (0 starting money is possible)
        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testMapNegativeMoney()
        {
            Map m = new Map("normal", -10000, 2);
        }


        #endregion

        #region Difficulty Stuff



        #endregion

        #region Money

        [Test()]
        public void MoneyIncreasesWhenEnemyIsKilled()
        {
            map.SpawnEnemy(enemy);
            map.KillEnemy(enemy);
            Assert.AreEqual(110, map.Money);
        }

        [Test()]
        public void MoneyIncreasesWhenTowerIsSold()
        {
            Map map = new Map("normal", 0, 1);
            Tower tower = new Tower("normal", 10, 20, 30, 40, rec);
            map.SellTower(tower);
            Assert.AreEqual(22, map.Money);
        }

        [Test()]
        public void InsufficientFundsRejectTowerPlacement()
        {
            map = new Map("normal", 0, 1);
            map.PlaceTower(tower);
            Assert.AreEqual(map.Towers, new List<Tower>());
        }

        [Test()]
        public void MoneyDecreasesWhenTowerIsPlaced()
        {
            map.PlaceTower(tower);
            Assert.AreEqual(70, map.Money);
        }


        #endregion

        #region Towers


        [Test()]
        public void TowersGetAddedToListWhenPlaced()
        {
            
            map.PlaceTower(tower);
            Assert.AreEqual(1, map.Towers.Count);

        }

        [Test()]
        public void TowersGetRemovedFromListWhenSold()
        {

            Tower tower = new Tower("tower", 10, 20, 30, 40, rec);
            Map map = new Map("normal", 0, 1);
            map.PlaceTower(tower);
            map.SellTower(tower);
            Assert.AreEqual(map.Towers, new List<Tower>());

        }

        [Test()]
        public void TowersCannotBePlacedOnTopOfAnotherTower()
        {
            map.PlaceTower(tower);
            map.PlaceTower(tower);
            Assert.AreEqual(1, map.Towers.Count);
        }

        #endregion

        #region Enemies

        [Test()]
        public void EnemiesNotInListThatGetKilledCrashGame()
        {
            map = new Map("normal", 0, 1);
            map.KillEnemy(enemy);
        }
        
        
        [Test()]
        public void EnemiesGetAddedToListWhenSpawned()
        {

        }

        [Test()]
        public void EnemiesGetRemovedFromListWhenKilled()
        {

        }

        #endregion 

        #region Save States

        [Test()]
        public void SaveStateListIsNotNullWhenMapInitializes()
        {
            Assert.IsNotNull(map.SaveStates);
        }


        [Test()]
        public void SaveStateNextStateAddsNewStateToList()
        {
            map.SaveNextState();
            Assert.AreEqual(1, map.SaveStates.Count);
        }

        [Test()]
        public void SaveStateOneSavedStateHasCorrectTowerList()
        {
            map.SaveNextState();
            Assert.AreEqual(map.Towers, map.SaveStates[0].towers);
        }

        [Test()]
        public void SaveStateTwoSavedStatesHaveCorrectTowerList()
        {
            map.SaveNextState();
            map.PlaceTower(tower);
            map.SaveNextState();
            Assert.AreEqual(map.Towers, map.SaveStates[1].towers);
        }
        [Test()]
        public void SaveStateTwoSavedStatesDoNotHaveIncorrectTowerList()
        {
            map.SaveNextState();
            map.PlaceTower(tower);
            map.SaveNextState();
            Assert.AreEqual(map.Towers, map.SaveStates[1].towers);
        }

        [Test()]
        public void SaveStateOneSaveStateHasCorrectMoneyCount()
        {
            map.SaveNextState();
            Assert.AreEqual(map.Money, map.SaveStates[0].money);
        }

        [Test()]
        public void SaveStateOneSaveStateHasCorrectScoreAmount()
        {
            map.SaveNextState();
            Assert.AreEqual(map.Score, map.SaveStates[0].score);
        }

        [Test()]
        public void LoadedStateHasCorrectMoneyAmount()
        {
            map.SaveNextState();
            map.PlaceTower(tower);
            map.LoadPreviousState();
            Assert.AreEqual(100, map.Money);

        }
        [Test()]
        public void LoadedStateHasCorrectTowerCount()
        {
            map.SaveNextState();
            map.PlaceTower(tower);
            map.LoadPreviousState();
            Assert.AreEqual(0, map.Towers.Count);
        }

        [Test()]
        public void LoadedStatesDoNotLoadIfThereIsNoPreviousStates()
        {
            Map temp = map;
            map.LoadPreviousState();
            Assert.AreEqual(temp, map);
        }
        [Test()]
        public void LoadedStatesDoChangeStateOfMap()
        {
            Map temp = map;
            map.SaveNextState();
            map.PlaceTower(tower);
            map.LoadPreviousState();
            Assert.AreEqual(map, temp);
        }

        [Test()]
        public void SavedStateLastStateHasCorrectEnemyCount()
        {
            map.SaveNextState();
            map.SpawnEnemy(enemy);
            map.SpawnEnemy(enemy);
            map.SpawnEnemy(enemy);
            map.SaveNextState();
            Assert.AreEqual(3, map.Enemies.Count);
        }
        [Test()]
        public void SavedStateLastStateHasCorrectEnemy()
        {
            map.SpawnEnemy(enemy);
            map.SpawnEnemy(enemy);
            enemy = new Enemy(20, 30f, "basic", 30, rec);
            map.SpawnEnemy(enemy);
            map.SaveNextState();
            Assert.AreEqual(map.SaveStates[0].enemies[2].Health, 20);

        }

        [Test()]
        public void LoadedStateHasCorrectLastEnemy()
        {
            map.SpawnEnemy(enemy);
            map.SpawnEnemy(enemy);
            map.SaveNextState();
            enemy = new Enemy(20, 30f, "basic", 30, rec);
            map.SpawnEnemy(enemy);
            map.LoadPreviousState();
            Assert.AreEqual(map.Enemies[1].Health, 10);
            

        }

        #endregion




    }
}
