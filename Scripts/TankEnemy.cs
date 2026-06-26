namespace ProyectoSDL2.Engine.Scripts
{
    public class TankEnemy : Enemy
    {
        public TankEnemy(int startPosX, int startPosY, int wave): base(startPosX, startPosY, 100, 100, GetFrames(), 
            new EnemyStats(
                hpEnemy: 10 + (5 * wave),
                dmgEnemy: 2 + (1 * wave),
                moveSpeedEnemies: 200f,
                armorEnemies: 2 + (2 * wave)
              ))
        {
        }

        private static List<Image> GetFrames()
        {
            List<Image> lista = new List<Image>();
            lista.Add(Engine.LoadImage("assets/enemy/TankEnemy_0.png"));
            lista.Add(Engine.LoadImage("assets/enemy/TankEnemy_1.png"));
            lista.Add(Engine.LoadImage("assets/enemy/TankEnemy_2.png"));
            lista.Add(Engine.LoadImage("assets/enemy/TankEnemy_3.png"));
            return lista;
        }
        
    }
}