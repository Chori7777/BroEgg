namespace ProyectoSDL2.Engine.Scripts
{
    public class Health
    {
        private int hp;

        public Health(int startingHp)
        {
            hp = startingHp;
        }

        public void GetDamaged(int amount = 1)
        {
            hp -= amount;
            Engine.Debug($"HP restante: {hp}");
        }

        public bool IsDead()
        {
            return hp <= 0; // devuelve true cuando hp es 0 o menor
        }
    }
}