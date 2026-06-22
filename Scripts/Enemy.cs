namespace ProyectoSDL2.Engine.Scripts
{
    public class Enemy : GameObject
    {
        private EnemyMovement movement;
        private Animation animation;
        private EnemyStats statsEnemy;
        protected bool facingRight = false;
        private float flashTimer;
        private bool isFlashing;

        public EnemyStats StatsEnemy => statsEnemy;
        public event Action OnEnemyDied;

        public Enemy(int startPosX, int startPosY, int enemyWidth, int enemyHeight, List<Image> frames, EnemyStats enemyStats)
            : base(startPosX, startPosY, enemyWidth, enemyHeight)
        {
            this.statsEnemy = enemyStats;
            movement = new EnemyMovement(transform, GameManager.Instance.LevelController.Player.Transform, statsEnemy.MoveSpeedEnemy);
            animation = new Animation(frames, 0.1f);
        }

        public override void Update()
        {
            movement.MoveEnemy();
            UpdateFacingDirection();
            animation.Update();
            
            if (isFlashing)
            {
                flashTimer -= Program.DeltaTime;
                if (flashTimer <= 0)
                {
                    isFlashing = false;
                }
            }
        }

        public void TriggerFlash()
        {
            isFlashing = true;
            flashTimer = 0.2f;
        }

        private void UpdateFacingDirection()
        {
            Transform playerTransform = GameManager.Instance.LevelController.Player.Transform;
            facingRight = playerTransform.PosX > transform.PosX;
        }

        public void Die()
        {
            IsPendingDestroy = true;
            OnEnemyDied?.Invoke(); // dispara el evento
            Engine.Debug("evento OnEnemyDied disparado");
        }

        public override void Render()
        {
            if (!isFlashing || (flashTimer * 15) % 2 == 0)
            {
                Engine.DrawFlipped(animation.CurrentFrame, transform.PosX, transform.PosY, !facingRight);
            }
        }
    }
}