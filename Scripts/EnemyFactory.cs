namespace ProyectoSDL2.Engine.Scripts
{
    public static class EnemyFactory
    {
        public enum TypeEnemy { TankEnemy, FastEnemy, BasicEnemy, SoldierEnemy }

        public static Enemy CreateEnemy(TypeEnemy enemy, int x, int y, int wave)
        {
            return enemy switch
            {
                TypeEnemy.TankEnemy => new TankEnemy(x, y, wave),
                TypeEnemy.BasicEnemy => new BasicEnemy(x, y, wave),
                TypeEnemy.SoldierEnemy => new SoldierEnemy(x, y, wave),
                TypeEnemy.FastEnemy => new FastEnemy(x, y, wave),
                _ => null
            };
        }
    }
}