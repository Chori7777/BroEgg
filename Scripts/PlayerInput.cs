namespace ProyectoSDL2.Engine.Scripts
{
    public class PlayerInput
    {
        private Transform transform;
        private PlayerStats playerStats;
        private int speed = 5;

    // Bueno basicamente son Constantes, no pueden modificarse x nada del mundo, lo dejo asi para q nadie pueda alterar su valor despues
        private const int ScreenWidth = 1180;
        private const int ScreenHeight = 620;

        public PlayerInput(Transform playerTransform,PlayerStats stats)
        {
            transform = playerTransform;
            playerStats = stats;
        }

        public void Update()
        {
            int moveX = 0;
            int moveY = 0;

            if (Engine.KeyPress(Engine.KEY_A)) moveX = -1;
            if (Engine.KeyPress(Engine.KEY_D)) moveX = 1;
            if (Engine.KeyPress(Engine.KEY_W)) moveY = -1;
            if (Engine.KeyPress(Engine.KEY_S)) moveY = 1;

            if (moveX != 0 && moveY != 0)
            {
                float length = MathF.Sqrt(moveX * moveX + moveY * moveY);
                transform.Translate((int)(moveX / length * speed), (int)(moveY / length * speed));
            }
            else
            {
                transform.Translate(moveX * speed, moveY * speed);
            }

            ClampToScreenBounds();

            if (Engine.KeyPress(Engine.KEY_ESC))
            {
                Program.isGameRunning = false;
            }
        }
             private void ClampToScreenBounds()
        {
            transform.PosX = Math.Clamp(transform.PosX, 0, ScreenWidth - transform.Width);
            transform.PosY = Math.Clamp(transform.PosY, 0, ScreenHeight - transform.Height);
        }
    }
}