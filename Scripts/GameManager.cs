using System.Windows.Markup;

namespace ProyectoSDL2.Engine.Scripts
{
    public enum GAME_STATE
    {
        START = 0,
        GAME = 1,
        TRANSICION = 2,
        WIN = 3,
        END = 4
    }

    public class GameManager
    {
        static GameManager instance;
        private GAME_STATE gameState = GAME_STATE.START;
        private LevelController levelController = new LevelController();

        public LevelController LevelController { get { return levelController; } } //por aca se accede al levelController

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
                case GAME_STATE.TRANSICION:
                    if(Engine.KeyPress(Engine.KEY_X))
                    {
                        Continue();
                    }
                    break;
            }
        }
        
        public void Continue()
        {
            levelController.NextLevel();
            gameState = GAME_STATE.GAME;
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
                case GAME_STATE.TRANSICION:
                    Engine.Clear();
                    Engine.Draw("assets/PantallaTransicion.png", 0, 0);
                    Engine.Show();
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