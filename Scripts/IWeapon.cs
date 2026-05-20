using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public interface IWeapon
    {
        float FireRate();
        int DoDamage();
    }
}
