
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
            if (Engine.KeyPress(Engine.KEY_A))
            {
                transform.Translate(-1 * speed, 0);
            }
            if (Engine.KeyPress(Engine.KEY_D))
            {
                transform.Translate(1 * speed, 0);
            }
            if (Engine.KeyPress(Engine.KEY_W))
            {
                transform.Translate(0, -1 * speed);
            }
            if (Engine.KeyPress(Engine.KEY_S))
            {
                transform.Translate(0, 1 * speed);
            }
            if(Engine.KeyPress(Engine.KEY_ESC))
            {
                Program.isGameRunning = false;  
            }

        }

        public void UpdateSpeed(int newSpeed)
        {
            speed = newSpeed;
        }

    }
}
