using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;

namespace UltimateErasme.Collisions
{
    public class GraisseAttaqueBox
    {
        public Rectangle Box { get; set; }
        public GameObject Boule { get; set; }

        public GraisseAttaqueBox(Rectangle box, GameObject Boule)
        {
            this.Box = box;
            this.Boule = Boule;
        }
    }
}
