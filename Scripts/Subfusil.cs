using ProyectoSDL2.Engine;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Subfusil : WeaponDad, IWeapon
    {
        private float timer = 0;
        private float baseFireRate = 0.15f;
        private int baseDamage = 1;
        private int bulletWidth = 16;
        private int bulletHeight = 16;

        public float FireRate => baseFireRate * (1f - playerStats.AttackSpeed / 100f);
        public int Damage => baseDamage + playerStats.Damage;

        public Subfusil(Transform ownerTransform, PlayerStats playerStats) : base(ownerTransform, "assets/Subfusil.png", playerStats) { }

        public override void Update()
        {
            timer += Program.DeltaTime;
            if (timer >= FireRate)
            {
                Shoot();
                timer = 0;
            }
        }

        private void Shoot()
        {
            Transform target = GetNearestEnemy();
            if (target == null) return;

            int weaponX = GetWeaponX();
            int weaponY = GetWeaponY();

            Bullet bullet = new Bullet(weaponX + 50, weaponY + 25, bulletWidth, bulletHeight, target, playerStats, baseDamage);
            GameManager.Instance.LevelController.AddBullet(bullet);
        }

        private Transform GetNearestEnemy()
        {
            Transform nearest = null;
            float nearestDistance = float.MaxValue;

            if (GameManager.Instance.LevelController.GameObjectsList.Count == 0) return null;

            for (int i = 0; i < GameManager.Instance.LevelController.GameObjectsList.Count; i++)
            {
                GameObject enemy = GameManager.Instance.LevelController.GameObjectsList[i];
                if (enemy is Enemy)
                {
                    float deltaX = enemy.Transform.PosX - ownerTransform.PosX;
                    float deltaY = enemy.Transform.PosY - ownerTransform.PosY;
                    float distance = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearest = enemy.Transform;
                    }
                }
            }
            return nearest;
        }

        public override void Render()
        {
            int weaponX = GetWeaponX();
            int weaponY = GetWeaponY();
            
            if (facingRight)
            {
                Engine.Draw(weaponSprite, weaponX, weaponY);
            }
            else
            {
                Engine.DrawFlipped(weaponSprite, weaponX, weaponY, true);
            }
        }
    }
}
