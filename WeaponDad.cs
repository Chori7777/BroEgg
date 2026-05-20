using ProyectoSDL2.Engine.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine
{
    abstract public class WeaponDad //clase padre de las armas
    {
        protected Transform ownerTransform;

        public WeaponDad(Transform ownerTransform)
        {
            this.ownerTransform = ownerTransform;
        }

        public abstract void Update(); //los hijos definen su update (escalable con las nuevas armas)
    }
}
