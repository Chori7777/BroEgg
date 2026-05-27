namespace ProyectoSDL2.Engine.Scripts
{
    public class Transform
    {
        private float posX;
        private float posY;
        private int width;
        private int height;

        public int PosX 
        { 
            get => (int)posX; 
            set => posX = value; 
        }
        public int PosY 
        { 
            get => (int)posY; 
            set => posY = value; 
        }
        public int Width => width;
        public int Height => height;

        public Transform(int x, int y, int width, int height)
        {
            posX = x;
            posY = y;
            this.width = width;
            this.height = height;
        }

        public void Translate(float moveX, float moveY)
        {
            posX += moveX;
            posY += moveY;
        }

        public bool Overlaps(Transform colision) //devuelve true si se cumple la colision AABB
        {
            return posX + width > colision.posX &&
                   posX < colision.posX + colision.width &&
                   posY + height > colision.posY &&
                   posY < colision.posY + colision.height;
        }
    }
}