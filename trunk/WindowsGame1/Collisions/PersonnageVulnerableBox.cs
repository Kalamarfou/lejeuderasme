using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;

namespace UltimateErasme.Collisions
{
    public class PersonnageVulnerableBox
    {
        public Rectangle Box { get; set; }
        public ErasmeManager ErasmeManager { get; set; }

        public PersonnageVulnerableBox(Rectangle box, ErasmeManager erasmeManager)
        {
            this.Box = box;
            this.ErasmeManager = erasmeManager;
        }
    }
}
