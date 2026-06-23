using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public interface IPoolable
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}
