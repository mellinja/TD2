using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SQA_Tower_Defense
{

    //Enemies main goal is to destroy Castles and towers. (25 lines)
    public class Enemy
    {

        protected float health, maxHealth;
        protected int gold;
        protected double speed;
        protected String type;
        protected Rectangle location;
        protected int xDirection = 1;
        protected int yDirection = 1;
        Random random = new Random();
        protected Castle castle;
        protected bool inFiringRange;
        protected Map map;
        protected int freezingStop;
        protected int counter, burnStop;
        protected Color c;
        
        //Constructs the Enemy class, throws 2 exceptions (ArgumentOurOfRangeException and ArgumentNullArgument) (15 lines)
        public Enemy(float health, double speed, String type, int gold, Rectangle location)
        {

            if (location.Equals(new Rectangle()))
                throw new ArgumentNullException();
            if (gold <= 0)
                throw new ArgumentOutOfRangeException();
            if (speed <= 0)
                throw new ArgumentOutOfRangeException();
            if (health <= 0)
                throw new ArgumentOutOfRangeException();

            if (type != "basic")
                throw new ArgumentException();


            this.maxHealth = health;
            this.health = health;
            this.speed = speed;
            this.type = type;
            this.gold = gold;

            this.location = location;

            this.inFiringRange = false;
        }


        public Enemy Clone()
        {
            return new Enemy(this.health, this.speed, this.type, this.gold, this.location);

        }


        //Checks whether it is slowed or burning
        public void Update()
        {
            counter++;
            if (burnStop > counter && freezingStop > counter)
            {
                this.health -= 1;
                this.c = Color.Purple;
            }
            else if (counter < burnStop)
            {
                this.health -= 1;
                this.c = Color.Red;
            }

            else if (freezingStop > counter)
                this.c = Color.Blue;



            else
                this.c = Color.White;

            


        }

        //Moves the enemy buy 1 unit down and 1 unit left (1 line)
        public void Move()
        {

            if (freezingStop>counter && counter % 2 == 0)
                return;
            
            
            if (inFiringRange)
            {

                this.castle.takeDamage(1);

                return;

            }

            Rectangle tempLocation = this.Location;


            if (this.castle == null)
            {

                this.location = new Rectangle(this.location.X + 1, this.location.Y + 1, this.location.Width, this.location.Height);

                return;
            }

            Rectangle up = new Rectangle(this.location.X, this.location.Y - 1, this.location.Width, this.location.Height);
            Rectangle down = new Rectangle(this.location.X, this.location.Y + 1, this.location.Width, this.location.Height);
            Rectangle left = new Rectangle(this.location.X - 1, this.location.Y, this.location.Width, this.location.Height);
            Rectangle right = new Rectangle(this.location.X + 1, this.location.Y, this.location.Width, this.location.Height);

            bool conflictUp = map.isConflicting(up);
            bool conflictDown = map.isConflicting(down);
            bool conflictLeft = map.isConflicting(left);
            bool conflictRight = map.isConflicting(right);

            if (!conflictDown && this.location.Y < this.castle.Location.Y)
            {
                this.location = down;
            }

            else if (!conflictUp && this.location.Y > this.castle.Location.Y)
            {
                this.location = up;
            }


            else if (!conflictRight && this.location.X < this.castle.Location.X)
            {
                this.location = right;
            }
            else if (!conflictLeft && this.location.X > this.castle.Location.X)
            {
                this.location = left;
            }
            else if (this.location.X == this.castle.Location.X)
            {
                if (!conflictRight)
                {
                    this.location = right;
                }
                else if (!conflictLeft)
                {
                    this.location = left;
                }

                else if (!conflictDown)
                {
                    this.location = down;
                }
                else if (!conflictUp)
                { this.location = up; }
            }
            else if (this.location.Y == this.castle.Location.Y)
            {
                if (!conflictDown)
                {
                    this.location = down;
                }
                else if (!conflictUp)
                { this.location = up; }
                else if (!conflictRight)
                {
                    this.location = right;
                }
                else if (!conflictLeft)
                {
                    this.location = left;
                }
            }
            else if (conflictDown && conflictUp && conflictLeft && !conflictRight)
            { this.location = right; }
            else if (conflictDown && conflictUp && !conflictLeft && conflictRight)
            { this.location = left; }
            else if (conflictDown && !conflictUp && conflictLeft && conflictRight)
            { this.location = up; }
            else if (!conflictDown && conflictUp && conflictLeft && conflictRight)
            { this.location = down; }



            if (this.Location.Intersects(this.castle.Location))
            {
                this.Location = tempLocation;
                this.inFiringRange = true;

            }
        }


        //Moves an enemy to the specified point (1 line)
        public void moveTo(int x, int y)
        {
            this.location = new Rectangle(x, y, this.location.Width, this.location.Height);

        }

        public void DrawHealth(Texture2D texture, SpriteBatch spriteBatch)
        {

            Color barColor;
            if (HealthPercentage < 0.30)
                barColor = Color.Red;
            else if (HealthPercentage < 0.70)
                barColor = Color.Yellow;
            else
                barColor = Color.Green;
            Rectangle barLocation = new Rectangle(this.location.X, this.location.Y + this.location.Height, (int)(this.location.Width * HealthPercentage), 20);

            spriteBatch.Draw(texture, barLocation, barColor);

        }

        public void addCastle(Castle c)
        {

            castle = c;

        }



        //Getters and setters for fields in the Enemy class (8 lines)
        public float Health
        {
            get { return this.health; }
            set { this.health = value; }
        }
        public double Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        public String Type
        {
            get { return this.type; }
        }
        public int Gold
        {
            get { return this.gold; }
        }
        public Rectangle Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
        public float MaxHealth
        {
            get { return this.maxHealth; }
            set { this.maxHealth = value; }
        }

        public float HealthPercentage
        {
            get { return (float)health / (float)maxHealth; }
        }

        public Castle Castle
        {
            get { return this.castle; }
            set { this.castle = value; }

        }
        public Map Map
        {
            get { return this.map; }
            set { this.map = value; }
        }
        public int Freezing
        {
            get { return this.freezingStop; }
            set { this.freezingStop = value; }


        }
        public int BurnStop
        {
            get { return this.burnStop; }
            set { this.burnStop = value; }
        }
        public Color Color
        {
            get { return this.c; }
            set { this.c = value; }
        }
        public int Counter
        {
            get { return this.counter; }

        }



    }
}

