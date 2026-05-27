using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    internal class HUD
    {
        private Font hudFont;
        private LevelController levelController;
        public HUD(LevelController levelController)
        {
            this.levelController = levelController;
            hudFont = new Font("assets/arial.ttf", 34);
        }
        public void Render()
        {
            Engine.DrawText("Tiempo: " + levelController.LevelManager.Timer + " / " + levelController.LevelManager.CurrentWaveData.WaveTime, 880, 10, 255, 255, 255, hudFont);
            Engine.DrawText("Enemigos: " + levelController.LevelManager.EnemiesKilled, 20, 100, 255, 255, 255, hudFont);
            Engine.DrawText("Ronda: " + levelController.LevelManager.CurrentRound, 20, 150, 255, 255, 255, hudFont); ;
            Engine.DrawText("Vida: " + levelController.Player.PlayerStats.Health, 10, 50, 255, 255, 255, hudFont);

            var expSystem = levelController.LevelManager.ExpSystem;

            int expActual = (int)expSystem.CurrentExp;
            int expNecesaria = (int)expSystem.ExpToNextLevel;

            Engine.DrawText("Nivel: " + expSystem.CurrentLevel, 10, 450, 255, 255, 0, hudFont);
            Engine.DrawText("EXP: " + expActual + " / " + expNecesaria, 10, 500, 200, 200, 200, hudFont);

            if (expSystem.AvailableStatPoints > 0)
            {
                Engine.DrawText("Puntos: " + expSystem.AvailableStatPoints, 10, 550, 0, 255, 0, hudFont);
            }
        }
    }
}


