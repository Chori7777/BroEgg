namespace ProyectoSDL2.Engine.Scripts
{
    public class LevelController
    {
        private Player player;
        private List<Enemy> enemyList = new List<Enemy>();
        private List<Bullet> bulletList = new List<Bullet>();
        public List<Enemy> EnemyList => enemyList;

        private float spawnTimer = 0;
        private float spawnInterval = 4f;
        private int enemiesPerWave = 4;

        private int enemiesKilled = 0;
        private int enemiesToWin = 20;

        public Player Player => player;
        public List<Bullet> BulletList => bulletList;

        public void Start()
        {
            player = new Player(500, 400);
            SpawnWave();
        }

        public void Update()
        {
            player.Update();

            UpdateEnemies();
            UpdateBullets();

            CheckCollisions();
            HandleSpawnTimer();
        }

        public void Render()
        {
            Engine.Clear();

            player.Render();

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Render();
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Render();
            }

            Engine.Show();
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

        private void SpawnWave()
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                int x = 100 + i * 120;
                int y = 100;
                enemyList.Add(new Enemy(x, y));
            }
        }

        // ── Update helpers ───────────────────────────────────────

        private void UpdateEnemies()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update();
            }
        }

        private void UpdateBullets()
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }
        }

        // ── Colisiones ───────────────────────────────────────────

        private void CheckCollisions()
        {
            CheckBulletsVsEnemies();
            CheckEnemiesVsPlayer();
        }

        private void CheckBulletsVsEnemies()
        {
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                for (int j = enemyList.Count - 1; j >= 0; j--)
                {
                    if (bulletList[i].Overlaps(enemyList[j].Transform))
                    {
                        enemyList[j].Health.GetDamaged();
                        bulletList.RemoveAt(i);

                        if (enemyList[j].Health.IsDead())
                        {
                            enemyList.RemoveAt(j);
                            enemiesKilled++;
                            CheckWinCondition();
                        }

                        break;
                    }
                }
            }
        }

        private void CheckEnemiesVsPlayer()
        {
            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                if (enemyList[i].Transform.Overlaps(player.Transform))
                {
                    player.Health.GetDamaged();
                    enemyList.RemoveAt(i);
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
            bulletList.Add(bullet);
        }
    }
}