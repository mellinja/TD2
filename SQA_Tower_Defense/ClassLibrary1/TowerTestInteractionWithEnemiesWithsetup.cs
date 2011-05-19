using System;
using System.Collections.Generic;
using SQA_Tower_Defense;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace ClassTests
{

[TestFixture()]
    class TowerTestInteractionWithEnemiesWithsetup
    {


        private Map map;
        private int towerRange;
        private Enemy enemy;
        private Tower tower;
        private Rectangle towerPosition;
        private Rectangle enemyPosition;

        [SetUp()]
        public void SetUp()
        {
            towerRange = 5;
            towerPosition = new Rectangle(0, 1, 1, 1);
            enemyPosition = new Rectangle(1, 1, 1, 1);
            enemy = new Enemy(10, 1.0f, "basic", 10, enemyPosition);
            tower = new Tower("Tower", 1, 1, 1, 100, towerPosition);
            map = new Map("normal", 100, 1);
            map.PlaceTower(tower);
            map.SpawnEnemy(enemy);

        }
        //Verifies that a tower does not attack an enemy outside of its range by checking the health of the enemy and the list of enemies of the tower
        [Test()]
        public void TowerDoesNotAttackEnemyOutsideItsRange()
        {
            //Move the enemy outside of the tower's range
            enemy.moveTo(1000, 1000);

            //Update method for map causes the towers to add nearby enemies
            map.Update();

            Assert.AreEqual(0, tower.Enemies.Count);
            //Update method causes the tower to attack nearest enemy in its range
            for(int i = 0; i < tower.UpdateMax; i++)
                tower.Update();

            //Verify that the enemy was not damaged
            Assert.AreEqual(10, enemy.Health);

        }


        //Verifies that a tower will attack an enemy within its range by checking the health of the enemy
        [Test()]
        public void TowerAttacksAnEnemyInItsRange()
        {

            map.Update(); //Update method for map causes the towers to add nearby enemies

            Assert.AreEqual(1, tower.Enemies.Count);

            for(int i = 0; i < tower.UpdateMax; i++)
                tower.Update(); //Enemy is already within tower's range rectangle; tower's update should attack it

            Assert.AreEqual(9, enemy.Health); //Verify that the enemy has taken one point of damage


        }


        //Verifies that a tower attacking an enemy will stop attacking said enemy once it is out of range
        [Test()]
        public void TowerStopsAttackingEnemyOnceItMovesOutsideOfRange()
        {


            map.Update(); //Update method for map causes the towers to add nearby enemies

            for(int i = 0; i < tower.UpdateMax; i++)
                tower.Update(); //Enemy is already within tower's range rectangle; tower's update should attack it after a certain number of cycles

            Assert.AreEqual(9, enemy.Health); //Verify that the enemy has taken one point of damage

            enemy.moveTo(1000, 1000); //Move the enemy outside of the tower's range

            map.Update(); //Update method for map causes should now remove the enemy from the tower's list of enemies

            for (int i = 0; i < 60; i++)
                tower.Update(); //The tower attacks once every 60 updates -- so force 60 updates on the tower for it to attack again.

            

            Assert.AreEqual(9, enemy.Health); //Verify that the enemy has not received additional damage

        }

        //Tests that an enemy being attacked by multiple towers is removed from both tower's list of enemies when killed.
        [Test()]
        public void EnemyThatDiesWithinRangeOfTwoTowersGetsRemovedFromBothTowerLists()
        {
            Tower tower2 = new Tower("Tower2", 10, 1, 1, 50, new Rectangle(2, 2, 1, 1)); //Second tower; enemy should still be within its range

            map.PlaceTower(tower2);

            enemy.Health = 2; //Manually set the enemy's health to 2 for the case of this test

            map.Update(); //Update method for map cases both of the towers to add nearby enemies

            Assert.AreEqual(tower.Enemies.Count, 1); //Verify that the first tower's list of enemies has one enemy
            Assert.AreEqual(tower2.Enemies.Count, 1); //Verify that the second tower's list of enemies has one enemy

       
            tower.AttackEnemy(enemy); //Update the first tower to attack the enemy                

            tower2.AttackEnemy(enemy); //Update the second tower to attack (and thereby killing) the enemy
            

            map.Update(); //Update method for map causes both of the towers to update their list of nearby enemies\

            Assert.AreEqual(tower.Enemies.Count, 0); //Verify that the first tower's list of enemies is empty
            Assert.AreEqual(tower2.Enemies.Count, 0); //Verify that the second tower's list of enemies is empty

        }



        [Test()]
        public void EnemyMoveOutofRange()
        {
           //enemy can move out of range 
           Enemy enemy2 = new Enemy(10, 1.0f, "basic", 10, new Rectangle(3, 3, 1, 1));
           map.SpawnEnemy(enemy2);
           map.Update();
           

           Assert.AreEqual(2, tower.Enemies.Count);
           enemy.moveTo(500, 500);
           map.Update();
           Assert.AreEqual(tower.Enemies.Count, 1);
        }

        [Test()]
        public void MoveBothEnemyOutofRange()
        {
            //testing that both enemies can move out of range after being attacked
            Enemy enemy2 = new Enemy(10, 1.0f, "basic", 10, new Rectangle(3, 3, 1, 1));

            map.SpawnEnemy(enemy2);

            map.Update();


            Assert.AreEqual(tower.Enemies.Count, 2);
            Assert.AreEqual(enemy.Health, 10);
            
            enemy.moveTo(5000, 5000);
            map.Update();
            
            Assert.AreEqual(tower.Enemies.Count, 1);
            Assert.AreEqual(enemy2.Health, 10);

            enemy2.moveTo(6000, 6000);
            map.Update();

            Assert.AreEqual(tower.Enemies.Count, 0);

        }

        [Test()]
        public void attackEvery60()
        {
            
            //Tests tower attacks once every 60 updates

            map.Update();


            Assert.AreEqual(enemy.Health, 10);
            float temp = enemy.Health;
            for (int x = 0; x < 59; x++)
            {
                tower.Update();
                Assert.AreEqual(temp, enemy.Health);
            }
            tower.Update();
            Assert.AreNotEqual(temp, enemy.Health);
        }

        [Test()]
        public void MultipleTowersCanAttackSingleEnemy()
        {
            //Testing that two towers can attack the same enemy
            Tower tower2 = new Tower("", 10, 20, 30, 40, new Rectangle(4,4, 1,1));

            map.PlaceTower(tower2);
            map.Update();

            float temp = enemy.Health; 
            for(int i = 0; i < tower.UpdateMax; i++)    
                tower.Update(); 
            
            Assert.AreNotEqual(enemy.Health, temp); 
            temp = enemy.Health;
            for(int i = 0; i < tower2.UpdateMax; i++)
                tower2.Update();
            
            Assert.AreNotEqual(enemy.Health, temp);
        }

        [Test()]
        public void EnemyDestroyed()
        {
            //testing that towers can destroy enemies and that they are removed from list of the enemies in the tower's range
            enemy.moveTo(40, 40); //Move the enemy away from the tower for this test

            Enemy enemy2 = new Enemy(1, 1.0f, "basic", 10, new Rectangle(3, 3, 1, 1));
            map.SpawnEnemy(enemy2);
            
            map.Update();
            
            Assert.AreEqual(2, tower.Enemies.Count);
            for (int i = 0; i < tower.UpdateMax; i++)
            {
                tower.Update();
            }

            map.Update();
            
            Assert.AreEqual(1, tower.Enemies.Count);
        }


    }
}
