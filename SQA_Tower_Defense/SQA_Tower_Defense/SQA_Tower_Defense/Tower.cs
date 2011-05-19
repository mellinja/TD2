using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SQA_Tower_Defense
{

    //Towers try to destroy enemies and protect the castle (52 lines)
    public class Tower
    {
        protected String name;
        
        protected int health;
        protected int attackDamage;
        protected int cost;
        protected int range;
        public int updateCounter;
        Rectangle location;
        protected List<Enemy> nearbyEnemies;
        public int UpdateMax = 60;

        //Contructs a tower. Throws exceptions for invalid ranges of values (18 lines)
        public Tower (String name, int health, int damage, int cost, int range, Rectangle location)
        {

            if (location.Equals(new Rectangle()))
                throw new ArgumentNullException();
            if (range <= 0)
                throw new ArgumentOutOfRangeException();
            if (damage <= 0)
                throw new ArgumentOutOfRangeException();
            if (health <= 0)
                throw new ArgumentOutOfRangeException();

            if (cost <= 0)
                throw new ArgumentOutOfRangeException();


            this.name = name;
            this.location = location;
            this.health = health;
            this.attackDamage = damage;
            this.cost = cost;
            this.range = range;
            this.updateCounter = 0;
            this.nearbyEnemies = new List<Enemy>();
        }


        //Adds an enemy to the enemy list for this tower (1 line)
        public void AddNearbyEnemy(Enemy enemy)
        {
            nearbyEnemies.Add(enemy);
        }

        //Attacks nearest enemy, reducing its health by the towers damage stat (2 lines)
        public void AttackEnemy()
        {
            if (nearbyEnemies.Count > 0)
                nearbyEnemies[0].Health -= this.attackDamage;
        }

        //Attacks enemy e (1 line)
        public void AttackEnemy(Enemy e)
        {
                e.Health -= this.attackDamage;
        }


        //getters and setters for the fields of a tower (9 lines)
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
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
        public String Name
        {
            get { return this.name; }
        }
        public List<Enemy> Enemies
        {
            get { return this.nearbyEnemies; }
        }
        public Rectangle Location
        {
            get { return this.location; }
            set { this.location = value; }
        }



        //Updates the towers information. Attacks enemies if they are close enough and the updateCounter is on the correct values (8 lines)
        public void Update()
        {
            this.updateCounter++;
            if (this.updateCounter == UpdateMax)
            {
                updateCounter = 0;
                Enemy attacking = this.getCurrentTarget();
                if (attacking != null)
                {
                    attacking.Health -= this.attackDamage;
                    if (attacking.Health <= 0)
                        this.Enemies.Remove(attacking);
                }
            }
        }


        //Returns the closest enemy in the enemies list, using the distance formula (13 lines)
        public Enemy getCurrentTarget()
        {
            Enemy attacking = null;
            double distanceToClosest = range;
            for (int i = 0 ; i < Enemies.Count ; i++)
            {
                Enemy e = this.Enemies[i];
                double tCenterX = this.Location.X;
                double eCenterX = e.Location.X;
                double tCenterY = this.Location.Y;
                double eCenterY = e.Location.Y;

                double distance = DistanceFrom(new Vector2((float)eCenterX, (float)eCenterY));//Math.Sqrt((tCenterX - eCenterX) * (tCenterX - eCenterX) + (tCenterY - eCenterY) * (tCenterY - eCenterY));
                
                if (distance <= distanceToClosest)
                {
                    distanceToClosest = distance;
                    attacking = e;
                }
            }
            return attacking;
        }

        public double DistanceFrom(Vector2 enemy)
        {
            return Math.Sqrt((this.location.Center.X - enemy.X) * (this.location.Center.X - enemy.X) + (this.location.Center.Y - enemy.Y) * (this.location.Center.Y - enemy.Y));

        }


        public Tower Clone()
        {
            return new Tower(this.Name, this.Health, this.attackDamage, this.Cost, this.Range, this.Location);
        }
    }
}
