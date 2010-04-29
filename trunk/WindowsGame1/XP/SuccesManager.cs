using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;
using System.Collections;

namespace UltimateErasme.XP
{
    public class SuccesManager
    {
        public Succes SuccesSaut { get; set; }
        public Succes SuccesDoubleSaut { get; set; }

        SpriteFont succesFont;
        Vector2 succesFontPosition;
        Vector2 succesPosition;
        ArrayList succesCollection;


        public SuccesManager(UltimateErasme game)
        {
            succesFont = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            succesFontPosition = new Vector2(400, 200);
            succesCollection = new ArrayList();

            SuccesSaut = new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Tigrou"),
                10, "Expert en Tigrou Bouing-Bouing", 100);
            succesCollection.Add(SuccesSaut);

            SuccesDoubleSaut = new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\SautHauteur"),
                10, "Expert en Doubles Sauts", 150);
            succesCollection.Add(SuccesDoubleSaut);

            succesPosition = new Vector2(400, 100);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Succes succes in succesCollection)
            {
                 succes.TestReussite();
            }
        }

        void timerSucces_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            String tempString = "";
            Vector2 fontOrigin = Vector2.Zero;

            foreach (Succes succes in succesCollection)
            {
                if (succes.TimerSucces.Enabled)
                {
                    spriteBatch.Draw(succes.Sprite, succesPosition, null, Color.White, succes.Rotation, succes.Center, succes.Scale, SpriteEffects.None, 0);
                    tempString = succes.Titre + ": " + succes.XpRecu.ToString() + " XP";
                    fontOrigin = succesFont.MeasureString(tempString) / 2;
                    spriteBatch.DrawString(succesFont, tempString, succesFontPosition, Color.OrangeRed, 0, fontOrigin, 1.2f, SpriteEffects.None, 0);
                }
            }
        }
    }
}
