namespace ProyectoSDL2.Engine.Scripts
{
    public class EnemyMovement
    {
        private Transform transform;
        private Transform playerTransform;
        private int speed = 2;

        public EnemyMovement(Transform enemyTransform, Transform playerTransform)
        {
            transform = enemyTransform;
            this.playerTransform = playerTransform;
        }

        public void MoveEnemy()
        {
            float deltaX = playerTransform.PosX - transform.PosX;
            float deltaY = playerTransform.PosY - transform.PosY;

            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (length == 0) return;

            float dx = deltaX / length;
            float dy = deltaY / length;

            transform.Translate((int)(dx * speed), (int)(dy * speed));
        }
    }
}