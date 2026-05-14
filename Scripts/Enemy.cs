namespace ProyectoSDL2.Engine.Scripts
{
    public class Enemy
    {
        private Transform transform;
        private Health health;
        private EnemyMovement movement;
        private Animation animation;

        public Transform Transform => transform;
        public Health Health => health;

        public Enemy(int startPosX, int startPosY)
        {
            transform = new Transform(startPosX, startPosY, 64, 64);
            health = new Health(2);
            movement = new EnemyMovement(transform, GameManager.Instance.LevelController.Player.Transform);

            List<Image> frames = new List<Image>();
            frames.Add(Engine.LoadImage("assets/enemy/0.png"));
            frames.Add(Engine.LoadImage("assets/enemy/1.png"));
            frames.Add(Engine.LoadImage("assets/enemy/2.png"));
            frames.Add(Engine.LoadImage("assets/enemy/3.png"));
            animation = new Animation(frames, 0.1f);
        }

        public void Update()
        {
            movement.MoveEnemy();
            animation.Update();
        }

        public void Render()
        {
            Engine.Draw(animation.CurrentFrame, transform.PosX, transform.PosY);
        }
    }
}