using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace UltimateErasme.GameObjects.Classes
{
    public class BelleExplosionCollision
    {
        public Rectangle Rectangle { get; set; }
        public double HeureDeCreation { get; set; }
        public BelleExplosionCollision(int x, int y, int width, int height, GameTime time)
        {
            Rectangle = new Rectangle(x, y, width, height);
            HeureDeCreation = time.TotalGameTime.TotalMilliseconds ;
           

        }
    }
}
