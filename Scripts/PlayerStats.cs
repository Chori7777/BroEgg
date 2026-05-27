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
        private int maxHealth = 999;
        private int damage = 3;
        private int armor = 0;
        private float lifeSteal = 0;
        private float attackSpeed = 0;
        private float critChance = 0;
        private float agility = 0;  
        private float moveSpeed = 2f;
        private int weaponLevel = 1;


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
        public int WeaponLevel => weaponLevel;
     

        public PlayerStats()
        {
            health = maxHealth;
        }
        public void RestoreHealth(int amount = 0)
        {
            if (amount == 0)
            {
                health = maxHealth;
            }
            else
            {
                health = Math.Min(health + amount, maxHealth);  // no nos pasamos de la vida máxima
            }
        }
        public void GetDamaged(int amount = 1)
        {
            float dodgeChance = agility * 0.5f;
            if (Random.Shared.NextSingle() * 100 < dodgeChance)
            {
                return;
            }

            int reducedDamage = Math.Max(1, amount - armor);
            health -= reducedDamage;
        }
        public (int finalDamage, bool isCrit, int lifeStealAmount) CalculateDamage()
        {
            bool isCrit = Random.Shared.NextSingle() * 100 < critChance;
            int finalDamage = isCrit ? damage * 2 : damage;
            int lifeStealAmount = (int)(finalDamage * (lifeSteal / 100f));

            return (finalDamage, isCrit, lifeStealAmount);
        }
        public bool IsDead()
        {
            return health <= 0;
        }
        public bool UpgradeMaxHealth()
        {
            maxHealth += 5;
            health += 5;
            return true;
        }

        public bool UpgradeDamage()
        {
            damage += 3;
            return true;
        }

        public bool UpgradeArmor()
        {
            armor += 1;
            return true;
        }

        public bool UpgradeMoveSpeed()
        {
            moveSpeed += 0.5f;
            return true;
        }

        public bool UpgradeAttackSpeed()
        {
            attackSpeed += 0.25f;
            return true;
        }
        public bool UpgradeLifeSteal()
        {
            lifeSteal += 5f;
            return true;
        }
        public bool UpgradeCritChance()
        {
            critChance+=5f;
            return true;
        }
        public bool UpgradeAgility()
        {
            agility += 5;
            return true;
        }

        public bool UpgradeWeapon()
        {
            if (weaponLevel < 3)
            {
                weaponLevel++;
                return true;
            }
            return false;
        }
    }
}
