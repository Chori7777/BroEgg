namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet
    {
        private Transform transform;
        private float dx;
        private float dy;
        private int speed = 8;

        public Transform Transform => transform;

        public Bullet(int startX, int startY, Transform target)
        {
            transform = new Transform(startX, startY, 16, 16);

            float deltaX = target.PosX - startX;
            float deltaY = target.PosY - startY;

            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
            dx = deltaX / length;
            dy = deltaY / length;
        }

        public void Update()
        {
            transform.Translate((int)(dx * speed), (int)(dy * speed));
        }

        public bool Overlaps(Transform other)
        {
            return transform.Overlaps(other);
        }

        public void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }
    }
}