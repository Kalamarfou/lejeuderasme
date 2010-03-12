using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;

namespace UltimateErasme.Collisions
{
    public class MechantVulnerableBox
    {
        public Rectangle Box { get; set; }
        public Mechant Mechant { get; set; }

        public MechantVulnerableBox(Rectangle box, Mechant mechant)
        {
            this.Box = box;
            this.Mechant = mechant;
        }
    }
}
