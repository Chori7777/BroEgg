namespace ProyectoSDL2.Engine.Scripts
{
    public class PlayerInput
    {
        private Transform transform;
        private int speed = 5;

        public PlayerInput(Transform playerTransform)
        {
            transform = playerTransform;
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

            if (Engine.KeyPress(Engine.KEY_ESC))
            {
                Program.isGameRunning = false;
            }
        }
    }
}