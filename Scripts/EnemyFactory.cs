using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public static class EnemyFactory
    {
       public enum TypeEnemy{ TankEnemy,FastEnemy,BasicEnemy,SoldierEnemy}
       

       public static Enemy CreateEnemy(TypeEnemy enemy)
        {
            switch(enemy)
            {
                case TypeEnemy.TankEnemy:
                    return new Enemy(200, 100, 100, 100);
                case TypeEnemy.BasicEnemy:
                    return new Enemy(200, 100, 100, 100);
                case TypeEnemy.SoldierEnemy:
                    return new Enemy(200, 100, 100, 100);
                case TypeEnemy.FastEnemy:
                    return new Enemy(200, 100, 100, 100);
            }

            return null;
       }

        public class WaveOne
        {
            
        }
    }
}
