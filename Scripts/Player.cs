namespace ProyectoSDL2.Engine.Scripts
{
    public class Player
    {
        Transform transform;
        PlayerInput input;
        Health health;

        bool isActive = true;

        public Transform Transform => transform;
        public PlayerInput Input => input;
        public Health Health => health;


        public Player(int startPosX, int startPosY)
        {
            transform = new Transform(startPosX, startPosY);
            health = new Health();
            input = new PlayerInput(transform);
        }

        public void Update()
        {
            if (isActive)
            {
                input.Update();
            }


        }

        public void Render()
        {
            if (isActive)
            {
                Engine.Draw("assets/ship.png", transform.PosX, transform.PosY);
            }
        }



    }
}
