namespace ProyectoSDL2.Engine.Scripts
{
    public class Transform
    {
        private int posX;
        private int posY;
        private int width;
        private int height;

        public int PosX => posX;
        public int PosY => posY;
        public int Width => width;
        public int Height => height;

        public Transform(int x, int y, int width, int height)
        {
            posX = x;
            posY = y;
            this.width = width;
            this.height = height;
        }

        public void Translate(int moveX, int moveY)
        {
            posX += moveX;
            posY += moveY;
        }

        public bool Overlaps(Transform colision)
        {
            return posX + width > colision.posX &&
                   posX < colision.posX + colision.width &&
                   posY + height > colision.posY &&
                   posY < colision.posY + colision.height;
        }
    }
}