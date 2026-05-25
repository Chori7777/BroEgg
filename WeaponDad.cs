using ProyectoSDL2.Engine.Scripts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine
{
    abstract public class WeaponDad
    {
        protected Transform ownerTransform;
        protected Image weaponSprite;
        protected bool facingRight;
        protected PlayerStats playerStats;

        public WeaponDad(Transform ownerTransform, string spritePath, PlayerStats playerStats)
        {
            this.ownerTransform = ownerTransform;
            this.playerStats = playerStats;
            weaponSprite = Engine.LoadImage(spritePath);
        }

        public abstract void Update();
        public abstract void Render();

        public void UpdateFacing(bool isFacingRight)
        {
            facingRight = isFacingRight;
        }

        protected int GetWeaponX()
        {
            return facingRight
                ? ownerTransform.PosX + ownerTransform.Width - 10
                : ownerTransform.PosX - 20;
        }

        protected int GetWeaponY()
        {
            return ownerTransform.PosY + ownerTransform.Height / 2 - 10;
        }
    }
}
