namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet : GameObject
    {
        private float dx;
        private float dy;
        private int speed = 8;
        private PlayerStats playerStats;
        public int BaseDamage { get; private set; }

        public Bullet(int startX, int startY, int bulletWidth, int bulletHeight, Transform target, PlayerStats playerStats, int baseDamage = 1)
            : base(startX, startY, bulletWidth, bulletHeight)
        {
            this.playerStats = playerStats;
            BaseDamage = baseDamage;

            float deltaX = target.PosX - startX;
            float deltaY = target.PosY - startY;
            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
            dx = deltaX / length;
            dy = deltaY / length;
        }

        public override void Update()
        {
            transform.Translate((int)(dx * speed), (int)(dy * speed));

            // la bala caminaba por fuera de la pantalla hasta que le cayo un meteorito que se llama "IsPendingDestroy"
            if (transform.PosX < -50 || transform.PosX > 1330 ||
                transform.PosY < -50 || transform.PosY > 770)
            {
                IsPendingDestroy = true;
            }
        }

        public bool Overlaps(Transform other)
        {
            return transform.Overlaps(other);
        }

        public override void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }
        public (int finalDamage, bool isCrit, int lifeStealAmount) CalculateFinalDamage()
        {
            return playerStats.CalculateDamage();
        }
    }
}