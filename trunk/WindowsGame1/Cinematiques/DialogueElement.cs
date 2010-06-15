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
        public SpriteFont Font { get; private set; }

        public DialogueElement(GameObject personnage, string text, Color color, SpriteFont font)
        {
            this.Personnage = personnage;
            this.Text = text;
            this.Color = color;
            this.Font = font;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, Text, new Vector2(20, 170), Color.Black);
            spriteBatch.End();
        }


    }
}
