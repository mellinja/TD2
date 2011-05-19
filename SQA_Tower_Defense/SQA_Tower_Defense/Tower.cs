using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SQA_Tower_Defense
{
    class Tower
    {
        protected Texture2D towerImage;
        protected String name;
        protected Vector2 location;
        protected int health;
        protected int attackDamage;
        protected int cost;
        protected int range;
        protected List<Enemy> nearbyEnemies;

        public Tower(Texture2D towerImage, String name, int health, int damage,
                        int cost, int range, Vector2 location)
        {
            this.towerImage = towerImage;
            this.name = name;
            this.health = health;
            this.attackDamage = damage;
            this.cost = cost;
            this.range = range;
            this.location = location;

        }

        public void AddNearbyEnemy(Enemy enemy)
        {
            nearbyEnemies.Add(enemy);
        }

        public void AttackEnemy()
        {
            if(nearbyEnemies.Count > 0)
                nearbyEnemies[0].Health -= this.attackDamage;
        }
        public int Health
        {
            get { return this.health; }
        }
        public int AttackDamage
        {
            get { return this.attackDamage; }
        }
        public int Cost
        {
            get { return this.cost; }
        }
        public int Range
        {
            get { return this.range; }
        }
        public Texture2D Image
        {
            get { return this.towerImage; }
        }
        public String Name
        {
            get { return this.name; }
        }


        
    }
}
