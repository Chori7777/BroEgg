using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class PlayerStats
    {
        //Stats

        private int health; // CurrentHealth
        private int maxHealth = 10;
        private int damage = 1;
        private int armor = 0;
        private float lifeSteal = 0;
        private float attackSpeed = 0;
        private float critChance = 0;
        private float agility = 0;  
        private float moveSpeed = 5f;
        private float experience = 0f;

        //Gets

        public int Health => health;  
        public int MaxHealth => maxHealth;
        public int Damage => damage; 
        public int Armor => armor; 
        public float LifeSteal => lifeSteal; 
        public float AttackSpeed => attackSpeed;
        public float CritChance => critChance;
        public float Agility => agility;
        public float MoveSpeed => moveSpeed;
        public float Experience => experience;

        public PlayerStats()
        {
            health = maxHealth;
        }

        public void GetDamaged(int amount = 1)
        {
            health -= amount;
        }

        public bool IsDead()
        {
            return health <= 0;
        }

    }
}
