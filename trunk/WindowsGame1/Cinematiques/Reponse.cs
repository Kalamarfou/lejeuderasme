using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UltimateErasme.Cinematiques
{
    public class Reponse
    {

        public string Texte { get; private set; }

        public SpriteFont Font { get; set; }
        public List<DialogueElement> includedElements { get; set; }

        public Vector2 reponsePosition { get; set; }
        public bool Selected { get; set; }

        public Reponse(String texte)
        {
            Texte = texte;
            includedElements = new List<DialogueElement>();
            Selected = false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            String textToDraw;
            Color color;

            if (Selected)
            {
                textToDraw = "O  " + Texte;
                color = Color.Red;
            }
            else
	        {
                textToDraw = "   " + Texte;
                color = Color.Gray;
	        }

            spriteBatch.DrawString(Font, textToDraw, reponsePosition, color);
        }
    }
}
