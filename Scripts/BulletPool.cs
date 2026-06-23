using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class BulletPool
    {
        private List<Bullet> availableBullets = new List<Bullet>(); // objetos disponibles
        private List<Bullet> bulletsInUse = new List<Bullet>();     // objetos en uso

        private int bulletWidth = 16;
        private int bulletHeight = 16;

        public BulletPool(int initialSize)
        {
            // llenamos el pool con bullets "vacias" desde el arranque
            for (int i = 0; i < initialSize; i++)
            {
                CreateNewBullet();
            }
        }

        private void CreateNewBullet()
        {
            Bullet bullet = new Bullet(0, 0, bulletWidth, bulletHeight, new Transform(0, 0, 1, 1), null, 1);
            bullet.OnDeactivate += OnDeactivateHandler; // se suscribe al evento
            availableBullets.Add(bullet);
        }

        public Bullet GetBullet(int x, int y, Transform target, PlayerStats playerStats, int baseDamage)
        {
            Bullet bullet;

            if (availableBullets.Count > 0)
            {
                // saco del disponibles
                bullet = availableBullets[availableBullets.Count - 1];
                availableBullets.RemoveAt(availableBullets.Count - 1);
            }
            else
            {
                // pool dinamico: si no hay, creo uno nuevo
                CreateNewBullet();
                bullet = availableBullets[0];
                availableBullets.RemoveAt(0);
            }

            bullet.Reset(x, y, target, playerStats, baseDamage);
            bulletsInUse.Add(bullet); // pasa a en uso
            return bullet;
        }

        private void OnDeactivateHandler(Bullet bullet)
        {
            // cuando se desactiva, lo saco de en uso y lo mando a disponibles
            bulletsInUse.Remove(bullet);
            availableBullets.Add(bullet);
        }
    }
}
