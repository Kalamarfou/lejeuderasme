using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects.enums;
using UltimateErasme.InputTesters;


namespace UltimateErasme.GameObjects
{
    public class JumpManager
    {
        public JumpState jumpState;
        public int hauteurDuSol;
        public Vector2 jumpVelocity;
        public string sensDuDoubleSaut = "";

        public JumpManager()
        {
            jumpVelocity = new Vector2(0, 4);
            jumpState = JumpState.auSol;
        }
    }
}
