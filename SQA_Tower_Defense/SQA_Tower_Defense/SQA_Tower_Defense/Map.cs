﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SQA_Tower_Defense
{
    //100 lines
    public class Map
    {

        Castle castle;
        String gametype;
        int money, score;
        int difficulty;
        List<Tower> towersOnMap;
        public List<Enemy> enemiesOnMap;
        List<SaveState> saveStates;
        Wave wave;
        int updateCounter = 0;
        int UPDATE_MAX = 50;
        bool rewinding;
        bool gameEndCondition;
        bool firstWaveConstructed;
        int numberOfEnemies = 10;

        float HARD_MULTIPLIER_ENEMY = 1.5f;
        float HARD_MULTIPLIER_SCORE = 12.5f;
        float HARD_MULTIPLIER_CASTLE = 0.75f;
        float EASY_MULTIPLIER_ENEMY = 0.75f;
        float EASY_MULTIPLIER_SCORE = 7.5f;
        float EASY_MULTIPLIER_CASTLE = 1.25f;


        Enemy StandardEnemy;

        //Constructs a game Map, which does most of the work load of the game (13 lines)
        public Map(String gametype, int startingMoney, int difficulty)
        {
            if (1 > difficulty || difficulty > 3)
                throw new ArgumentOutOfRangeException();

            if (gametype != "normal")
                throw new ArgumentException();


            if (startingMoney < 0)
                throw new ArgumentOutOfRangeException();

            this.firstWaveConstructed = false;
            this.gametype = gametype;

            this.money = startingMoney;

            this.difficulty = difficulty;
            this.score = 0;


            this.towersOnMap = new List<Tower>();
            this.enemiesOnMap = new List<Enemy>();
            this.saveStates = new List<SaveState>();

            this.gameEndCondition = false;
        }


        //Places a tower if not other towers are at that location if the user has enough money (6 lines)
        public void PlaceTower(Tower tower)
        {
            for (int i = 0; i < towersOnMap.Count; i++)
            {
                if (tower.Location.Intersects(towersOnMap[i].Location))
                    return;
            }
            if (this.money >= tower.Cost)
            {
                towersOnMap.Add(tower);
                money -= tower.Cost;
            }
        }
        //Places the destination castle on the map; the location cannot conflict or intersect with any of the towers already placed on the map.
        public void PlaceCastle(Castle c)
        {
            if (castle != null) return;

            for (int i = 0; i < towersOnMap.Count; i++)
            {
                if (c.Location.Intersects(towersOnMap[i].Location))
                    return;
            }

            foreach (Enemy e in enemiesOnMap)
            {
                e.Castle = c;
            }
            castle = c;

        }
        //Checks to see if the given rectangle (location) intersects with any of the towers currently placed on the map.
        public bool isConflicting(Rectangle rectangle)
        {


            foreach (Tower t in this.Towers)
            {
                if (t.Name == "path")
                {
                    return false;
                }
                if (rectangle.Intersects(t.Location))
                {
                    return true;
                }
            }

            return false;


        }


        //Sells a tower, adding .75 times its original cost to the bank (8 lines)
        public void SellTower(Tower tower)
        {
            towersOnMap.Remove(tower);
            money += (int)(tower.Cost * 0.75);
        }

        public void setStandardEnemy(Enemy e)
        {
            this.StandardEnemy = e;
        }

        //Adds an Enemy to that map (1 line)
        public void SpawnEnemy(Enemy enemy)
        {
            if (difficulty == 3)
            {
                enemy.MaxHealth *= HARD_MULTIPLIER_ENEMY;
                enemy.Health *= HARD_MULTIPLIER_ENEMY;
            }
            else if (difficulty == 1)
            {
                enemy.MaxHealth *= EASY_MULTIPLIER_ENEMY;
                enemy.Health *= EASY_MULTIPLIER_ENEMY;
            }
            enemy.Map = this;
            enemy.Castle = this.castle;
            enemiesOnMap.Add(enemy);
        }

        //Removes an enemy, giveing the user its money (2 lines)
        public void KillEnemy(Enemy enemy)
        {
            enemiesOnMap.Remove(enemy);
            score += enemy.Gold * 10;
            money += enemy.Gold;
        }

        //sets Wave w to the current wave of enemies (1 line)
        public void newWave(Wave w)
        {
            this.wave = w;
        }

        //Getters and Setters for the fields of the map class (11 lines)
        #region Getters/Setters

        public String Gametype
        {
            get { return this.gametype; }
        }
        public int Money
        {
            get { return this.money; }
            set { this.money = value; }
        }
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }

        }
        public int Difficulty
        {
            get { return this.difficulty; }
            set { this.difficulty = value; }
        }
        public List<Tower> Towers
        {
            get { return this.towersOnMap; }
            set { this.towersOnMap = value; }
        }
        public List<Enemy> Enemies
        {
            get { return this.enemiesOnMap; }
            set { this.enemiesOnMap = value; }
        }
        public List<SaveState> SaveStates
        {
            get { return this.saveStates; }
        }
        public bool Rewinding
        {
            get { return this.rewinding; }
            set { this.rewinding = value; }
        }

        public bool GameOver
        {
            get { return this.gameEndCondition; }
        }

        public Castle Castle
        {
            get { return this.castle; }
        }



        #endregion


        //Save state creation and loading (31 lines)
        #region Save States

        public struct SaveState
        {
            public List<Enemy> enemies;
            public List<Tower> towers;
            public Wave wave;
            public int score;
            public int money;
            public int counter;

        }

        public void SaveNextState()
        {
            if (saveStates.Count > 1000)
                saveStates.RemoveAt(0);
            SaveState nextState = new SaveState();
            nextState.enemies = new List<Enemy>();
            nextState.towers = new List<Tower>();
            nextState.counter = this.updateCounter;


            if (this.wave != null)
            {
                nextState.wave = new Wave(this.wave.Enemy,0);

                for (int i = 0; i < this.wave.Enemies.Count; i++)
                {
                    nextState.wave.Enemies.Push(this.wave.Enemies.Peek().Clone());
                }
            }

            for (int i = 0; i < this.Towers.Count; i++)
            {
                nextState.towers.Add(this.Towers[i]);
            }
            for (int i = 0; i < this.Enemies.Count; i++)
            {
                Enemy temp = this.Enemies[i];
                Enemy enemy = new Enemy(temp.MaxHealth, temp.Speed, temp.Type, temp.Gold, temp.Location);
                enemy.Health = temp.Health;
                nextState.enemies.Add(enemy);
            }
            nextState.score = this.Score;
            nextState.money = this.Money;
            saveStates.Add(nextState);
        }

        public void LoadPreviousState()
        {

            if (saveStates.Count == 0)
                return;
            SaveState previousState = new SaveState();
            previousState = this.saveStates[saveStates.Count - 1];
            this.Money = previousState.money;
            this.Score = previousState.score;
            this.Towers = new List<Tower>();
            this.Enemies = new List<Enemy>();
            this.updateCounter = previousState.counter;


            if (previousState.wave != null)
            {
                this.wave = new Wave(previousState.wave.Enemy, 0);
                for (int i = 0; i < previousState.wave.Enemies.Count; i++)
                {
                    this.wave.Enemies.Push(previousState.wave.Enemies.Peek().Clone());
                }
            }
            for (int i = 0; i < previousState.towers.Count; i++)
                this.Towers.Add(previousState.towers[i]);
            for (int i = 0; i < previousState.enemies.Count; i++)
            {
                Enemy temp = previousState.enemies[i];
                Enemy enemy = new Enemy(temp.MaxHealth, temp.Speed, temp.Type, temp.Gold, temp.Location);
                enemy.Castle = this.castle;
                enemy.Health = temp.Health;
                enemy.Map = this;
                this.Enemies.Add(enemy);
            }
            this.saveStates.RemoveAt(saveStates.Count - 1);
        }

        #endregion


        #region Wave stuff


        public Stack<Enemy> currentWave()
        {
            if (this.wave != null)
                return this.wave.Enemies;

            else return null;

        }
        #endregion

        //Spawns enemies off the wave stack, then adds enemies to each towers lists, then removes dead enemies (27 lines)
        public void Update()
        {


            updateCounter++;

            if (null != castle)
            {
                if (castle.Health <= 0)
                {
                    gameEndCondition = true;
                    castle = null;
                }
            }

            if (gameEndCondition == true) return;
            foreach (Enemy e in this.enemiesOnMap)
            {
                if (!rewinding)
                    e.Move();
            }
            if (updateCounter % UPDATE_MAX == 0)
            {
                if (wave != null)
                {
                    Enemy e = wave.getEnemy();
                    if (e != null)
                    {
                        this.SpawnEnemy(e);
                    }
                    else
                    {
                        wave = null;
                    }

                }
            }

            if (updateCounter % 1000 == 0)
            {
                if (StandardEnemy != null)
                {
                    if (!firstWaveConstructed)
                    {
                        this.wave = new Wave(
                        StandardEnemy, numberOfEnemies);
                        firstWaveConstructed = true;
                    }
                    else if (updateCounter % 2000 == 0)
                    {
                        StandardEnemy.Health = (int)(StandardEnemy.Health * 1.2f);
                        this.wave = new Wave(
                        StandardEnemy, numberOfEnemies);
                        updateCounter = 0;

                    }
                    else
                    {

                        numberOfEnemies = (int)(numberOfEnemies * 1.2f);
                        this.wave = new Wave(
                        StandardEnemy, numberOfEnemies);
                    }
                }
            }


            foreach (Tower t in this.towersOnMap)
            {
                List<Enemy> KillEnemyList = new List<Enemy>();
                t.Enemies.Clear();
                foreach (Enemy e in this.enemiesOnMap)
                {
                    if (e.Health <= 0)
                    {
                        KillEnemyList.Add(e);
                    }
                    else
                    {
                        double tCenterX = t.Location.X + t.Location.Width / 2;
                        double eCenterX = e.Location.X + e.Location.Width / 2;
                        double tCenterY = t.Location.Y + t.Location.Height / 2;
                        double eCenterY = e.Location.Y + e.Location.Height / 2;

                        double distance = Math.Sqrt((tCenterX - eCenterX) * (tCenterX - eCenterX) + (tCenterY - eCenterY) * (tCenterY - eCenterY));
                        if (t.Range >= distance)
                            t.Enemies.Add(e);
                    }

                }
                foreach (Enemy e in KillEnemyList)
                {
                    KillEnemy(e);
                }

            }

        }
    }
    }
