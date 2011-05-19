using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SQA_Tower_Defense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Castle
    {
        float health;
        Rectangle location;
        string image;
        public Castle(float Health, Rectangle rec)
        {

            if (Health <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (0 == rec.Width || 0 == rec.Height)
            {
                throw new ArgumentNullException();
            }

            this.health = Health;
            this.location = rec;
            this.image = "castle";

        }

        public float Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public Rectangle Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        public void takeDamage(int i)
        {
            this.health -= i;

        }

        public string CastleImage
        {
            get { return this.image; }

        }

    }
}
