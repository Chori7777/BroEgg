
namespace ProyectoSDL2.Engine.Scripts
{
    public enum GAME_STATE
    {
        START = 0,
        GAME = 1,
        END = 2
    }

    public class GameManager
    {
        static GameManager instance;
        private int score;
        GAME_STATE GAME_STATE = GAME_STATE.START;
        LevelController levelController = new LevelController();

        public LevelController LevelController { get { return levelController; } }

        static public GameManager Instace
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


        public void SetScore(int newScore)
        {
            score = newScore;
        }

        public void Render()
        {
            switch (GAME_STATE)
            {
                case GAME_STATE.START:
                    Engine.Clear();
                    Engine.Draw("assets/MainMenu.png", 0, 0);//renderizar inicio
                    Engine.Show();
                    break;
                case GAME_STATE.GAME:
                    levelController.Render();//renderizar juego
                    break;
                case GAME_STATE.END: //renderizar game over
                    Engine.Clear();
                    Engine.Draw("assets/PantallaDerrota.png", 0, 0);//renderizar inicio
                    Engine.Show();
                    break;

            }
        }
        public void Update()
        {
            switch (GAME_STATE)
            {
                case GAME_STATE.START:
                    if(Engine.KeyPress(Engine.KEY_X))
                    {
                        GAME_STATE = GAME_STATE.GAME;
                    }
                    break;
                case GAME_STATE.GAME:
                    levelController.Update();
                    break;
                case GAME_STATE.END: //renderizar game over
                    break;

            }
        }

        public void ChangeGameState(GAME_STATE newState)
        {
            GAME_STATE = newState;
        }

    }
}
