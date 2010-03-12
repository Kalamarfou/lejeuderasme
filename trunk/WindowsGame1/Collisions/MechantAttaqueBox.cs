using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework;

namespace UltimateErasme.Collisions
{
    public class MechantAttaqueBox
    {
        public Rectangle Box { get; set; }
        public Mechant Mechant { get; set; }

        public MechantAttaqueBox(Rectangle box, Mechant mechant)
        {
            this.Box = box;
            this.Mechant = mechant;
        }
    }
}
