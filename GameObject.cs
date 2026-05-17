using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoSDL2.Engine.Scripts;

namespace ProyectoSDL2.Engine
{
    public abstract class GameObject
    {
        protected Transform transform;
        public Transform Transform => transform;
        private int posX, posY, width, height;

        public bool IsPendingDestroy = false; // esto es para cuando hay que destruir algo en las colisiones

        public GameObject(int startPosX, int startPosY, int itsWidth, int itsHeight)
        {
            transform = new Transform(startPosX, startPosY, itsWidth, itsHeight);
        }

        public abstract void Update(); //el abstract OBLIGA a los hijos a definir el Update del GameObject

        public abstract void Render();
    }
}
