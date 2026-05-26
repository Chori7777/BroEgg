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
        private float moveSpeedEnemy;
        private int armorEnemy;

        //Gets

        public int HpEnemy => hpEnemy;
        public int DmgEnemy => dmgEnemy;
        public float MoveSpeedEnemy => moveSpeedEnemy;
        public int ArmorEnemy => armorEnemy;

       public EnemyStats(int hpEnemy, int dmgEnemy, float moveSpeedEnemies, int armorEnemies)
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
