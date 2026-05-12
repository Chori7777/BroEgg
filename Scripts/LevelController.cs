using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class LevelController
    {
         Player player;
         public Player Player => player;

         List<Enemy> enemyList = new List<Enemy>();
         List<PowerUp> powerUpsList = new List<PowerUp>();
         List<Bullet> bulletList = new List<Bullet>();

         public List<Bullet> BulletList => bulletList;
         public List<PowerUp> PowerUpsList => powerUpsList;



        public void Start()
        {
            player = new Player(500, 400);

            enemyList.Add(new Enemy(200, 200));
            enemyList.Add(new Enemy(200, 400));

            powerUpsList.Add(new PowerUp(300, 300));
        }

        public void Update()
        {
            player.Update();
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update();
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }
            for (int i = 0; i < powerUpsList.Count; i++)
            {
                powerUpsList[i].Update();
            }

        }

        public void Render()
        {
            Engine.Clear();

            player.Render();
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Render();
            }
            for (int i = 0; i < powerUpsList.Count; i++)
            {
                powerUpsList[i].Render();
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Render();
            }

            Engine.Show();
        }

        public void AddBulletToList(Bullet bullet)
        {
            bulletList.Add(bullet);
        }
    }
}
