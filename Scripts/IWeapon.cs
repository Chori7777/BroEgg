using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public interface IWeapon
    {
        float FireRate { get; }
        int Damage { get; }
        void Update();
        void Render();
        void UpdateFacing(bool isFacingRight);
    }
}
