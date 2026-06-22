using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class StatsScreen
    {
        private PlayerStats playerStats;
        private ExpSystem expSystem; 
        public ExpSystem ExpSystem => expSystem;
        private int selectedOption = 0;
        private string[] options = 
            {
        "Vida Maxima +5",
        "Daño +1",
        "Armadura +1",
        "Robo de Vida +5%",
        "Velocidad de Ataque +10%",
        "Prob. Critico +5%",
        "Agilidad +5%",
        "Velocidad de movimiento +0.5%",
         "Mejorar el Arma"
            };
        private Font font;

        private bool upKeyWasPressed = false;
        private bool downKeyWasPressed = false;
        private bool spaceKeyWasPressed = false;



        public StatsScreen(PlayerStats stats, ExpSystem exp)
        {
            playerStats = stats;
            expSystem = exp;
            font = Engine.LoadFont("assets/arial.ttf",24);
        }
        public void Update()
        {
            // Primero: Checamos el estado de las teclas UNA SOLA VEZ por frame
            bool upKeyIsPressed = Engine.KeyPress(Engine.KEY_W);
            bool downKeyIsPressed = Engine.KeyPress(Engine.KEY_S);
            bool spaceKeyIsPressed = Engine.KeyPress(Engine.KEY_Z);

          
            if (upKeyIsPressed && !upKeyWasPressed)
            {
                selectedOption--;
                if (selectedOption < 0) selectedOption = options.Length - 1;
            }
            upKeyWasPressed = upKeyIsPressed;

            if (downKeyIsPressed && !downKeyWasPressed)
            {
                selectedOption++;
                if (selectedOption >= options.Length) selectedOption = 0;
            }
            downKeyWasPressed = downKeyIsPressed;

            if (spaceKeyIsPressed && !spaceKeyWasPressed && expSystem.AvailableStatPoints > 0)
            {
                ApplyUpgrade(selectedOption);
            }
            spaceKeyWasPressed = spaceKeyIsPressed;
        }

        private void ApplyUpgrade(int option)
        {
            switch (option)
            {
                case 0: playerStats.UpgradeMaxHealth(); break; //aplico lo que quiero y evaluo el caso
                case 1: playerStats.UpgradeDamage(); break;
                case 2: playerStats.UpgradeArmor(); break;
                case 3: playerStats.UpgradeLifeSteal(); break;
                case 4: playerStats.UpgradeAttackSpeed(); break;
                case 5: playerStats.UpgradeCritChance(); break;
                case 6: playerStats.UpgradeAgility(); break;
                case 7: playerStats.UpgradeMoveSpeed(); break;
                case 8: playerStats.UpgradeWeapon(); break;
            }
            expSystem.SpendStatPoint(); //gasto puntos de mejora si es que tengo
        }
        public void Render()
        {
            Engine.Clear();
            Engine.Draw("assets/Screens/ScreenPayStats.png", 0, 0);
            Engine.DrawText("Puntos disponibles " + expSystem.AvailableStatPoints, 100, 40, 255, 255, 0, font);
            Engine.DrawText("NIVEL: " + expSystem.CurrentLevel, 100, 75, 200, 200, 0, font);

            for (int i = 0; i < options.Length; i++)
            {
                byte r = (i == selectedOption) ? (byte)255 : (byte)180;
                byte g = (i == selectedOption) ? (byte)255 : (byte)180;
                byte b = (i == selectedOption) ? (byte)0 : (byte)180;
                Engine.DrawText(options[i], 850, 250 + i * 40, r, g, b, font);
            }

            Engine.DrawText("W/S: Navegar", 550, 500, 150, 150, 150, font);
            Engine.DrawText("Z: Mejorar stat", 550, 530, 150, 150, 150, font);
            Engine.DrawText("X: Continuar", 550, 560, 150, 150, 150, font);
            Engine.DrawText("Ronda actual: " + GameManager.Instance.LevelController.LevelManager.CurrentRound, 800, 100, 255, 255, 255, font);

            Engine.Show();
        }
    }
}
