using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class EnemyStats
    {
        //Stats

        private int hpEnemy;
        private int dmgEnemy;
        private int moveSpeedEnemy;
        private int armorEnemy;

        //Gets

        public int HpEnemy => hpEnemy;
        public int DmgEnemy => dmgEnemy;
        public int MoveSpeedEnemy => moveSpeedEnemy;
        public int ArmorEnemy => armorEnemy;

       public EnemyStats(int hpEnemy, int dmgEnemy, int moveSpeedEnemies, int armorEnemies)
        {
            this.hpEnemy = hpEnemy;
            this.dmgEnemy = dmgEnemy;
            this.moveSpeedEnemy = moveSpeedEnemies;
            this.armorEnemy = armorEnemies;
        }

        public void GetDamaged(int amount = 1)
        {
            hpEnemy -= amount;
        }

        public bool IsDead()
        {
            return hpEnemy <= 0;
        }
    }
}
