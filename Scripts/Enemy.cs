namespace ProyectoSDL2.Engine.Scripts
{
    public class Enemy : GameObject
    {
       
        private EnemyMovement movement;
        private Animation animation;
        private EnemyStats statsEnemy;

        public EnemyStats StatsEnemy => statsEnemy;

        public Enemy(int startPosX, int startPosY, int enemyWidth, int enemyHeight, List<Image> frames, EnemyStats enemyStats)  : base(startPosX, startPosY, enemyWidth, enemyHeight)
        {
            this.statsEnemy = enemyStats;
            movement = new EnemyMovement(transform, GameManager.Instance.LevelController.Player.Transform, statsEnemy.MoveSpeedEnemy);

            
            animation = new Animation(frames, 0.1f);
        }

        public override void Update()
        {
            movement.MoveEnemy();
            animation.Update();
        }

        public override void Render()
        {
            Engine.Draw(animation.CurrentFrame, transform.PosX, transform.PosY);
        }
    }
}