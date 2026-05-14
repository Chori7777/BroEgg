using System.Diagnostics;

namespace ProyectoSDL2.Engine.Scripts
{
    class Program
    {
        static float deltaTime;

        static public bool isGameRunning = true;

        public static float DeltaTime => deltaTime;

        static void Main(string[] args)
        {

            Engine.Initialize();

            GameManager.Instance.Start();

            DateTime startTime = DateTime.Now;
            float currentTime;

            float lastFrameTime = 0;

            while (isGameRunning)
            {
                currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                deltaTime = currentTime - lastFrameTime;
                lastFrameTime = currentTime;

                GameManager.Instance.Update();
                GameManager.Instance.Render();
            }

        }


    }

}

