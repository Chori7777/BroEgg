

namespace ProyectoSDL2.Engine.Scripts
{
    public class PowerUp
    {
        Transform transform;

        public PowerUp(int x, int y)
        {
            transform = new Transform(x, y);
        }

        public void Update()
        {
            if (transform.PosX + 50 > GameManager.Instace.LevelController.Player.Transform.PosX &&
                transform.PosX < GameManager.Instace.LevelController.Player.Transform.PosX + 100 &&
                transform.PosY + 50 > GameManager.Instace.LevelController.Player.Transform.PosY &&
                transform.PosY < GameManager.Instace.LevelController.Player.Transform.PosY + 100)
            {
                Engine.Debug("Colision con powerup");
                GameManager.Instace.LevelController.Player.Input.UpdateSpeed(10);
                GameManager.Instace.LevelController.PowerUpsList.Remove(this);
            }
        }

        public void Render()
        {
            //jarvis renderizame
            Engine.Draw("assets/powerup.png", transform.PosX, transform.PosY);
        }
    }
}
