namespace ProyectoSDL2.Engine.Scripts
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy(int startPosX, int startPosY, int wave)
            : base(startPosX, startPosY, 100, 100, GetFrames(), new EnemyStats(
                hpEnemy: 2 + (2 * wave),
                dmgEnemy: 1 + (1 * wave),
                moveSpeedEnemies: 200f,
                armorEnemies: 1 + (1 * wave)
              ))
        {
        }

        private static List<Image> GetFrames()
        {
            return new List<Image>
            {
                Engine.LoadImage("assets/enemy/BasicEnemy_0.png"),
                Engine.LoadImage("assets/enemy/BasicEnemy_1.png"),
                Engine.LoadImage("assets/enemy/BasicEnemy_2.png"),
                Engine.LoadImage("assets/enemy/BasicEnemy_3.png")
            };
        }
    }
}