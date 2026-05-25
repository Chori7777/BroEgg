namespace ProyectoSDL2.Engine.Scripts
{
    public class SoldierEnemy : Enemy
    {
        private float shootTimer = 0f;
        private float shootCooldown = 1.5f;
        private int bulletWidth = 12;
        private int bulletHeight = 12;

        public SoldierEnemy(int startPosX, int startPosY, int wave)
            : base(startPosX, startPosY, 100, 100, GetFrames(), new EnemyStats(
                hpEnemy: 4 + (1 * wave),
                dmgEnemy: 3 + (1 * wave),
                moveSpeedEnemies: 4,
                armorEnemies: 1 + (1 * wave)
              ))
        {
        }

        private static List<Image> GetFrames()
        {
            return new List<Image>
            {
                Engine.LoadImage("assets/enemy/SoldierEnemy.png"),
                Engine.LoadImage("assets/enemy/SoldierEnemy1.png"),
                Engine.LoadImage("assets/enemy/SoldierEnemy2.png"),

            };
        }

        public override void Update()
        {
            base.Update();
            ShootAtPlayer();
        }

        private void ShootAtPlayer()
        {
            shootTimer += Program.DeltaTime;
            if (shootTimer >= shootCooldown)
            {
                Transform playerTransform = GameManager.Instance.LevelController.Player.Transform;

                EnemyBullet bullet = new EnemyBullet(
                    transform.PosX,
                    transform.PosY,
                    bulletWidth,
                    bulletHeight,
                    playerTransform,
                    StatsEnemy.DmgEnemy
                );

                GameManager.Instance.LevelController.AddEnemyBullet(bullet);
                shootTimer = 0f;
            }
        }
    }
}