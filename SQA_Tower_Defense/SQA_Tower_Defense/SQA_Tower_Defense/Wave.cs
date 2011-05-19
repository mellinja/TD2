using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQA_Tower_Defense
{
    //12 lines
    public class Wave
    {

        Stack<Enemy> waveEnemies;
        int updateTimer;


        //Initializes the wave, which contains a stack of enemies of the same type, haveing "count" enemies in the stack to start with (8 lines)
        public Wave(Enemy enemy, int count)
        {
            updateTimer = 0;
            waveEnemies = new Stack<Enemy>();
            if(null == enemy)
               throw new ArgumentNullException();
            if (count <= 0)
                throw new ArgumentOutOfRangeException();
            for (int i = 0; i < count; i++)
            {
                waveEnemies.Push(new Enemy(enemy.Health,enemy.Speed,enemy.Type,enemy.Gold,enemy.Location));
            }

        }

        public Stack<Enemy> Enemies
        {
            get { return this.waveEnemies; }
        }
        


        //Returns the top enemy on the stack, unless stack is empty (4 lines)
        public Enemy getEnemy()
        {
            if (waveEnemies.Count == 0)
                return null;
            else
                return waveEnemies.Pop();


        }

    }
}
