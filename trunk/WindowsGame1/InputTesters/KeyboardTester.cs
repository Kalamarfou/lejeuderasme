using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme.InputTesters
{
    class KeyboardTester
    {
        public KeyboardState previousKeyboardState {get;set;} 
        public KeyboardState keyboardState {get;set;} 

        public KeyboardTester()
        {
            previousKeyboardState = Keyboard.GetState();
            keyboardState = Keyboard.GetState();
        }

        internal void UpdatePreviousKeyboardState()
        {
            this.previousKeyboardState = this.keyboardState;
        }



        internal void GetKeyboard()
        {
            keyboardState = Keyboard.GetState();
        }

        internal bool test(Keys key)
        {
            if (keyboardState.IsKeyDown(key) &&
                previousKeyboardState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool testEnfonceInfini(Keys key)
        {
            if (keyboardState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
