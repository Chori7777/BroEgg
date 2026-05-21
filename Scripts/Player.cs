namespace ProyectoSDL2.Engine.Scripts
{
    public class Player : GameObject
    {
        private PlayerInput input;
        private PlayerStats playerstats;
        public PlayerStats PlayerStats => playerstats;
       
       
        private DefaultWeapon weapon;
        private Animation animation;
       

        private bool facingRight = false;

        public Player(int startPosX, int startPosY, int playerWidth, int playerHeight) : base(startPosX, startPosY, playerWidth, playerHeight)
        {
            playerstats = new PlayerStats();
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
            


            if (playerstats.IsDead())
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