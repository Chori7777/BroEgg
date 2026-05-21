namespace ProyectoSDL2.Engine.Scripts
{
    public class Player : GameObject
    {
        private PlayerInput input;
        private Health health;
       
        private DefaultWeapon weapon;
        private Animation animation;
        public Health Health => health;

        private bool facingRight = false;

        public Player(int startPosX, int startPosY, int playerWidth, int playerHeight) : base(startPosX, startPosY, playerWidth, playerHeight)
        {
            health = new Health(10);
            input = new PlayerInput(transform);
            weapon = new DefaultWeapon(transform);

            List<Image> frames = new List<Image>();
            frames.Add(Engine.LoadImage("assets/Player/Player_0.png"));
            frames.Add(Engine.LoadImage("assets/Player/Player_1.png"));
            frames.Add(Engine.LoadImage("assets/Player/Player_2.png"));
            frames.Add(Engine.LoadImage("assets/Player/Player_3.png"));
            animation = new Animation(frames, 0.01f); 
        }

        public override void Update()
        {
            if (Engine.KeyPress(Engine.KEY_D)) facingRight = true;
            if (Engine.KeyPress(Engine.KEY_A)) facingRight = false;

            input.Update();
            weapon.Update();
            animation.Update();
            


            if (health.IsDead())
            {
                GameManager.Instance.ChangeGameState(GAME_STATE.END);
            }
        }

        public override void Render()
        {
            Engine.DrawFlipped(animation.CurrentFrame, transform.PosX, transform.PosY, facingRight);
        }
    }
}