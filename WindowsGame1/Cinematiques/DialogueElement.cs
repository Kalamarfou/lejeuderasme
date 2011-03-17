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
		public string Sound { get; private set; }
        public Color Color { get; private set; }
        public bool Played { get; set; }
        public bool SoundPlayed { get; set; }
        public SpriteFont Font { get; private set; }
        public List<Reponse> reponses { get; set; }

        public DialogueElement(GameObject personnage, string text, string sound, Color color, SpriteFont font)
        {
            this.Personnage = personnage;
            this.Text = text;
            this.Color = color;
            this.Font = font;
			this.Sound = sound;
            this.SoundPlayed = false;
            reponses = new List<Reponse>();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
			try{
                int h = Personnage.Sprite.Height;
				spriteBatch.Draw(Personnage.Sprite, new Vector2(0, 600 - h), Color.White);
			}
			catch (Exception e) {}
            spriteBatch.DrawString(Font, Text, new Vector2(170, 500), Color.Black);
            if (reponses.Count > 0)
            {
                int i = 30;
                foreach (Reponse rep in reponses)
                {
                    rep.Font = Font;
                    rep.reponsePosition = new Vector2(190, 500 + i);
                    i = i + 30;
                    rep.Draw(spriteBatch, gameTime);
                }
            }
            spriteBatch.End();
        }
		
    }
}
