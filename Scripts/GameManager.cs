using System.Windows.Markup;

namespace ProyectoSDL2.Engine.Scripts
{
    public enum GAME_STATE
    {
        START,
        GAME,
        TRANSICION,
        WIN,
        END,
        PAUSE,
    }

    public class GameManager
    {
        Font font;
        static GameManager instance;
        private GAME_STATE gameState = GAME_STATE.START;
        private LevelController levelController = new LevelController();
        private StatsScreen statsScreen;

        private bool wasEscPressedLastFrame = false;
        private bool wasPPressedLastFrame = false; 
        public LevelController LevelController { get { return levelController; } } //por aca se accede al levelController privado

        static public GameManager Instance //constructor static para que se acceda desde cualquier lado
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager(); //solo se hace new GameManager si no existe
                }
                return instance;
            }
        }

        public void Start()
        {
            levelController.Start();
             font = Engine.LoadFont("Fonts/arial.ttf", 24);
            statsScreen = new StatsScreen(levelController.Player.PlayerStats, levelController.LevelManager.ExpSystem);
        }

        public void Update()
        {
            // Declaramos la variable FUERA del switch para que esté en todos los cases
            bool isEscPressedNow = Engine.KeyPress(Engine.KEY_ESC);
            bool isPPressedNow = Engine.KeyPress(Engine.KEY_P); // Usamos P para pausar/despausar

            switch (gameState)
            {
                case GAME_STATE.START:
                    if (Engine.KeyPress(Engine.KEY_X))
                    {
                        gameState = GAME_STATE.GAME;
                    }
                    break;

                case GAME_STATE.GAME:
                  
                    if (isPPressedNow && !wasPPressedLastFrame)
                    {
                        gameState = GAME_STATE.PAUSE;
                    }
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
                    statsScreen.Update();
                    if (Engine.KeyPress(Engine.KEY_X))
                    {
                        Continue();
                    }
                    break;
                case GAME_STATE.PAUSE:
                   
                    if (isPPressedNow && !wasPPressedLastFrame)
                    {
                        gameState = GAME_STATE.GAME;
                    }
               
                    if (isEscPressedNow && !wasEscPressedLastFrame)
                    {
                        Program.isGameRunning = false;
                    }
                    break;
            }

            // Actualizamos los flags al FINAL del Update()
            wasEscPressedLastFrame = isEscPressedNow;
            wasPPressedLastFrame = isPPressedNow;
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
                    Engine.Draw("assets/Screens/SplashScreen.png", 0, 0);
                    Engine.Show();
                    break;

                case GAME_STATE.GAME:
                    levelController.Render();
                    break;
                case GAME_STATE.TRANSICION:
                    Engine.Clear();
                 
                    statsScreen.Render();
                  
                    Engine.Show();
                    break;

                case GAME_STATE.WIN:
                    Engine.Clear();
                    Engine.Draw("assets/Screens/WinScreen.png", 0, 0);
                    Engine.Show();
                    break;

                case GAME_STATE.END:
                    Engine.Clear();
                    Engine.Draw("assets/Screens/LoseScreen.png", 0, 0);
                    Engine.Show();
                    break;
                case GAME_STATE.PAUSE:
                    Engine.Clear();
                    
                    levelController.Render();
  
                    Engine.DrawText("PAUSA - P para continuar", 500, 350, 24, 255, 255, font);
                    Engine.DrawText("ESC para salir", 520, 400, 20, 200, 200, font);

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
            statsScreen = new StatsScreen(levelController.Player.PlayerStats, levelController.LevelManager.ExpSystem);
            gameState = GAME_STATE.GAME;
        }
    }
}