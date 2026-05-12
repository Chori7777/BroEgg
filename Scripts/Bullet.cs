    using System;

    namespace ProyectoSDL2.Engine.Scripts
    {
        public class Bullet
        {
            Transform transform;
            float dx, dy;

            int speed = 7;

            public Bullet(int x, int y, Transform enemyTransform)
            {
                transform = new Transform(x + 50, y + 60);

            // Vector desde la bala hacia el enemigo
            float deltaX = enemyTransform.PosX - transform.PosX;
            float deltaY = enemyTransform.PosY - transform.PosY;

            float length = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
            dx = deltaX / length;
            dy = deltaY / length;
        }

            public void Update()
            {
                transform.Translate((int)(dx * speed), (int)(dy * speed));

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
