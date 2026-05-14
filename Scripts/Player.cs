namespace ProyectoSDL2.Engine.Scripts
{
    public class Player
    {
        private Transform transform;
        private PlayerInput input;
        private Health health;
       
        private Weapon weapon;

        public Transform Transform => transform;
        public Health Health => health;

        public Player(int startPosX, int startPosY)
        {
            transform = new Transform(startPosX, startPosY, 64, 64);
            health = new Health(10);
            input = new PlayerInput(transform);
            weapon = new Weapon(transform);

           
        }

        public void Update()
        {
            input.Update();
            weapon.Update();
          

            if (health.IsDead())
            {
                GameManager.Instance.ChangeGameState(GAME_STATE.END);
            }
        }

        public void Render()
        {
            
            
                Engine.Draw("assets/ship.png", transform.PosX, transform.PosY);
            
        }
    }
}