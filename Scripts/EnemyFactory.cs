namespace ProyectoSDL2.Engine.Scripts
{
    public static class EnemyFactory
    {
        public enum TypeEnemy { TankEnemy, FastEnemy, BasicEnemy, SoldierEnemy }

        public static Enemy CreateEnemy(TypeEnemy enemy, int x, int y, int wave)
        {

            switch (enemy)
            {
                case TypeEnemy.TankEnemy: return new TankEnemy(x, y, wave); break; //x, y, wave se asignan en el metodo SpawnWave en LevelManager
                case TypeEnemy.BasicEnemy: return new BasicEnemy(x, y, wave); break;
                case TypeEnemy.SoldierEnemy: return new SoldierEnemy(x, y, wave); break;
                case TypeEnemy.FastEnemy: return new FastEnemy(x, y, wave); break;
                default: return null;

            }
        }
    }
}