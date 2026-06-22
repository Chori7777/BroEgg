using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class LevelManager
    {
        private LevelController levelController;
        private ExpSystem expSystem;
        private List<WaveData> waveDataList;
        private int currentWaveIndex;

        //Todas las variables fueron separadas del LevelController (borrar esto luego(Si no se borra no pasa nada tampoco))

        private float spawnTimer = 0;
        private float spawnInterval = 4f;
        private int enemiesPerWave = 4;


        private int enemiesKilled = 0;
        private float waveTimer = 0;


        public int EnemiesKilled => enemiesKilled;
        public int CurrentRound => currentWaveIndex + 1;

       

        public int Timer => (int)waveTimer;

        public ExpSystem ExpSystem => expSystem;

        public WaveData CurrentWaveData => waveDataList[currentWaveIndex];

        public LevelManager(LevelController controller)
        {
            levelController = controller;
            expSystem = new ExpSystem();
            waveDataList = new List<WaveData>();
            currentWaveIndex = 0;
            // Apartado de configuracion super detallada (Y simple a la vez) para las 10 oleadas del increible y popular juego de broegg
            //La informacion la podes checar en Wave data, pero aca esta a mano:


            //ENEMIGOS A MATAR, LA DURACION DE LA OLEADA, EL PORCENTAJE DE ENEMIGOS BASICOS EN LA OLEADA, EL PORCENTAJE DE ENEMIGOS RAPIDOS
            //EL PORCENTAJE DE ENEMIGOS TANQUE Y LOS ENEMIGOS SOLDADO

            //Oleada 1
            waveDataList.Add(new WaveData(20, 20f, 95f, 1f, 10f, 5f,4));
            //Oleada 2
            waveDataList.Add(new WaveData(25, 30f, 80f, 5f, 10f, 0f,4));
            //Oleada 3
            waveDataList.Add(new WaveData(40, 40f, 70f, 5f, 15f, 5f,5));
            //Oleada 4
           /* waveDataList.Add(new WaveData(45, 50f, 70f, 10f, 10f, 10f,5));
            //Oleada 5
            waveDataList.Add(new WaveData(60, 60f, 60f, 15f, 15f, 10f,6));
            //Oleada 6
            waveDataList.Add(new WaveData(65, 70f, 50f, 20f, 25f, 5f,6));
            //Oleada 7
            waveDataList.Add(new WaveData(70, 80f, 45f, 25f, 10f, 20f,7));
            //Oleada 8
            waveDataList.Add(new WaveData(75, 90f, 30f, 30f, 30f, 10f,7));
            //Oleada 9
            waveDataList.Add(new WaveData(90, 90f, 25f, 25f, 25f, 25f,8));
            //Oleada 10
            waveDataList.Add(new WaveData(100, 90f, 15f, 30f, 25f, 25f, 6));*/
            




            // Y bueno se pueden agregar mas 

        }

        public void StartWave()
        {
            enemiesKilled = 0;
            waveTimer = 0;
            spawnTimer = 0;
            SpawnWave();

        }
        public void UpdateWave()
        {
            waveTimer += Program.DeltaTime;
            HandleSpawnTimer();
        }


        // ── Spawn ────────────────────────────────────────────────

        private void HandleSpawnTimer()
        {
            spawnTimer += Program.DeltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnWave();
                spawnTimer = 0;
            }
        }
        private EnemyFactory.TypeEnemy GetRandomEnemyType()
        {
            //Bueno aca se elige un numero aleatorio entre 0 y 1
            float random = (float)Random.Shared.NextDouble();
            float total = CurrentWaveData.BasicChance + CurrentWaveData.FastChance + CurrentWaveData.TankChance + CurrentWaveData.SoldierChance;
            float value = random * total;

            if (value < CurrentWaveData.BasicChance)
                return EnemyFactory.TypeEnemy.BasicEnemy;

            else if (value < CurrentWaveData.BasicChance + CurrentWaveData.FastChance)
                return EnemyFactory.TypeEnemy.FastEnemy;

            else if (value < CurrentWaveData.BasicChance + CurrentWaveData.FastChance + CurrentWaveData.TankChance)
                return EnemyFactory.TypeEnemy.TankEnemy;

            else
                return EnemyFactory.TypeEnemy.SoldierEnemy;
        }
    
        private void SpawnWave()
        {
            for (int i = 0; i < CurrentWaveData.EnemiesPerRound; i++)
            {
                int x = Random.Shared.Next(100, 1200);
                int y = Random.Shared.Next(100, 700);

                var enemyType = GetRandomEnemyType();

                Enemy enemy = EnemyFactory.CreateEnemy(enemyType, x, y, currentWaveIndex + 1);

                enemy.OnEnemyDied += OnEnemyKilled;                      // suma kills y da EXP
                enemy.OnEnemyDied += levelController.HandleEnemyDied;    // chequea condición de victoria

                levelController.GameObjectsList.Add(enemy);
            }
            
        }
        //  ────────────  Asesinato  ────────────────────────────────────────────────
        public void OnEnemyKilled()
        {
            enemiesKilled++;
            expSystem.AddExp(20f);
        }
        public bool IsWaveComplete()
        {
            return enemiesKilled >= CurrentWaveData.EnemiesToKill || waveTimer >= CurrentWaveData.WaveTime;
        }

        public void NextLevel()
        {
            currentWaveIndex++;
        }

        public void ClearEnemies()
        {
            for (int i = levelController.GameObjectsList.Count - 1; i >= 0; i--)
            {
                if (levelController.GameObjectsList[i] is Enemy)
                {
                    levelController.GameObjectsList.RemoveAt(i);
                }
            }
        }

        public bool HasWon()
        {
            return currentWaveIndex >= waveDataList.Count - 1 && IsWaveComplete();
        }
    }
}
