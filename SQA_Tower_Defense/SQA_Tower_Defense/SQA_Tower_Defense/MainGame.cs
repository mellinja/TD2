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
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Tower placingTower;
        public Texture2D towerTex, backTex;
        public SpriteFont font, toolTipFont, bigFont;

        public Interface menu;

        public MouseState mouseState, previousMouseState;
        public KeyboardState boardState, previousKeyboardState;

        public int gridSize;

        public bool rewindingTime;
        public int gameTimer;

        public Map map;

        public List<Menu> menus;

        public Menu main, options, language, difficulty;


        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1200;
            graphics.PreferredBackBufferWidth = 1920;
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        public void Initialize(int i)
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }

        public void LoadContent(int i)
        {
            this.LoadContent();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            this.IsMouseVisible = true;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            toolTipFont = Content.Load<SpriteFont>("tooltipfont");
            bigFont = Content.Load<SpriteFont>("bigFont");
            gridSize = 50;
            gameTimer = 0;
            placingTower = null;
            map = new Map("normal", 100, 2);

            towerTex = Content.Load<Texture2D>("Sprites\\Eiffel");
            backTex = Content.Load<Texture2D>("Sprites\\Blank");
            Enemy e = new Enemy(100, 10f, "basic", 10, new Rectangle(50, 900, 50, 50));
            e.Map = map;
            map.setStandardEnemy(e.Clone());
            map.Enemies.Add(e);
            Castle c = new Castle(1000000000, new Rectangle(1500, 200, 40, 40));
            map.PlaceCastle(c);
            this.menu = new Interface(this.graphics, this.spriteBatch, this.font);
            this.menu.Background = backTex;
            this.menu.TowerTex = towerTex;
            this.menu.ToolTipFont = toolTipFont;


            menus = new List<Menu>();
            this.main = new Menu("Main Menu", new Point(0, 0), backTex, font, spriteBatch);
            this.main.Interface = menu;
            main.AddSubOption("Resume");
            main.AddSubOption("Exit Game");

            main.Game = this;
            this.options = new Menu("Options", new Point(0, 250), backTex, font, spriteBatch);
            this.options.Interface = menu;
            this.difficulty = new Menu("Difficulty", new Point(0, 500), backTex, font, spriteBatch);
            this.difficulty.Interface = menu;
            this.difficulty.AddSubOption("Raise Difficulty");
            this.difficulty.AddSubOption("Lower Difficulty");
            this.difficulty.AddSubOption("Back");

            difficulty.Map = this.map;

            this.language = new Menu("Languages", new Point(0, 750), backTex, font, spriteBatch);
            this.language.Interface = menu;
            this.language.AddSubOption("Spanish");
            this.language.AddSubOption("English");
            this.language.AddSubOption("Back");


            options.AddSubMenu(difficulty);
            options.AddSubMenu(language);

            this.options.AddSubOption("Back");

            main.AddSubMenu(options);




            menus.Add(main);
            menus.Add(options);
            menus.Add(language);
            menus.Add(difficulty);
            Enemy e2 = e.Clone();
            //Path creation using towers
            for (int i = 0; i <= Math.Abs(e2.Location.Y - c.Location.Y) / 50; i++)
            {
                Tower t = new Tower("path",10,0,0,1,new Rectangle(50, 900 - Math.Sign(e2.Location.Y - c.Location.Y) * i * 50, 50, 50));
                map.PlaceTower(t);
            }
            for (int i = 0; i < Math.Abs(e2.Location.X - c.Location.X) / 50; i++)
            {
                Tower t = new Tower("path", 10, 0, 0, 1, new Rectangle(50 - Math.Sign(e2.Location.X - c.Location.X) * i * 50,c.Location.Y, 50, 50));
                map.PlaceTower(t);
            }

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void Update()
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            UpdateInput();

            if (!main.Visible)
            {
                foreach (Enemy e in map.Enemies)
                    e.Update();

                foreach (Tower t in map.Towers)
                    t.Update();
                map.Update();
            }
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            this.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Mouse Location: " + mouseState.X + "," + mouseState.Y, new Vector2(0, 0), Color.White);

            if (placingTower != null)
                spriteBatch.DrawString(font, "Tower Location: " + placingTower.Location.X + ", " + placingTower.Location.Y, new Vector2(0, 20), Color.White);


            #region Draw Towers and Enemies

            foreach (Tower tower in map.Towers)
            {
                spriteBatch.Draw(towerTex, tower.Location, tower.Color);
            }
            foreach (Enemy enemy in map.Enemies)
            {
                spriteBatch.Draw(towerTex, enemy.Location, enemy.Color);
                enemy.DrawHealth(backTex, spriteBatch);
            }
            if (placingTower != null)
                spriteBatch.Draw(towerTex, placingTower.Location, placingTower.Color);

            #endregion

            #region Display Tooltips
            bool skip = false;
            foreach (Enemy enemy in map.Enemies)
            {
                if (enemy.Location.Contains(mouseState.X, mouseState.Y))
                {

                    menu.DisplayToolTip(enemy, new Vector2(mouseState.X, mouseState.Y));
                    skip = true;
                    break;
                }
            }
            if (!skip)
            {
                foreach (Tower tower in map.Towers)
                {
                    if (tower.Location.Contains(mouseState.X, mouseState.Y))
                    {
                        menu.DisplayToolTip(tower, new Vector2(mouseState.X, mouseState.Y));
                        break;
                    }

                }
            }

            foreach (Tower tower in menu.Towers)
            {
                if (tower.Location.Contains(mouseState.X, mouseState.Y))
                {
                    menu.DisplayToolTip(tower, new Vector2(mouseState.X, mouseState.Y));
                    break;
                }

            }



            #endregion

            #region Display Other Text

            if (rewindingTime)
            {
                menu.DisplayRewind(bigFont);

            }
            else
            {
                menu.Draw(map.SaveStates[map.SaveStates.Count - 1]);

            }
            #endregion


            if (main.Visible)
            {
                main.Draw();

                if (options.Visible)
                    options.Draw();
                if (language.Visible)
                    language.Draw();
                if (difficulty.Visible)
                {
                    difficulty.Draw();
                    string diff = "Normal";
                    if(map.Difficulty == 1)
                        diff = "Easy";
                    else if(map.Difficulty == 3)
                        diff = "Hard";
                    spriteBatch.DrawString(font, "Currently on " + diff, new Vector2(difficulty.Area.X + difficulty.Area.Width, difficulty.Area.Y), Color.Red);
                }
            }
            else
            {
                difficulty.Visible = false;
                options.Visible = false;
                language.Visible = false;
            }


            foreach (Tower tower in menu.Towers)
            {
                if (tower.Location.Contains(mouseState.X, mouseState.Y))
                {
                    menu.DisplayToolTip(tower, new Vector2(mouseState.X, mouseState.Y));
                    break;
                }

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }


        public void UpdateInput()
        {
            mouseState = Mouse.GetState();
            boardState = Keyboard.GetState();

            if (placingTower != null)
            {

                placingTower.Location = new Rectangle(mouseState.X - (mouseState.X % gridSize), mouseState.Y - (mouseState.Y % gridSize), placingTower.Location.Width, placingTower.Location.Height);

                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    for (int i = 0; i < map.Towers.Count; i++)
                    {

                    }
                    map.PlaceTower(new Tower(placingTower.Name, placingTower.Health, placingTower.AttackDamage, placingTower.Cost, placingTower.Range, placingTower.Location));

                    placingTower = null;
                }



            }
            else
            {
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    foreach (Tower tower in menu.Towers)
                    {
                        if (tower.Location.Contains(mouseState.X, mouseState.Y))
                            placingTower = tower.Clone();
                    }
                }

            }

            if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                for (int i = 0; i < map.Towers.Count; i++)
                {
                    if (map.Towers[i].Location.Contains(mouseState.X, mouseState.Y))
                    {
                        map.SellTower(map.Towers[i]);
                    }
                }
            }

            #region Selecting towers with Keyboard Input

            if (boardState.IsKeyDown(Keys.D1) && previousKeyboardState.IsKeyUp(Keys.D1))
            {
                Tower temp = menu.Towers[0];
                placingTower = new Tower(temp.Name, temp.Health, temp.AttackDamage, temp.Cost, temp.Range, new Rectangle(mouseState.X - (mouseState.X % gridSize), mouseState.Y - (mouseState.Y % gridSize), 50, 50));
            }

            if (boardState.IsKeyDown(Keys.D2) && previousKeyboardState.IsKeyUp(Keys.D2))
            {
                Tower temp = menu.Towers[1];
                placingTower = new Tower(temp.Name, temp.Health, temp.AttackDamage, temp.Cost, temp.Range, new Rectangle(mouseState.X - (mouseState.X % gridSize), mouseState.Y - (mouseState.Y % gridSize), 50, 50));

            }

            #endregion

            if (boardState.IsKeyUp(Keys.LeftAlt))
            {
                rewindingTime = false;
                map.Rewinding = false;
                map.SaveNextState();
            }
            if (boardState.IsKeyDown(Keys.LeftAlt))
            {
                map.Rewinding = true;
                rewindingTime = true;
                map.LoadPreviousState();

            }


            #region Menu interaction

            if (boardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
            {

                main.Visible = !main.Visible;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {

                foreach (Menu item in menus)
                {
                    if (item.Visible)
                        item.RegisterClick(new Point(mouseState.X, mouseState.Y));

                }



            }


            #endregion



            previousMouseState = mouseState;
            previousKeyboardState = boardState;



        }

        public bool isMouseVisible
        {
            get { return this.IsMouseVisible; }
        }



    }
}

