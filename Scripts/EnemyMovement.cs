namespace ProyectoSDL2.Engine.Scripts
{
    public class EnemyMovement
    {
        private Transform transform;
        private Transform playerTransform;
        private float speed = 2f;

        public EnemyMovement(Transform enemyTransform, Transform playerTransform, float velEn)
        {
            this.speed = velEn;
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

            transform.Translate(dx * speed * Program.DeltaTime, dy * speed * Program.DeltaTime);
        }
    }
}