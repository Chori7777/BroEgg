namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet : GameObject
    {
        private float dx;
        private float dy;
        private int speed = 8;


        public Bullet(int startX, int startY, int bulletWidth, int bulletHeight, Transform target) : base(startX, startY, bulletWidth, bulletHeight)
        {

            float deltaX = target.PosX - startX;
            float deltaY = target.PosY - startY;

            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
            dx = deltaX / length;
            dy = deltaY / length;
        }

        public override void Update()
        {
            transform.Translate((int)(dx * speed), (int)(dy * speed));
        }

        public bool Overlaps(Transform other)
        {
            return transform.Overlaps(other);
        }

        public override void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }
    }
}