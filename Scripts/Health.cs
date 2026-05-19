namespace ProyectoSDL2.Engine.Scripts
{
    public class Health
    {
        private int hp;
        private int maxHp;
        public int Hp => hp; // Propiedad de solo lectura para acceder a hp desde fuera de la clase por que soy un crack
        public int MaxHp => maxHp; // Propiedad de solo lectura para acceder a maxHp desde fuera de la clase
        public Health(int startingHp)
        {
            hp = startingHp;
            maxHp= startingHp;
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