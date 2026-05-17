using System.Runtime.Intrinsics.X86;

namespace ProyectoSDL2.Engine.Scripts
{
    public class LevelController
    {
        private Player player;

        private List<GameObject> gameObjectsList = new List<GameObject>();
        public List<GameObject> GameObjectsList => gameObjectsList;

        private float spawnTimer = 0;
        private float spawnInterval = 4f;
        private int enemiesPerWave = 4;

        private int enemiesKilled = 0;
        private int enemiesToWin = 20;

        private int playerWidth = 64; private int playerHeight = 64;
        private int enemyWidth = 64; private int enemyHeight = 64;

        public Player Player => player;
        public void Start()
        {
            player = new Player(500, 400, playerWidth, playerHeight);
            SpawnWave();
        }

        public void Update()
        {
            player.Update();

            UpdateGameObjects();

            CheckCollisions();
            HandleSpawnTimer();
            CleanupDestroyedObjects();
        }

        public void Render()
        {
            Engine.Clear();

            player.Render();

            for (int i = 0; i < gameObjectsList.Count; i++)
            {
                gameObjectsList[i].Render();
            }

            Engine.Show();
        }

        // ── Spawn ────────────────────────────────────────────────

        private void HandleSpawnTimer() //reloj del spawner
        {
            spawnTimer += Program.DeltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnWave();
                spawnTimer = 0;
            }
        }

        private void SpawnWave()
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                int x = 100 + i * 120;
                int y = 100;
                gameObjectsList.Add(new Enemy(x, y, enemyWidth, enemyHeight)); // spawnea enemigos
            }
        }

        // ── Update helpers ───────────────────────────────────────

        private void UpdateGameObjects()
        {
            for (int i = 0; i < gameObjectsList.Count; i++)
            {
                gameObjectsList[i].Update();
            }
        }

        // ── Colisiones ───────────────────────────────────────────

        private void CheckCollisions()
        {
            CheckBulletsVsEnemies();
            CheckEnemiesVsPlayer();
        }
        private void CleanupDestroyedObjects() //metodo clave para limpiar ciclos for y que no haya overflow
        {
            // Acá sí recorremos de atrás hacia adelante para borrar de forma segura
            for (int i = gameObjectsList.Count - 1; i >= 0; i--)
            {
                if (gameObjectsList[i].IsPendingDestroy)
                {
                    gameObjectsList.RemoveAt(i);
                }
            }
        }
        private void CheckBulletsVsEnemies()
        {
            for (int i = 0; i < gameObjectsList.Count; i++) //recorremos la lista de GameObjects
            {
                GameObject bulletObject = gameObjectsList[i]; //guardamos el GameObjectActual en una veriable de tipo GameObject

                
                if (bulletObject.IsPendingDestroy || !(bulletObject is Bullet)) continue;// Si ya está marcada para destruir
                                                                                         // o no es una bala, pasamos de largo

                Bullet bullet = (Bullet)bulletObject; //convretimos ese GameObject en una bala posta
                                                      //(para acceder a sus metodos)

                for (int j = 0; j < gameObjectsList.Count; j++)
                {
                    GameObject enemyObject = gameObjectsList[j]; //recorremos por segunda vez para ver enemigos


                    if (enemyObject.IsPendingDestroy || !(enemyObject is Enemy)) continue;
                    Enemy enemy = (Enemy)enemyObject; //convretimos ese GameObject en un enemigo posta

                    // Evaluamos la colisión
                    if (bullet.Overlaps(enemyObject.Transform))
                    {
                        enemy.Health.GetDamaged();

                        // En vez de borrar en caliente, los MARCAMOS
                        bulletObject.IsPendingDestroy = true;

                        if (enemy.Health.IsDead())
                        {
                            enemyObject.IsPendingDestroy = true;
                            enemiesKilled++;
                            CheckWinCondition();
                        }

                        // Como la bala ya chocó y se va a destruir, rompemos el bucle J 
                        // para evaluar la siguiente bala del bucle I
                        break;
                    }
                }
            }
        }

        private void CheckEnemiesVsPlayer()
        {
            for (int i = gameObjectsList.Count - 1; i >= 0; i--)
            {
                GameObject enemyObject = gameObjectsList[i];

                if (enemyObject.Transform.Overlaps(player.Transform) && enemyObject is Enemy)
                {
                    player.Health.GetDamaged();
                    gameObjectsList.RemoveAt(i);
                }
            }
        }

        private void CheckWinCondition()
        {
            if (enemiesKilled >= enemiesToWin)
            {
                GameManager.Instance.ChangeGameState(GAME_STATE.WIN);
            }
        }

       
        public void AddBullet(Bullet bullet)
        {
            gameObjectsList.Add(bullet);
        }
    }
}