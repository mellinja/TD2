using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQA_Tower_Defense;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna;
using Microsoft.Xna.Framework.Graphics;

namespace ClassTests
{

    [TestFixture()]
    public class MainGameTests
    {
        MainGame towerGame;
        [SetUp()]
        public void SetUp()
        {
            using (towerGame = new MainGame())
            {
                towerGame.Run();
            }
        }


        [Test()]
        public void GameInitializesSuccessfully()
        {

            Assert.IsNotNull(towerGame);
        }

        [Test()]
        public void LoadContentLoadsMouseStatusSuccessfully()
        {

            Assert.AreEqual(true, towerGame.isMouseVisible);
        }

        [Test()]
        public void LoadContentInitializesSpriteBatchSuccessfully()
        {

            Assert.IsNotNull(towerGame.spriteBatch);
        }

        [Test()]
        public void LoadContentLoadsFontSuccessfully()
        {

            Assert.IsNotNull(towerGame.font);

        }

        [Test()]
        public void LoadContentInitializesEverythingSuccessfully()
        {
            towerGame.LoadContent(1);

            Assert.IsNotNull(towerGame.toolTipFont);
            Assert.AreEqual(25, towerGame.gridSize);
            Assert.AreEqual(0, towerGame.gameTimer);
            Assert.IsNull(towerGame.placingTower);
            Assert.IsNotNull(towerGame.map);
            Assert.IsNotNull(towerGame.towerTex);
            Assert.IsNotNull(towerGame.backTex);
            Assert.AreEqual(1, towerGame.map.Enemies.Count);
            Assert.IsNotNull(towerGame.menu);
            Assert.IsNotNull(towerGame.menu.Background);
            Assert.IsNotNull(towerGame.menu.TowerTex);
            Assert.IsNotNull(towerGame.menu.ToolTipFont);


        }

        [Test()]
        public void Pressing1SelectsNewTower()
        {
            towerGame.LoadContent(1);
            Assert.IsNull(towerGame.placingTower);
            towerGame.boardState = new KeyboardState();
            towerGame.UpdateInput();
            Assert.IsNotNull(towerGame.placingTower);
        }

        [Test()]
        public void Pressing1SelectsCorrectTower()
        {

            towerGame.LoadContent(1);
            Assert.IsNull(towerGame.placingTower);
            Tower temp = towerGame.menu.Towers[0];
            towerGame.boardState = new KeyboardState();
            towerGame.UpdateInput();
            Assert.AreEqual(towerGame.placingTower, temp);


        }

        [Test()]
        public void ClickingNewTowerSelectsNewTower()
        {
            towerGame.LoadContent(1);
            Assert.IsNull(towerGame.placingTower);
            towerGame.mouseState = new MouseState(towerGame.menu.Towers[0].Location.Center.X, towerGame.menu.Towers[0].Location.Center.Y, 0, ButtonState.Pressed, ButtonState.Released,
                                            ButtonState.Released, ButtonState.Released, ButtonState.Released);
            towerGame.UpdateInput();
            Assert.IsNotNull(towerGame.placingTower);

        }

        [Test()]
        public void ClickingNewTowerSelectsCorrectTower()
        {
            towerGame.LoadContent(1);
            Assert.IsNull(towerGame.placingTower);
            Tower temp = towerGame.menu.Towers[0];
            towerGame.mouseState = new MouseState(towerGame.menu.Towers[0].Location.Center.X, towerGame.menu.Towers[0].Location.Center.Y, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            towerGame.UpdateInput();
            Assert.AreEqual(towerGame.placingTower, temp);
        }

        [Test()]
        public void ClickingEmptySpaceWhenSelectingTowerPlacesTower()
        {
            towerGame.LoadContent(1);
            Assert.IsNull(towerGame.placingTower);
            Tower temp = towerGame.menu.Towers[0];
            towerGame.mouseState = new MouseState(towerGame.menu.Towers[0].Location.Center.X, towerGame.menu.Towers[0].Location.Center.Y, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            towerGame.UpdateInput();
            Assert.AreEqual(towerGame.placingTower, temp);
            towerGame.mouseState = new MouseState(10, 10, 0, ButtonState.Pressed, ButtonState.Released,
                    ButtonState.Released, ButtonState.Released, ButtonState.Released);
            int tempMoney = towerGame.map.Money;
            Assert.AreEqual(towerGame.map.Money, tempMoney);
            towerGame.UpdateInput();

            Assert.IsNull(towerGame.placingTower);

            Assert.AreEqual(towerGame.map.Money, tempMoney - temp.Cost);
        }

    }

}
