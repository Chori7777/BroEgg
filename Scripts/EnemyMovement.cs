

namespace ProyectoSDL2.Engine.Scripts
{
    public class EnemyMovement
    {
        Transform transform;
        Transform playerTransform;
        private int speed = 5;

        public EnemyMovement(Transform newTransform,Transform playerTransform)
        {
            transform = newTransform;
            this.playerTransform = playerTransform; 
          

        }

        public void MoveEnemy()
        {
       
            float deltaX = playerTransform.PosX - transform.PosX;
            float deltaY = playerTransform.PosY - transform.PosY;

            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
            if (length == 0) return; // evitar división por cero si están encima

            float dx = deltaX / length;
            float dy = deltaY / length;

            transform.Translate((int)(dx * speed), (int)(dy * speed));
        }
    }
}
