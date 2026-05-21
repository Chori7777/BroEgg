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
            Engine.DrawText("Tiempo:" + levelController.Timer, 880, 10, 255, 255, 255, hudFont);
            Engine.DrawText("Enemigos: " + levelController.EnemiesKilled, 20, 100, 255, 255, 255, hudFont);
            Engine.DrawText("Ronda: " + levelController.CurrentRound, 880, 100, 255, 255, 255, hudFont);
            Engine.DrawText("Vida: " + levelController.Player.PlayerStats.Health, 10, 50, 255, 255, 255, hudFont);
        }


    }
}


