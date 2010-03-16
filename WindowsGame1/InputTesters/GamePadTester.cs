using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.GameObjects.enums;
using System.Collections;

namespace UltimateErasme.InputTesters
{
            
    public class GamePadTester
    {
        public GamePadState previousGamePadState { get; set; }
        public GamePadState gamePadState { get; set; }

        public GamePadTester()
        {
            previousGamePadState = GamePad.GetState(PlayerIndex.One);
            gamePadState = GamePad.GetState(PlayerIndex.One);
        }

        internal bool test(Buttons button)
        {
            if (gamePadState.IsButtonDown(button) && 
                previousGamePadState.IsButtonUp(button))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void ChooseGamePad(ControllerType controllerType)
        {
            if (controllerType == ControllerType.xBoxControler1 || controllerType == ControllerType.keyboardPlusXBoxControler1)
            {
                this.gamePadState = GamePad.GetState(PlayerIndex.One);
            }
            if (controllerType == ControllerType.xBoxControler2)
            {
                this.gamePadState = GamePad.GetState(PlayerIndex.Two);
            }
        }

        internal void UpdatePreviousGamePadState()
        {
            this.previousGamePadState = this.gamePadState;
        }

        internal float GetStickX()
        {
            return gamePadState.ThumbSticks.Left.X;
        }
    }
}
