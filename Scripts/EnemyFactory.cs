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
       
        
       public static Enemy CreateEnemy(TypeEnemy enemy, int x , int y, int wave)
        {
            switch(enemy)
            {
                case TypeEnemy.TankEnemy:

                    List<Image> tankFrames = new List<Image>();
                    tankFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_0.png"));
                    tankFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_1.png"));
                    tankFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_2.png"));
                    tankFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_3.png"));

                    return new Enemy(x, y, 100, 100, tankFrames, new EnemyStats(
                        hpEnemy:     10 + (5 * wave),
                        dmgEnemy:    2  + (1 * wave),
                        moveSpeedEnemies:   2,
                        armorEnemies:  2  + (2 * wave)  
                      ));

                case TypeEnemy.BasicEnemy:

                    List<Image> basicFrames = new List<Image>();
                    basicFrames.Add(Engine.LoadImage("assets/enemy/BasicEnemy_0.png"));
                    basicFrames.Add(Engine.LoadImage("assets/enemy/BasicEnemy_1.png"));
                    basicFrames.Add(Engine.LoadImage("assets/enemy/BasicEnemy_2.png"));
                    basicFrames.Add(Engine.LoadImage("assets/enemy/BasicEnemy_3.png"));

                    return new Enemy(x, y, 100, 100, basicFrames, new EnemyStats(
                         hpEnemy: 2 + (2 * wave),
                         dmgEnemy: 1 + (1 * wave),
                         moveSpeedEnemies: 2,
                         armorEnemies: 1 + (1 * wave)
                       ));

                case TypeEnemy.SoldierEnemy:

                    List<Image> soldierFrames = new List<Image>();
                    soldierFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_0.png"));
                    soldierFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_1.png"));
                    soldierFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_2.png"));
                    soldierFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_3.png"));

                    return new Enemy(x, y, 100, 100,soldierFrames, new EnemyStats(
                        hpEnemy: 4 + (1 * wave),
                        dmgEnemy: 3 + (1 * wave),
                        moveSpeedEnemies: 4,
                        armorEnemies: 1 + (1 * wave)
                      ));

                case TypeEnemy.FastEnemy:

                    List<Image> fastFrames = new List<Image>();
                    fastFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_0.png"));
                    fastFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_1.png"));
                    fastFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_2.png"));
                    fastFrames.Add(Engine.LoadImage("assets/enemy/TankEnemy_3.png"));

                    return new Enemy(x, y, 100, 100, fastFrames,  new EnemyStats(
                        hpEnemy: 3 + (2 * wave),
                        dmgEnemy: 2 + (1 * wave),
                        moveSpeedEnemies: 8,
                        armorEnemies: 1 + (1 * wave)
                      ));
            }

            return null;
       }

       
    }
}
