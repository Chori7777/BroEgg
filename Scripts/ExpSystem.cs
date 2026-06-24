using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class ExpSystem
    {
        //Sistema de XP
        private int currentLevel = 1;
        private float currentExp = 0;
        private float expToNextLevel = 100;
        private int availableStatPoints = 0;
        // Las variabledirijillas publicas
        public int CurrentLevel => currentLevel;
        public float CurrentExp => currentExp;
        public float ExpToNextLevel => expToNextLevel;
        public int AvailableStatPoints => availableStatPoints;

        //Sumar XP desde levelManager.OnEnemyKilled()
        public void AddExp(float amount)
        {
            currentExp += amount;
            CheckLevelUp();
        }
        //Cada vez q se sume XP que se cheque si no subio de nivel
        private void CheckLevelUp()
        {
            while (currentExp >= expToNextLevel)
            {
                currentExp -= expToNextLevel;
                currentLevel++;
                availableStatPoints++;
                expToNextLevel = CalculateExpForNextLevel();
            }
        }
        //Calculos para la escala de niveles y hacerlo mas automatico
        private float CalculateExpForNextLevel()
        {
            return expToNextLevel * currentLevel * 1.5f;
        }
        // Para la pantalla de transicion 
        public bool SpendStatPoint()
        {
            if (availableStatPoints > 0)
            {
                availableStatPoints--;
                return true;
            }
            return false;
        }
        // Reiniciar las variables cuando se reinicia el juego
        public void Reset()
        {
            currentLevel = 1;
            currentExp = 0;
            expToNextLevel = 100;
            availableStatPoints = 0; 
        }

    }

}
