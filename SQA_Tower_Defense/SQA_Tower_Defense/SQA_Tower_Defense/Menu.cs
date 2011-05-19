using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SQA_Tower_Defense
{
    public class Menu
    {
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D texture;
        Rectangle baseStart, subMenuStart;
        List<String> subOptions;
        List<Menu> subMenus;
        List<Rectangle> subMenusRecs, subOptionsRecs;
        int textHeight;
        bool isVisible;
        String title;
        Interface interfaces;
        Map map;
        MainGame game;
        public Menu(String title, Point baseStartingLocation, Texture2D texture, SpriteFont font, SpriteBatch spriteBatch)
        {


            this.texture = texture;
            this.font = font;
            this.title = title;
            textHeight = (int)font.MeasureString("Main Menu").Y;
            baseStart = new Rectangle(baseStartingLocation.X, baseStartingLocation.Y, (int)font.MeasureString("Main Menu").X, (int)font.MeasureString("Main Menu").Y);
            subMenuStart = new Rectangle(baseStart.X, baseStart.Y, baseStart.Width, baseStart.Height);
            this.spriteBatch = spriteBatch;

            subOptions = new List<string>();
            subMenus = new List<Menu>();
            subMenusRecs = new List<Rectangle>();
            subOptionsRecs = new List<Rectangle>();
            this.isVisible = false;


        }

        public void Draw()
        {



            if (interfaces.Language == "En" && baseStart.Width != 300)
            {
                baseStart.Width = 300;



                for (int i = 0; i < subMenusRecs.Count; i++)
                {
                    subMenusRecs[i] = new Rectangle(subMenusRecs[i].X, subMenusRecs[i].Y, 300, subMenusRecs[i].Height);

                }

                for(int i = 0; i < subOptionsRecs.Count;i++)
                {
                    subOptionsRecs[i] = new Rectangle(subOptionsRecs[i].X, subOptionsRecs[i].Y, 300, subOptionsRecs[i].Height);

                }
            }
            if(interfaces.Language == "Sp" && baseStart.Width != 500)
            {
                baseStart.Width = 500;



                for (int i = 0; i < subMenusRecs.Count; i++)
                {
                    subMenusRecs[i] = new Rectangle(subMenusRecs[i].X, subMenusRecs[i].Y, 500, subMenusRecs[i].Height);

                }

                for (int i = 0; i < subOptionsRecs.Count; i++)
                {
                    subOptionsRecs[i] = new Rectangle(subOptionsRecs[i].X, subOptionsRecs[i].Y, 500, subOptionsRecs[i].Height);

                }


            }

            spriteBatch.Draw(texture, baseStart, Color.White);
            spriteBatch.DrawString(font, interfaces.LM.getTranslation(title, interfaces.Language), new Vector2(baseStart.X + (0.5f * baseStart.Width) - font.MeasureString(interfaces.LM.getTranslation(title, interfaces.Language)).X / 2, baseStart.Y), Color.Red);


            for (int i = 0; i < subOptionsRecs.Count; i++)
            {
                spriteBatch.Draw(texture, subOptionsRecs[i], Color.Red);
                
                
                spriteBatch.DrawString(font, interfaces.LM.getTranslation(subOptions[i], interfaces.Language), new Vector2(subOptionsRecs[i].X + (0.5f * subOptionsRecs[i].Width) - font.MeasureString(interfaces.LM.getTranslation(subOptions[i], interfaces.Language)).X / 2, subOptionsRecs[i].Y), Color.White);
            }
            
            
            for (int i = 0; i < subMenusRecs.Count; i++)
            {
                spriteBatch.Draw(texture, subMenusRecs[i], Color.Blue);
                spriteBatch.DrawString(font, interfaces.LM.getTranslation(subMenus[i].title, interfaces.Language), new Vector2(subMenusRecs[i].X + (0.5f * subMenusRecs[i].Width) - font.MeasureString(interfaces.LM.getTranslation(subMenus[i].title, interfaces.Language)).X / 2, subMenusRecs[i].Y), Color.White);
            }


        }

        public void RegisterClick(Point location)
        {
            for (int i = 0; i < subOptionsRecs.Count; i++)
            {
                if (subOptionsRecs[i].Contains(location))
                    DoobieDoobieDo(subOptions[i]);
            }
            for (int i = 0; i < subMenusRecs.Count; i++)
            {
                if (subMenusRecs[i].Contains(location))
                    subMenus[i].Visible = !subMenus[i].Visible;
            }
        }

        void DoobieDoobieDo(String command)
        {
            switch (command)
            {
                case "Back":
                    this.Visible = false;
                    break;
                case "Backo":
                    this.Visible = false;
                    break;
                case "English":
                    this.interfaces.Language = "En";
                    break;
                case "Ingles":
                    this.interfaces.Language = "En";
                    break;
                case "Spanish":
                    this.interfaces.Language = "Sp";
                    break;
                case "Lower Difficulty":
                    if (map.Difficulty > 1)
                        map.Difficulty--;
                    break;
                case "Raise Difficulty":
                    if (map.Difficulty < 3)
                        map.Difficulty++;
                    break;
                case "Exit Game":
                    game.Exit();
                    break;
                case "Resume":
                    this.Visible = false;
                    break;
                


            }
        }


        public void AddSubMenu(Menu menu)
        {

            subMenus.Add(menu);

            subMenuStart = new Rectangle(subMenuStart.X, subMenuStart.Y + textHeight, baseStart.Width, subMenuStart.Height);
            subMenusRecs.Add(subMenuStart);

        }

        public void AddSubOption(String option)
        {
            subOptions.Add(option);

            //baseStart = new Rectangle(baseStart.X, baseStart.Y, baseStart.Width > (int)font.MeasureString(option).X ? baseStart.Width : (int)font.MeasureString(option).X, baseStart.Height + 20);
            subMenuStart = new Rectangle(subMenuStart.X, subMenuStart.Y + textHeight, baseStart.Width, subMenuStart.Height);
            subOptionsRecs.Add(subMenuStart);

        }

        public List<String> SubOptions
        {
            get { return this.subOptions; }
            set { this.subOptions = value; }
        }
        public List<Menu> SubMenus
        {
            get { return this.subMenus; }
            set { this.subMenus = value; }
        }

        public bool Visible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public Rectangle Area
        {
            get { return this.baseStart; }
        }

        public String Title
        {
            get { return this.title; }
        }

        public Interface LanguageManager
        {
            get { return this.interfaces; }
            set { this.interfaces = value; }

        }

        public Map Map
        {
            get { return this.map; }
            set { this.map = value; }
        }

        public Interface Interface
        {
            set { this.interfaces = value; }
        }
        public MainGame Game
        {
            get { return this.game; }
            set { this.game = value; }

        }

    }
}
