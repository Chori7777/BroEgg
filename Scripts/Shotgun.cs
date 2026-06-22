using ProyectoSDL2.Engine;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Shotgun : WeaponDad, IWeapon
    {
        private float timer = 0;
        private float baseFireRate = 2f;
        private int baseDamage = 1;
        private int bulletWidth = 16;
        private int bulletHeight = 16;
        private int pellets = 3;
        private float spreadAngle = 0.3f;

        public float FireRate => baseFireRate * (1f - playerStats.AttackSpeed / 100f);
        public int Damage => baseDamage + playerStats.Damage;

        public Shotgun(Transform ownerTransform, PlayerStats playerStats) : base(ownerTransform, "assets/Shotgun.png", playerStats) { }

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
            

            for (int i = 0; i < pellets; i++)
            {
                Bullet bullet = CreatePellet(target, i, weaponX, weaponY);
                GameManager.Instance.LevelController.AddBullet(bullet);
            }
        }

        private Bullet CreatePellet(Transform target, int pelletIndex, int startX, int startY)
        {
            float deltaX = target.PosX - startX;
            float deltaY = target.PosY - startY;
            float angle = MathF.Atan2(deltaY, deltaX);
            
            float spread = (pelletIndex - pellets / 2f) * spreadAngle;
            float newAngle = angle + spread;
            
            Transform fakeTarget = new Transform(
                startX + (int)(MathF.Cos(newAngle) * 100),
                startY + (int)(MathF.Sin(newAngle) * 100),
                1, 1
            );
            
            return new Bullet(startX, startY, bulletWidth, bulletHeight, fakeTarget, playerStats, baseDamage);
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
