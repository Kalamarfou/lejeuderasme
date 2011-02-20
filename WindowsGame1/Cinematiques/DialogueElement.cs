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

        public DialogueElement(GameObject personnage, string text, string sound, Color color, SpriteFont font)
        {
            this.Personnage = personnage;
            this.Text = text;
            this.Color = color;
            this.Font = font;
			this.Sound = sound;
            this.SoundPlayed = false;
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
            spriteBatch.End();
        }
		
    }
}
