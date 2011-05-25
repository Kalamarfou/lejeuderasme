using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme
{
    class ButtonMenu
    {
        String texte;
        Color color;
        Color onClickColor;
        Vector2 position;

        public ButtonMenu(String texte, Color color, Color onClickColor, Vector2 position)
        {
            this.texte = texte;
            this.color = color;
            this.onClickColor = onClickColor;
            this.position = position;
        }

        public Boolean isPressed()
        {
            if (((Mouse.GetState().LeftButton == ButtonState.Pressed)
                || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
                && (Math.Abs(Mouse.GetState().X - position.X) < (texte.Length * 9))
                && (Math.Abs(Mouse.GetState().Y - position.Y) < 30))
            {
                return true;
            }
            return false;
        }

        public Boolean isNear(int espace)
        {
            return ((Math.Abs(Mouse.GetState().X - position.X) < (texte.Length * 50))
                && (Math.Abs(Mouse.GetState().Y - position.Y - 13) < espace));
        }

        public Boolean isNear()
        {
            return ((Math.Abs(Mouse.GetState().X - position.X) < (texte.Length * 9))
                && (Math.Abs(Mouse.GetState().Y - position.Y) < 30)) ;
        }

        public String getText()
        {
            return texte;
        }

        public float getX()
        {
            return position.X;
        }

        public float getY()
        {
            return position.Y;
        }

        public Color getColor()
        {
            return color;
        }

        public Color getOnClickColor()
        {
            return onClickColor;
        }

        public void setX(float x)
        {
            position.X = x;
        }

        public void setY(float y)
        {
            position.Y = y;
        }
    }
}
