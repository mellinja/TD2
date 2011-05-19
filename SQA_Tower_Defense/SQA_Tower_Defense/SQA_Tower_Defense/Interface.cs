using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SQA_Tower_Defense
{
    public class Interface
    {

        GraphicsDeviceManager graphics;
        Rectangle background;
        Color color;
        List<Tower> TowerTypes;
        Texture2D TowerTextures;
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        SpriteFont font;
        SpriteFont tipFont;
        LanguageManager lm;
        string language;


        public Interface(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font)
        {
            this.graphics = graphics;
            this.background = new Rectangle(0, graphics.PreferredBackBufferHeight - graphics.PreferredBackBufferHeight / 5, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight / 5);
            this.color = Color.Black;


            this.TowerTypes = new List<Tower>();
            Rectangle tower = new Rectangle(background.X + 20, background.Y + 5, 50, 50);
            TowerTypes.Add(new Tower("Basic", 10, 50, 10, 100, tower));
            TowerTypes.Add(new Tower("Basic", 10, 15, 15, 100, new Rectangle(tower.X + 100, tower.Y, tower.Width, tower.Height)));

            this.spriteBatch = spriteBatch;
            this.font = font;
            lm = new LanguageManager();
            language = "En";

        }

        public void Draw(Map.SaveState info)
        {
            spriteBatch.Draw(backgroundTexture, this.background, Color.White);

            foreach (Tower tower in TowerTypes)
            {
                spriteBatch.Draw(TowerTextures, tower.Location, Color.White);
            }

            Vector2 textLocation = new Vector2(graphics.PreferredBackBufferWidth - 300, graphics.PreferredBackBufferHeight * 4 / 5);
            spriteBatch.DrawString(font, lm.getTranslation("gold", language) + ": " + info.money, textLocation, Color.Red);
            spriteBatch.DrawString(font, lm.getTranslation("Speed", language) + ": " + info.score, new Vector2(textLocation.X, textLocation.Y + 25), Color.Red);

        }

        public void DisplayToolTip(Enemy enemy, Vector2 position)
        {
            Rectangle tipBackground = new Rectangle((int)position.X, (int)position.Y - 70, 140, 80);
            spriteBatch.Draw(backgroundTexture, tipBackground, new Color(0, 0, 0, 100));




            spriteBatch.DrawString(tipFont, enemy.Type + " " + lm.getTranslation("enemy", language), new Vector2(tipBackground.X, tipBackground.Y), Color.White);
            spriteBatch.DrawString(tipFont, lm.getTranslation("Health", language) + ": " + enemy.HealthPercentage * 100 + "%" + "\n" + lm.getTranslation("Speed", language) + ": " + enemy.Speed, new Vector2(tipBackground.X, tipBackground.Y + 15), Color.White);
            //spriteBatch.DrawString(tipFont, "Speed: " + enemy.Speed, new Vector2(tipBackground.X, tipBackground.Y + 30), Color.White);

        }

        public void DisplayToolTip(Tower tower, Vector2 position)
        {
            Rectangle tipBackground = new Rectangle((int)position.X, (int)position.Y - 70, 180, 80);
            spriteBatch.Draw(backgroundTexture, tipBackground, new Color(0, 0, 0, 100));

            for (int i = tower.Location.Center.X - tower.Range; i < tower.Location.Center.X + tower.Range; i++)
            {
                for (int j = tower.Location.Center.Y - tower.Range; j < tower.Location.Center.Y + tower.Range; j++)
                {
                    if (tower.DistanceFrom(new Vector2(i, j)) < tower.Range)
                    {
                        spriteBatch.Draw(backgroundTexture, new Rectangle(i, j, 1, 1), new Color(50, 1, 1, 100));
                    }
                }


            }

            // spriteBatch.DrawString(tipFont, tower.Name + lm.getTranslation("Tower", language), new Vector2(tipBackground.X, tipBackground.Y), Color.White);
            spriteBatch.DrawString(tipFont, lm.getTranslation("Damage", language) + ": " + tower.AttackDamage, new Vector2(tipBackground.X, tipBackground.Y + 15), Color.White);
            spriteBatch.DrawString(tipFont, lm.getTranslation("Sell for", language) + " " + tower.Cost * 3 / 4 + " " + lm.getTranslation("gold", language) + ": ", new Vector2(tipBackground.X, tipBackground.Y + 30), Color.White);
        }

        public void DisplayRewind(SpriteFont font)
        {
            spriteBatch.DrawString(font, "< " + lm.getTranslation("Rewinding", language) + "... >", new Vector2(graphics.PreferredBackBufferWidth / 2 - graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 2 - graphics.PreferredBackBufferHeight / 8), Color.White);

        }

        public LanguageManager LM
        {
            get { return this.lm; }

        }
        public Texture2D Background
        {
            get { return this.backgroundTexture; }
            set { this.backgroundTexture = value; }
        }

        public Texture2D TowerTex
        {
            get { return this.TowerTextures; }
            set { this.TowerTextures = value; }
        }


        public List<Tower> Towers
        {
            get { return this.TowerTypes; }
        }


        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public SpriteFont ToolTipFont
        {
            get { return this.tipFont; }
            set { this.tipFont = value; }
        }
        public string Language
        {
            get { return this.language; }
            set { this.language = value; }
        }





    }
}
