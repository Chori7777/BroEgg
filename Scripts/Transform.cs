using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Transform
    {
        //posiciones
        int posX,posY;

        public int PosX => posX; 
        public int PosY => posY;

        //constructor
        public Transform(int x, int y)
        {
            posX = x;
            posY = y;
        }

        //translate
        public void Translate(int moveX, int moveY)
        {
            posX += moveX;
            posY += moveY;
        }


    }
}
