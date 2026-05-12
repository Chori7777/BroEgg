using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Weapon
    {
        float timer = 0;
        private Transform transform;

        public Weapon(Transform enemyTransform)
        {
            transform = enemyTransform;
        }

        public void Shoot()
        {
            //sumo el deltatime
            timer += Program.DeltaTime;

            if (timer >= 2)
            {
                Engine.Debug("Disparo");
                GameManager.Instace.LevelController.AddBulletToList(new Bullet(transform.PosX,transform.PosY));
                timer = 0;
            }
        }
    }
}
