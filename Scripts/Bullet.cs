using System;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Bullet
    {
        Transform transform;


        public Bullet(int x, int y)
        {
            transform = new Transform(x + 50, y + 60);
        }

        public void Update()
        {
            transform.Translate(0, 7);

            if (transform.PosX + 5 > GameManager.Instace.LevelController.Player.Transform.PosX &&
                transform.PosX < GameManager.Instace.LevelController.Player.Transform.PosX + 100 &&
                transform.PosY + 5 > GameManager.Instace.LevelController.Player.Transform.PosY &&
                transform.PosY < GameManager.Instace.LevelController.Player.Transform.PosY + 100)
            {
                GameManager.Instace.LevelController.Player.Health.GetDamaged();
                GameManager.Instace.LevelController.BulletList.Remove(this);
            }
        }

        public void Render()
        {
            Engine.Draw("assets/bullet.png", transform.PosX, transform.PosY);
        }

    }
}
