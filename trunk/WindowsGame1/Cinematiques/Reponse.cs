using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UltimateErasme.Cinematiques
{
    public class Reponse
    {

        public string Texte { get; private set; }

        public SpriteFont Font { get; set; }
        public List<DialogueElement> includedElements { get; set; }

        public Vector2 reponsePosition { get; set; }
        public bool Selected { get; set; }

        public Texture2D graisse;



        public Reponse(String texte, ContentManager contentManager )
        {
            Texte = texte;
            includedElements = new List<DialogueElement>();
            Selected = false;
            graisse = contentManager.Load<Texture2D>(@"Sprites\Graisse\graisse");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            String textToDraw;
            Color color;

            if (Selected)
            {
                textToDraw = Texte;
                color = Color.Red;
                Vector2 graissePosition = new Vector2(reponsePosition.X - 80, reponsePosition.Y - 10);
                spriteBatch.Draw(graisse, graissePosition, null, Color.White);
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
