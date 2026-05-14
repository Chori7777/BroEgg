namespace ProyectoSDL2.Engine.Scripts
{
    public class Weapon
    {
        private Transform ownerTransform;
        private float timer = 0;
        private float fireRate = 1f;

        public Weapon(Transform ownerTransform)
        {
            this.ownerTransform = ownerTransform;
        }

        public void Update()
        {
            timer += Program.DeltaTime;

            if (timer >= fireRate)
            {
                Shoot();
                timer = 0;
            }
        }

        private void Shoot()
        {
            Transform target = GetNearestEnemy();

            if (target == null) return;

            Bullet bullet = new Bullet(ownerTransform.PosX, ownerTransform.PosY, target);
            GameManager.Instance.LevelController.AddBullet(bullet);
        }

        private Transform GetNearestEnemy()
        {
            List<Enemy> enemies = GameManager.Instance.LevelController.EnemyList;

            if (enemies.Count == 0) return null;

            Transform nearest = null;
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < enemies.Count; i++)
            {
                float deltaX = enemies[i].Transform.PosX - ownerTransform.PosX;
                float deltaY = enemies[i].Transform.PosY - ownerTransform.PosY;
                float distance = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = enemies[i].Transform;
                }
            }

            return nearest;
        }
    }
}