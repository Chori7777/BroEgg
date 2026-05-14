namespace ProyectoSDL2.Engine.Scripts
{
    public class Animation
    {
        private List<Image> frames;
        private int currentFrameIndex = 0;
        private float speed;
        private float timer = 0;

        public Image CurrentFrame => frames[currentFrameIndex];

        public Animation(List<Image> frames, float speed)
        {
            this.frames = frames;
            this.speed = speed;
        }

        public void Update()
        {
            timer += Program.DeltaTime;

            if (timer >= speed)
            {
                currentFrameIndex++;
                timer = 0;

                if (currentFrameIndex >= frames.Count)
                {
                    currentFrameIndex = 0;
                }
            }
        }
    }
}