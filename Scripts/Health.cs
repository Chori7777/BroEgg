using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Health
    {
        int health = 2;


        public void GetDamaged()
        {
            health -= 1;
            Engine.Debug($"Vida: {health}");

            if(health <= 0)
            {
                GameManager.Instace.ChangeGameState(GAME_STATE.END);
            }
        }
    }
}
