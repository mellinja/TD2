using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQA_Tower_Defense
{

    //Cell class holds units for path finding algorithms (4 lines)
    public class Cell
    {
        Boolean Occupied;
        Enemy enemy;


        //Constructes and empty cell (1 line)
        public Cell()
        {
            Occupied = false;
        }


        //sets the cell to occupied status, holding enemy e (2 lines)
        public void Occupy(Enemy e)
        {
            Occupied = true;
            enemy = e;
        }


        //Returns whether or not the cell is occupied (1 line)
        public Boolean isOccupied()
        {
            return Occupied;
        }



    }
}