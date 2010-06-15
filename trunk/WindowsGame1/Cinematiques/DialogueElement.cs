using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UltimateErasme.Cinematiques
{
    public class DialogueElement
    {

        public GameObject Personnage { get; private set; }
        public string Text { get; private set; }
        public Color Color { get; private set; }
        public bool Played { get; set; }

        public DialogueElement(GameObject personnage, string text, Color color)
        {
            this.Personnage = personnage;
            this.Text = text;
            this.Color = color;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }


    }
}
