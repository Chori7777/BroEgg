namespace ProyectoSDL2.Engine.Scripts
{
    public class EnemyBullet : GameObject
    {
        private float dx;
        private float dy;
        private int speed = 5;
        public int Damage { get; private set; }

        public EnemyBullet(int startX, int startY, int bulletWidth, int bulletHeight, Transform target, int damage)
            : base(startX, startY, bulletWidth, bulletHeight)
        {
            Damage = damage;

            float deltaX = target.PosX - startX;
            float deltaY = target.PosY - startY;
            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (length == 0)
            {
                dx = 0;
                dy = 0;
                return;
            }

            dx = deltaX / length;
            dy = deltaY / length;
        }

        public override void Update()
        {
            transform.Translate((int)(dx * speed), (int)(dy * speed));

         
            // Chequeamos si la bala salió de la pantalla
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
            Engine.Draw("assets/enemyBullet.png", transform.PosX, transform.PosY);
        }
    }
}