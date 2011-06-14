using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace UltimateErasme.XP
{
    public class XpManager
    {
        int level = 1;
        int xpRemainingToNextLevel = 10;
        int totalXP = 0;
        int lastXpObtained = 0;
        int XpCombo = 0;
        string XpRaison = "";
        
        Timer timerXp;
        Timer timerCombo;
        bool afficherCombo = false;

        SpriteFont xpFont;
        Vector2 xpFontPosition;
        Vector2 xpComboPosition;
        Vector2 xpTotalPosition;
        Vector2 xpLevelPosition;
        Vector2 xpRemainingToNextLevelPosition;

        SuccesManager succesManager;

        public XpManager(UltimateErasme game)
        {
            xpFont = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            xpFontPosition = new Vector2(400, 520);
            xpComboPosition = new Vector2(400, 540);
            xpTotalPosition = new Vector2(10, 10);
            xpRemainingToNextLevelPosition = new Vector2(10, 30);
            xpLevelPosition = new Vector2(10, 50);

            succesManager = new SuccesManager(game, this);

            timerXp = new Timer();
            timerCombo = new Timer();
        }

        public void Update(GameTime gameTime)
        {
            succesManager.Update(gameTime);
        }

        public void AddXp(XpEvents xpEvent)
        {
            switch (xpEvent)
            {
                case XpEvents.Saut:
                    AddXp(1, "Saut");
                    succesManager.SuccesCollection[SuccesEvents.Saut].NombreDeFoisReussi++;
                    break;
                case XpEvents.DoubleSaut:
                    AddXp(2, "Double saut");
                    succesManager.SuccesCollection[SuccesEvents.DoubleSaut].NombreDeFoisReussi++;
                    break;
                case XpEvents.KillALaGraisse:
                    AddXp(3, "Kill a la graisse");
                    succesManager.SuccesCollection[SuccesEvents.KillALaGraisse].NombreDeFoisReussi++;
                    break;
                case XpEvents.KillAuBulo:
                    AddXp(3, "Kill a coup de Bulo");
                    succesManager.SuccesCollection[SuccesEvents.KillAuBulo].NombreDeFoisReussi++;
                    break;
                case XpEvents.KillALEclair:
                    AddXp(5, "Kill a coup d'eclair de Voltaire");
                    succesManager.SuccesCollection[SuccesEvents.KillALEclair].NombreDeFoisReussi++;
                    break;
                case XpEvents.Transformation:
                    AddXp(1, "Transformation reussie");
                    succesManager.SuccesCollection[SuccesEvents.Transformation].NombreDeFoisReussi++;
                    break;
                case XpEvents.KillALExplosion:
                    AddXp(4, "Kill a l'explosion");
                    succesManager.SuccesCollection[SuccesEvents.KillALExplosion].NombreDeFoisReussi++;
                    break;
                case XpEvents.SuicideALExplosion:
                    AddXp(-4, "Suicide a l'explosion");
                    succesManager.SuccesCollection[SuccesEvents.SuicideALExplosion].NombreDeFoisReussi++;
                    break;
                case XpEvents.SortageDeBulo:
                    AddXp(1, "Sortage de Bulo");
                    succesManager.SuccesCollection[SuccesEvents.SortageDeBulo].NombreDeFoisReussi++;
                    succesManager.SuccesCollection[SuccesEvents.FarmeurDeBulo].NombreDeFoisReussi++;
                    break;
                case XpEvents.RentrageDeBulo:
                    AddXp(1, "Rentrage de Bulo");
                    succesManager.SuccesCollection[SuccesEvents.RentrageDeBulo].NombreDeFoisReussi++;
                    succesManager.SuccesCollection[SuccesEvents.FarmeurDeBulo].NombreDeFoisReussi++;
                    break;
                case XpEvents.AttaqueTournoyante:
                    AddXp(1, "Attaque Tournoyante");
                    succesManager.SuccesCollection[SuccesEvents.AttaqueTournoyante].NombreDeFoisReussi++;
                    break;
                default:
                    return;
            }
            timerXp.Dispose();
            timerXp = new Timer(1500) { Enabled = true };
            timerXp.Elapsed += new ElapsedEventHandler(timerXp_Elapsed);
        }

        void timerXp_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (((Timer)sender) == timerXp)
            {
                timerXp.Enabled = false;
            }
        }

        internal void AddXpSucces(int XpRecu)
        {
            AddXp(XpRecu, "Succes reussi");
        }

        private void AddXp(int xpGagne, string raison)
        {
            totalXP += xpGagne;
            lastXpObtained = xpGagne;
            xpRemainingToNextLevel -= xpGagne;
            ComboManager(xpGagne);
            LevelUpManager();

            XpRaison = raison;
        }

        private void ComboManager(int xpGagne)
        {
            XpCombo += xpGagne;
            if (timerCombo.Enabled)
            {
                afficherCombo = true;
            }
            timerCombo.Dispose();
            timerCombo = new Timer(2000) { Enabled = true };
            timerCombo.Elapsed += new ElapsedEventHandler(timerCombo_Elapsed);
            ComboSuccesManager();
        }

        private void ComboSuccesManager()
        {
            if (XpCombo > 999)
            {
                succesManager.SuccesCollection[SuccesEvents.Combo1000Points].NombreDeFoisReussi++;
            }
            else if (XpCombo > 499)
            {
                succesManager.SuccesCollection[SuccesEvents.Combo500Points].NombreDeFoisReussi++;
            }
            else if (XpCombo > 199)
            {
                succesManager.SuccesCollection[SuccesEvents.Combo200Points].NombreDeFoisReussi++;
            }
            else if (XpCombo > 99 )
            {
                succesManager.SuccesCollection[SuccesEvents.Combo100Points].NombreDeFoisReussi++;
            }
            else if (XpCombo > 49)
            {
                succesManager.SuccesCollection[SuccesEvents.Combo50Points].NombreDeFoisReussi++;
            }
        }

        void timerCombo_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (((Timer)sender) == timerCombo)
            {
                timerCombo.Enabled = false;
                XpCombo = 0;
            }
        }

        private void LevelUpManager()
        {
            if (xpRemainingToNextLevel <= 0)
            {
                int tempXp = -xpRemainingToNextLevel;
                level++;
                xpRemainingToNextLevel = 10 * (int)Math.Pow(level, 2);
                xpRemainingToNextLevel -= tempXp;
                LevelUpManager();
            }
        }

        public int GetCurrentLevel()
        {
            return level;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            String tempString = "" ;
            Vector2 fontOrigin = Vector2.Zero;

            // last XP
            if (timerXp.Enabled)
            {
                tempString = XpRaison + ": " + lastXpObtained.ToString() + " XP";
                fontOrigin = xpFont.MeasureString(tempString) / 2;
                spriteBatch.DrawString(xpFont, tempString, xpFontPosition, Color.Black, 0, fontOrigin, 1, SpriteEffects.None, 0);
            }

            // XP Combo
            if (timerCombo.Enabled && afficherCombo)
            {
                tempString = "COMBO: " + XpCombo.ToString() + " XP";
                fontOrigin = xpFont.MeasureString(tempString) / 2;
                spriteBatch.DrawString(xpFont, tempString, xpComboPosition, Color.OrangeRed, 0, fontOrigin, 1.2f, SpriteEffects.None, 0);
            }

            // XP Total
            tempString = "Total XP: " + totalXP.ToString() + " XP";
            spriteBatch.DrawString(xpFont, tempString, xpTotalPosition, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // XP remaining to next level
            tempString = "XP a farmer jusqu'au prochain level: " + xpRemainingToNextLevel.ToString() + " XP";
            spriteBatch.DrawString(xpFont, tempString, xpRemainingToNextLevelPosition, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // Level
            tempString = "Level actuel: " + level.ToString();
            spriteBatch.DrawString(xpFont, tempString, xpLevelPosition, Color.OrangeRed, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            //Succes manager
            succesManager.Draw(gameTime, spriteBatch);
        }


    }
}
