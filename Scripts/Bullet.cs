namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet : GameObject
    {
        private float dx;
        private float dy;
        private int speed = 8;
        private PlayerStats playerStats;
        public int BaseDamage { get; private set; }

        public event Action<Bullet>? OnDeactivate; // evento para avisar al pool

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
            transform.Translate((int)(dx * speed), (int)(dy * speed));

            if (transform.PosX < -50 || transform.PosX > 1330 ||
                transform.PosY < -50 || transform.PosY > 770)
            {
                Deactivate();
            }
        }

        public void Deactivate()
        {
            IsPendingDestroy = true;
            OnDeactivate?.Invoke(this); // avisa al pool que puede reciclarlo
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