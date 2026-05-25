namespace ProyectoSDL2.Engine.Scripts
{
    public class FastEnemy : Enemy
    {
        public FastEnemy(int startPosX, int startPosY, int wave)
            : base(startPosX, startPosY, 100, 100, GetFrames(), new EnemyStats(
                hpEnemy: 3 + (2 * wave),
                dmgEnemy: 2 + (1 * wave),
                moveSpeedEnemies: 8,
                armorEnemies: 1 + (1 * wave)
              ))
        {
        }

        private static List<Image> GetFrames()
        {
            return new List<Image>
            {
                Engine.LoadImage("assets/enemy/FastEnemy.png"),
                Engine.LoadImage("assets/enemy/FastEnemy1.png"),
                Engine.LoadImage("assets/enemy/FastEnemy2.png"),
                Engine.LoadImage("assets/enemy/FastEnemy3.png")
            };
        }
    }
}