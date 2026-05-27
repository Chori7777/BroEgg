namespace ProyectoSDL2.Engine.Scripts
{
    public class Player : GameObject
    {
        private PlayerInput input;
        private PlayerStats playerStats;
        public PlayerStats PlayerStats => playerStats;
       
        private List<IWeapon> weapons;
        private int currentWeaponIndex;
        private IWeapon currentWeapon;
        private Animation animation;

        public event Action OnPlayerDied; //public para que se llame desde LevelController
       
        private bool facingRight = false;
        private int lastWeaponLevel = 1;
        private float flashTimer;
        private bool isFlashing;

        public Player(int startPosX, int startPosY, int playerWidth, int playerHeight) : base(startPosX, startPosY, playerWidth, playerHeight)
        {
            playerStats = new PlayerStats();
            input = new PlayerInput(transform, playerStats);
            
            weapons = new List<IWeapon>
            {
                new Pistol(transform, playerStats),
                new Subfusil(transform, playerStats),
                new Shotgun(transform, playerStats)
            };
            currentWeaponIndex = 0;
            currentWeapon = weapons[currentWeaponIndex];

            List<Image> frames = new List<Image>();
            frames.Add(Engine.LoadImage("assets/Player/Player_0.png"));
            frames.Add(Engine.LoadImage("assets/Player/Player_1.png"));
            frames.Add(Engine.LoadImage("assets/Player/Player_2.png"));
            frames.Add(Engine.LoadImage("assets/Player/Player_3.png"));
            animation = new Animation(frames, 0.10f); 
        }

        public override void Update()
        {
            if (Engine.KeyPress(Engine.KEY_D)) facingRight = true;
            if (Engine.KeyPress(Engine.KEY_A)) facingRight = false;

            if (Engine.KeyPress(Engine.KEY_1)) SwitchWeapon(0);
            if (Engine.KeyPress(Engine.KEY_2)) SwitchWeapon(1);
            if (Engine.KeyPress(Engine.KEY_3)) SwitchWeapon(2);

            if (playerStats.WeaponLevel != lastWeaponLevel)
            {
                UpdateWeapon();
                lastWeaponLevel = playerStats.WeaponLevel;
            }

            if (isFlashing)
            {
                flashTimer -= Program.DeltaTime;
                if (flashTimer <= 0)
                {
                    isFlashing = false;
                }
            }

            currentWeapon.UpdateFacing(facingRight);

            input.Update();
            currentWeapon.Update();
            animation.Update();
            
            if (playerStats.IsDead())
            {
                OnPlayerDied?.Invoke(); //inicia el evento
            }
        }
        
        public void TriggerFlash()
        {
            isFlashing = true;
            flashTimer = 0.3f;
        }

        private void SwitchWeapon(int index)
        {
            if (index >= 0 && index < weapons.Count)
            {
                currentWeaponIndex = index;
                currentWeapon = weapons[currentWeaponIndex];
            }
        }

        public void UpdateWeapon()
        {
            switch (playerStats.WeaponLevel)
            {
                case 1:
                    weapons[0] = new Pistol(transform, playerStats);
                    currentWeaponIndex = 0;
                    break;
                case 2:
                    weapons[1] = new Subfusil(transform, playerStats);
                    currentWeaponIndex = 1;
                    break;
                case 3:
                    weapons[2] = new Shotgun(transform, playerStats);
                    currentWeaponIndex = 2;
                    break;
            }
            currentWeapon = weapons[currentWeaponIndex]; //el arma es igual a la currentPosition en la lista de IWeapon
        }
        public override void Render()
        {
            if (!isFlashing || (int)(flashTimer * 10) % 2 == 0) //locura esto chicos eh, seguro que chori no sabe ni como funciona, y si lo sabe me lo cojo
            {
                Engine.DrawFlipped(animation.CurrentFrame, transform.PosX, transform.PosY, facingRight);
            }
            currentWeapon.Render();
        }
    }
}