namespace ProyectoSDL2.Engine.Scripts
{
    public class Player : GameObject
    {
        private PlayerInput input;
        private Health health;
       
        private Weapon weapon;
        public Health Health => health;

        public Player(int startPosX, int startPosY, int playerWidth, int playerHeight) : base(startPosX, startPosY, playerWidth, playerHeight)
        {
            health = new Health(10);
            input = new PlayerInput(transform);
            weapon = new Weapon(transform);
        }

        public override void Update()
        {
            input.Update();
            weapon.Update();
          

            if (health.IsDead())
            {
                GameManager.Instance.ChangeGameState(GAME_STATE.END);
            }
        }

        public override void Render()
        {
            
               Engine.Draw("assets/ship.png", transform.PosX, transform.PosY);
        }
    }
}