namespace ProyectoSDL2.Engine.Scripts
{
    public class EnemyBullet : GameObject, IPoolable
    {
        private float dx;
        private float dy;
        private int speed = 5;
        public int Damage { get; private set; } //esto significa que puede ser leida publicamente pero modificada de forma privada
        public bool IsActive { get; private set; }

        public void Activate() => IsActive = true;

        public EnemyBullet(int startX, int startY, int bulletWidth, int bulletHeight, Transform target, int damage)
            : base(startX, startY, bulletWidth, bulletHeight)
        {
            Damage = damage;

            float deltaX = target.PosX - startX;
            float deltaY = target.PosY - startY;
            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (length == 0) //ojo con la division por 0 pa, te puede dar muchos problemas en la vida, uno nunca sabe 
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

        public void Reset(int startX, int startY, Transform target, int damage) //para la interfaz IPoolable
        {
            //Esto es para reutilizr la bala que esta en uso 
            Damage = damage;
            IsPendingDestroy = false;
            IsActive = true;

            transform.PosX = startX;
            transform.PosY = startY;

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

        public bool Overlaps(Transform other)
        {
            return transform.Overlaps(other);
        }
        public void Deactivate()
        {
            IsActive = false;
            IsPendingDestroy = false;
        }

        public override void Render()
        {
            Engine.Draw("assets/enemyBullet.png", transform.PosX, transform.PosY);
        }
    }
}