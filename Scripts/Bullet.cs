namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet : GameObject, IPoolable
    {
        private float dx;
        private float dy;
        private int speed = 8;
        private PlayerStats playerStats;
        public int BaseDamage { get; private set; }

        public bool IsActive { get; private set; }

        public void Activate() => IsActive = true;

        public Bullet(int startX, int startY, int bulletWidth, int bulletHeight, Transform target, PlayerStats playerStats, int baseDamage = 1)
            : base(startX, startY, bulletWidth, bulletHeight)
        {
            this.playerStats = playerStats;
            BaseDamage = baseDamage;
            SetDirection(target, startX, startY);
        }

        private void SetDirection(Transform target, int startX, int startY)
        {
            float deltaX = target.PosX - startX;
            float deltaY = target.PosY - startY;
            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
            dx = deltaX / length;
            dy = deltaY / length;
        }

        public void Reset(int startX, int startY, Transform target, PlayerStats stats, int baseDamage)
        {
            transform.PosX = startX;
            transform.PosY = startY;
            playerStats = stats;
            BaseDamage = baseDamage;
            IsPendingDestroy = false;
            SetDirection(target, startX, startY);
        }

        public override void Update()
        {
            if (!IsActive) return;
            transform.Translate((int)(dx * speed), (int)(dy * speed));

            if (transform.PosX < -50 || transform.PosX > 1330 ||
                transform.PosY < -50 || transform.PosY > 770)
            {
                Deactivate();
            }
        }

        public void Deactivate()
        {
            IsActive = false;
            IsPendingDestroy = false; // lo resetea para reutilizarlo
        }

        public override void Render()
        {
            if (!IsActive) return;
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }

        public (int finalDamage, bool isCrit, int lifeStealAmount) CalculateFinalDamage()
        {
            return playerStats.CalculateDamage();
        }
    }
}