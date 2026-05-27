using System.Runtime.Intrinsics.X86;

namespace ProyectoSDL2.Engine.Scripts
{
    public class LevelController
    {
        private Player player;
        private HUD hud;
        private LevelManager levelManager;

        private int playerWidth = 64; private int playerHeight = 64;
        private int enemyWidth = 64; private int enemyHeight = 64;

        private List<GameObject> enemyBulletsList = new List<GameObject>();

        private List<GameObject> gameObjectsList = new List<GameObject>();
        public List<GameObject> GameObjectsList => gameObjectsList;

        private Image backgroundImage;
        private Image redFlashImage;
        public LevelManager LevelManager => levelManager;

        public Player Player => player;
        
        private float screenFlashTimer;
        private bool isScreenFlashing;


        public void Start()
        {
            backgroundImage = Engine.LoadImage("assets/Background.png"); 
            redFlashImage = Engine.LoadImage("assets/RedFlash.png");
            hud = new HUD(this);
            player = new Player(500, 400, playerWidth, playerHeight);
            levelManager = new LevelManager(this);
            levelManager.StartWave();

            player.OnPlayerDied += HandlePlayerDeath; //se suscribe el metodo al evento
        }

        public void Update()
        {
            player.Update();

            UpdateGameObjects();

            UpdateEnemyBullets();
            
            CheckCollisions();

            levelManager.UpdateWave();

            CleanupDestroyedObjects();

            CleanupEnemyBullets();  

            if (isScreenFlashing)
            {
                screenFlashTimer -= Program.DeltaTime;
                if (screenFlashTimer <= 0)
                {
                    isScreenFlashing = false;
                }
            }
        }

        private void TriggerScreenFlash()
        {
            isScreenFlashing = true;
            screenFlashTimer = 0.2f;
        }

        public void Render()
        {
            Engine.Clear();
            Engine.Draw(backgroundImage, 0, 0); 
            player.Render();

            for (int i = 0; i < gameObjectsList.Count; i++)
            {
                gameObjectsList[i].Render();
            }

            for (int i = 0; i < enemyBulletsList.Count; i++)
            {
                enemyBulletsList[i].Render();
            }

            hud.Render();

            if (isScreenFlashing)
            {
                Engine.Draw(redFlashImage, 0, 0);
            }

            Engine.Show();
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
            CheckEnemyBulletsVsPlayer();
        }
        private void CleanupDestroyedObjects() //metodo clave para limpiar ciclos for y que no haya overflow
        {
            // Acá sí recorremos de atrás hacia adelante para borrar de forma segura
            for (int i = gameObjectsList.Count - 1; i >= 0; i--)
            {
                if (gameObjectsList[i].IsPendingDestroy) //se fija si el currentGameObject esta esperando ser destruido
                {
                    gameObjectsList.RemoveAt(i); //entonces lo remueve de la lista
                }
            }
        }
        private void CheckBulletsVsEnemies()
        {
            for (int i = 0; i < gameObjectsList.Count; i++) 
            {
                GameObject bulletObject = gameObjectsList[i]; 
                if (bulletObject.IsPendingDestroy || !(bulletObject is Bullet)) continue; 

                Bullet bullet = (Bullet)bulletObject; 

                for (int j = 0; j < gameObjectsList.Count; j++)
                {
                    GameObject enemyObject = gameObjectsList[j];
                    if (enemyObject.IsPendingDestroy || !(enemyObject is Enemy)) continue;
                    Enemy enemy = (Enemy)enemyObject;

                    if (bullet.Overlaps(enemyObject.Transform))
                    {
                        var (finalDamage, isCrit, lifeStealAmount) = bullet.CalculateFinalDamage(); //calcula el daño con todas sus cosas
                        enemy.StatsEnemy.GetDamaged(finalDamage); //se le manda el daño final al enemigo
                        enemy.TriggerFlash(); 

                        if (lifeStealAmount > 0)
                        {
                            player.PlayerStats.RestoreHealth(lifeStealAmount); //se le da vida al jugador con la funcion RestoreHealth
                        }

                        bulletObject.IsPendingDestroy = true; //se avisa que esa bala que colisiono tiene que ser destruida

                        if (enemy.StatsEnemy.IsDead())
                        {
                            enemyObject.IsPendingDestroy = true; //se avisa que el enemigo tiene que ser destruido
                            levelManager.OnEnemyKilled(); //da EXP y aumenta en 1 el contador de enemigos matados
                            enemy.Die(); //Activa el evento
                        }

                        break; //rompemos todos los bucles para volver a empezar
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
                    Enemy enemy = (Enemy)enemyObject;
                    player.PlayerStats.GetDamaged(enemy.StatsEnemy.DmgEnemy);
                    player.TriggerFlash(); //sufre danio
                    TriggerScreenFlash();
                    gameObjectsList.RemoveAt(i);
                }
            }
        }

        private void CheckWinCondition() //se fija si al terminar la ronda, ya gano y carga la WIN o si todavia el jugador no gano y tiene que cargar la StatsScreen
        {
            if (levelManager.IsWaveComplete())
            {
                if (levelManager.HasWon())
                {
                    GameManager.Instance.ChangeGameState(GAME_STATE.WIN);
                }
                else
                {
                    GameManager.Instance.ChangeGameState(GAME_STATE.TRANSICION);
                }
            }
        }
        public void AddEnemyBullet(EnemyBullet bullet)
        {
            enemyBulletsList.Add(bullet);
        }

        private void UpdateEnemyBullets()
        {
            for (int i = 0; i < enemyBulletsList.Count; i++)
            {
                enemyBulletsList[i].Update();
            }
        }

        private void CheckEnemyBulletsVsPlayer()
        {
            for (int i = enemyBulletsList.Count - 1; i >= 0; i--)
            {
                GameObject bulletObject = enemyBulletsList[i];
                if (bulletObject.IsPendingDestroy || !(bulletObject is EnemyBullet)) continue;

                EnemyBullet bullet = (EnemyBullet)bulletObject;

                if (bullet.Overlaps(player.Transform))
                {
                    player.PlayerStats.GetDamaged(bullet.Damage);
                    player.TriggerFlash();
                    TriggerScreenFlash();
                    bulletObject.IsPendingDestroy = true;
                }
            }
        }

        private void CleanupEnemyBullets()
        {
            for (int i = enemyBulletsList.Count - 1; i >= 0; i--)
            {
                if (enemyBulletsList[i].IsPendingDestroy)
                {
                    enemyBulletsList.RemoveAt(i);
                }
            }
        }
        public void AddBullet(Bullet bullet)
        {
            gameObjectsList.Add(bullet);
        }


        // Separado de lo q hicieron uds

        public void NextLevel()
        {
            levelManager.ClearEnemies();
            levelManager.NextLevel();
            player.PlayerStats.RestoreHealth();
         
            Engine.Debug("Ronda actual: " + LevelManager.CurrentRound);
            if(!levelManager.HasWon())
            {
                levelManager.StartWave();

            }
            else
            {
                GameManager.Instance.ChangeGameState(GAME_STATE.WIN);
            }
        }

        private void HandlePlayerDeath() //se ejecuta cuando el evento OnPlayerDied ocurre (playerSats.IsDeath() = true)
        {
            // Cambiamos el estado del juego a derrota
            GameManager.Instance.ChangeGameState(GAME_STATE.END);
        }

        public void HandleEnemyDied()
        {
            CheckWinCondition();
        }
    }
}