namespace ProyectoSDL2.Engine.Scripts
{
    public enum GAME_STATE
    {
        START = 0,
        GAME = 1,
        WIN = 2,
        END = 3
    }

    public class GameManager
    {
        static GameManager instance;
        private GAME_STATE gameState = GAME_STATE.START;
        private LevelController levelController = new LevelController();

        public LevelController LevelController { get { return levelController; } }

        static public GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void Start()
        {
            levelController.Start();
        }

        public void Update()
        {
            switch (gameState)
            {
                case GAME_STATE.START:
                    if (Engine.KeyPress(Engine.KEY_X))
                    {
                        gameState = GAME_STATE.GAME;
                    }
                    break;

                case GAME_STATE.GAME:
                    levelController.Update();
                    break;

                case GAME_STATE.WIN:
                    if (Engine.KeyPress(Engine.KEY_X))
                    {
                        Restart();
                    }
                    break;

                case GAME_STATE.END:
                    if (Engine.KeyPress(Engine.KEY_X))
                    {
                        Restart();
                    }
                    break;
            }
        }

        public void Render()
        {
            switch (gameState)
            {
                case GAME_STATE.START:
                    Engine.Clear();
                    Engine.Draw("assets/MainMenu.png", 0, 0);
                    Engine.Show();
                    break;

                case GAME_STATE.GAME:
                    levelController.Render();
                    break;

                case GAME_STATE.WIN:
                    Engine.Clear();
                    Engine.Draw("assets/PantallaVictoria.png", 0, 0);
                    Engine.Show();
                    break;

                case GAME_STATE.END:
                    Engine.Clear();
                    Engine.Draw("assets/PantallaDerrota.png", 0, 0);
                    Engine.Show();
                    break;
            }
        }

        public void ChangeGameState(GAME_STATE newState)
        {
            gameState = newState;
        }

        private void Restart()
        {
            levelController = new LevelController();
            levelController.Start();
            gameState = GAME_STATE.GAME;
        }
    }
}