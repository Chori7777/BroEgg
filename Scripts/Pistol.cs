using ProyectoSDL2.Engine;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Pistol : WeaponDad, IWeapon
    {
        private float timer = 0;
        private float baseFireRate = 0.4f;
        private int baseDamage = 1;
        private int bulletWidth = 16;
        private int bulletHeight = 16;

        public float FireRate => baseFireRate * (1f - playerStats.AttackSpeed / 100f);
        public int Damage => baseDamage + playerStats.Damage;

        public Pistol(Transform ownerTransform, PlayerStats playerStats) : base(ownerTransform, "assets/Pistol.png", playerStats) { }

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

            GameManager.Instance.LevelController.AddBullet(weaponX + 50, weaponY + 25, target, playerStats, baseDamage);

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
