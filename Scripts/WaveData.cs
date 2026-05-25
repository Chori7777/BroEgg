using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class WaveData
    {
        public int EnemiesToKill;
        public float WaveTime;
        public float BasicChance;
        public float FastChance;
        public float TankChance;
        public float SoldierChance;
        public int EnemiesPerRound;

        public WaveData(int enemies, float time, float basic, float fast, float tank, float soldier,int enemiesPerWave)
        {
            EnemiesToKill = enemies;
            WaveTime = time;
            BasicChance = basic;
            FastChance = fast;
            TankChance = tank;
            SoldierChance = soldier;
            EnemiesPerRound = enemiesPerWave;

        }
    }
}
