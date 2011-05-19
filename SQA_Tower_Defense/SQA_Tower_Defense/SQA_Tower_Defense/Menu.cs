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

        public Menu(String title, Point baseStartingLocation, Texture2D texture, SpriteFont font, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.font = font;
            this.title = title;
            textHeight = (int)font.MeasureString("Main Menu").Y;
            baseStart = new Rectangle(baseStartingLocation.X, baseStartingLocation.Y, (int)font.MeasureString("Main Menu").X, (int)font.MeasureString("Main Menu").Y);
            subMenuStart = new Rectangle(baseStart.X, baseStart.Y + textHeight, baseStart.Width, baseStart.Height);
            this.spriteBatch = spriteBatch;

            subOptions = new List<string>();
            subMenus = new List<Menu>();
            subMenusRecs = new List<Rectangle>();
            subOptionsRecs = new List<Rectangle>();
            this.isVisible = false;


        }

        public void Draw()
        {

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
                    subMenus[i].Visible = true;
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



            }
        }


        public void AddSubMenu(Menu menu)
        {

            subMenus.Add(menu);

            baseStart = new Rectangle(baseStart.X, baseStart.Y, baseStart.Width > (int)font.MeasureString(menu.title).X ? baseStart.Width : (int)font.MeasureString(menu.title).X, baseStart.Height + 20);
            subMenuStart = new Rectangle(subMenuStart.X, subMenuStart.Y + textHeight, baseStart.Width, subMenuStart.Height);
            subMenusRecs.Add(subMenuStart);

        }

        public void AddSubOption(String option)
        {
            subOptions.Add(option);

            baseStart = new Rectangle(baseStart.X, baseStart.Y, baseStart.Width > (int)font.MeasureString(option).X ? baseStart.Width : (int)font.MeasureString(option).X, baseStart.Height + 20);
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


    }
}
