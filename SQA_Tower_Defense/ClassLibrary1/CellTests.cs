
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQA_Tower_Defense;
using Microsoft.Xna.Framework;


namespace ClassTests
{
    [TestFixture()]
    class CellTests
    {
        Cell cell;

        [Test]
        public void CellInitializes()
        {
            Cell cell = new Cell();
            Assert.IsNotNull(cell);
        }

        [Test]
        public void CellHasOccupied()
        {
            Cell cell = new Cell();
            cell.Occupy(new Enemy(10, 4, "basic", 4, new Rectangle(1, 1, 1, 1)));
            Assert.IsTrue(cell.isOccupied());
        }


    }
}
