using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class WaveData
    {
        public int EnemiesToKill { get; private set; }
        public float WaveTime { get; private set; }
        public float BasicChance { get; private set; }
        public float FastChance { get; private set; }
        public float TankChance { get; private set; }
        public float SoldierChance { get; private set; }
        public int EnemiesPerRound { get; private set; }

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
