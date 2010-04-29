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
        SpriteFont succesFont;
        Vector2 succesFontPosition;
        Vector2 succesPosition;
        public SuccesCollection SuccesCollection { get; set; }


        public SuccesManager(UltimateErasme game)
        {
            succesFont = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            succesFontPosition = new Vector2(400, 200);
            SuccesCollection = new SuccesCollection();

            SuccesCreator(game);

            succesPosition = new Vector2(400, 100);
        }

        private void SuccesCreator(UltimateErasme game)
        {
            
            #region normaux
            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Tigrou"),
                SuccesEvents.Saut, 10, "Expert en Tigrou Bouing-Bouing", 100)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\SautHauteur"),
                    SuccesEvents.DoubleSaut, 10, "Expert en Doubles Sauts", 150)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Graisse"),
                    SuccesEvents.KillALaGraisse, 15, "Graisse Mortelle", 200)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Bulo"),
                    SuccesEvents.KillAuBulo, 10, "Maitre de Bulorang 3eme Dan", 200)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Eclair"),
                    SuccesEvents.KillALEclair, 10, "Maitre du chapeau pointu", 250)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Transformation"),
                    SuccesEvents.Transformation, 10, "Transformeur fou", 100)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                    SuccesEvents.KillALExplosion, 10, "Exploseur fou", 250)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                    SuccesEvents.SuicideALExplosion, 10, "Maitre suicideur", 100)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                    SuccesEvents.SortageDeBulo, 10, "Fan du Bulo", 100)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                    SuccesEvents.RentrageDeBulo, 10, "Rentreur de Bulo professionel", 100)
                );

            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                    SuccesEvents.AttaqueTournoyante, 20, "Tournoyeur fou", 350)
                );
            #endregion


            #region combo
            SuccesCollection.Add
                (
                new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                    SuccesEvents.Combo50Points, 1, "Comboteur", 20)
                );

            SuccesCollection.Add
            (
            new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                SuccesEvents.Combo100Points, 1, "Comboteur 2", 30)
            );

            SuccesCollection.Add
            (
            new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                SuccesEvents.Combo200Points, 1, "Comboteur fou", 50)
            );

            SuccesCollection.Add
            (
            new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                SuccesEvents.Combo500Points, 1, "Comboteur fou 2", 150)
            );

            SuccesCollection.Add
            (
            new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                SuccesEvents.Combo1000Points, 1, "Comboteur Ultime", 300)
            );
            #endregion


            #region special
            SuccesCollection.Add
            (
            new Succes(game.Content.Load<Texture2D>(@"Sprites\Succes\Base"),
                SuccesEvents.FarmeurDeBulo, 100, "Farmeur d'XP", 400)
            );
            #endregion
        }

        public void Update(GameTime gameTime)
        {
            foreach (Succes succes in SuccesCollection)
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
            Vector2 tempSuccesPosition = new Vector2(succesPosition.X, succesPosition.Y);
            Vector2 tempSuccesFontPosition = new Vector2(succesFontPosition.X, succesFontPosition.Y);

            foreach (Succes succes in SuccesCollection)
            {
                if (succes.TimerSucces.Enabled)
                {
                    spriteBatch.Draw(succes.Sprite, tempSuccesPosition, null, Color.White, succes.Rotation, succes.Center, succes.Scale, SpriteEffects.None, 0);
                    tempString = succes.Titre + ": " + succes.XpRecu.ToString() + " XP";
                    fontOrigin = succesFont.MeasureString(tempString) / 2;
                    spriteBatch.DrawString(succesFont, tempString, tempSuccesFontPosition, Color.OrangeRed, 0, fontOrigin, 1.2f, SpriteEffects.None, 0);
                    tempSuccesPosition += new Vector2(120, 0);
                    tempSuccesFontPosition += new Vector2(0, 20);
                }
            }
        }
    }
}
